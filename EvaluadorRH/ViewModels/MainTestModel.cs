using EvaluadorRH.Classes;

using SQLHelper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Kit;

namespace EvaluadorRH.ViewModels
{
    public class MainTestModel : ViewModelBase<MainTestModel>
    {
        private int Id;
        private int _ApplicantId;
        public int ApplicantId
        {
            get => _ApplicantId;
            set
            {
                _ApplicantId = value;
                OnPropertyChanged();
            }
        }

        private string _ApplicantName;
        public string ApplicantName
        {
            get => _ApplicantName;
            set
            {
                _ApplicantName = value;
                OnPropertyChanged();
            }
        }
        private int _TestIndex;
        public int TestIndex
        {
            get => _TestIndex;
            set
            {
                _TestIndex = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ActualTest));
            }
        }
        public DateTime TestDate { get; private set; }

        public ObservableCollection<Test> Tests { get; set; }
        public Test ActualTest
        {
            get
            {
                if (_TestIndex >= Tests.Count || _TestIndex < 0)
                {
                    return null;
                }
                return Tests[_TestIndex];
            }
        }

        private Stopwatch Timer;
        private System.Windows.Threading.DispatcherTimer DispatcherTimer;
        public string Tiempo => String.Format("{0:00}:{1:00}", Timer.Elapsed.Minutes, Timer.Elapsed.Seconds);
        private readonly Locker Locker;
        public MainTestModel(Applicant Applicant)
        {
            this.Locker = new Locker();
            this._TestIndex = -1;
            this.ApplicantId = Applicant.Id;
            this.ApplicantName = Applicant.FullName;
            this.TestDate = DateTime.Now;
            this.Tests = new ObservableCollection<Test>();
            LoadTests();
        }
        private void LoadTests()
        {
            using (IReader reader = AppData.SQLHLite.Leector("SELECT ID,TITLE,MARKDOWN FROM TESTS"))
            {
                while (reader.Read())
                {
                    this.Tests.Add(new Test(Convert.ToInt32(reader[0]), Convert.ToString(reader[1]), Convert.ToString(reader[2]), string.Empty));
                }
            }
        }
        public void Start()
        {
            this.Locker.Hide();
            Next();

            this.Timer = new Stopwatch();
            this.DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this.DispatcherTimer.Tick += DispatcherTimer_Tick;
            this.DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            this.Timer.Start();
            this.DispatcherTimer.Start();

            using (var con = AppData.SQLHLite.Conecction())
            {
                AppData.SQLHLite.EXEC(con,
                    "INSERT INTO MAIN_TESTS(APPLICANT_ID,START_DATE,END_DATE,TIME_ELAPSED) VALUES(?,?,?,?)",
                    ApplicantId,
                    SQLHelper.SQLHelper.FormatTime(DateTime.Now),
                    SQLHelper.SQLHelper.FormatTime(DateTime.Now),
                    SQLHelper.SQLHelper.FormatTime(this.Timer.Elapsed));
                this.Id = AppData.SQLHLite.LastScopeIdentity(con);
            }
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Tiempo));
        }
        public bool Next()
        {
            this.ActualTest?.Save(this.Id);
            this.TestIndex++;
            if (this.TestIndex < this.Tests.Count)
            {
                this.ActualTest.Start();
                return true;
            }
            UpdateAndStop();
            return false;
        }
        private void UpdateAndStop()
        {
            this.Timer.Stop();
            this.DispatcherTimer.Stop();
            this.Locker.Show();
            AppData.SQLHLite.EXEC(
                "UPDATE MAIN_TESTS SET END_DATE=?,TIME_ELAPSED=? WHERE ID=?",
                SQLHelper.SQLHelper.FormatTime(DateTime.Now),
                SQLHelper.SQLHelper.FormatTime(this.Timer.Elapsed),
                ApplicantId);
        }
    }
}
