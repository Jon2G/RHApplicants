using Plugin.Xamarin.Tools.Shared.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace EvaluadorRH.Classes
{
    public class Test : ViewModelBase<Test>
    {
        public int Id { get; private set; }
        public string Title { get; set; }
        public string MarkDown { get;private set; }
        public string Languaje { get;set; }
        private bool _Finished;
        public bool Finished
        {
            get => _Finished;
            set
            {
                _Finished = value;
                OnPropertyChanged();
            }
        }
        private string _Solution;
        public string Solution
        {
            get => _Solution; set
            {
                _Solution = value;
                OnPropertyChanged();
            }
        }
        private readonly Stopwatch Timer;
        private readonly System.Windows.Threading.DispatcherTimer DispatcherTimer;
        public string Tiempo => String.Format("{0:00}:{1:00}", Timer.Elapsed.Minutes, Timer.Elapsed.Seconds);
        private DateTime StartDate;

        public Test(int Id, string Title, string MarkDown, string Solution)
        {
            this.Id = Id;
            this.Title = Title;
            this.MarkDown = MarkDown;
            this.Solution = Solution;

            this.Timer = new Stopwatch();
            this.DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this.DispatcherTimer.Tick += DispatcherTimer_Tick;
            this.DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }
        public void Start()
        {
            this.StartDate = DateTime.Now;
            this.DispatcherTimer.Start();
            this.Timer.Start();
        }
        public void Stop()
        {
            this.DispatcherTimer.Stop();
            this.Timer.Stop();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(Tiempo));
        }

        internal void Save(int MainTestId)
        {
            this.Stop();
            AppData.SQLHLite.EXEC(
                "INSERT INTO COMPLETED_TESTS(MAIN_TEST_ID,TEST_ID,SOLUTION,START_DATE,END_DATE,TIME_ELAPSED) VALUES(?,?,?,?,?,?)",
                MainTestId,this.Id,this.Solution,
                SQLHelper.SQLHelper.FormatTime(this.StartDate),
                SQLHelper.SQLHelper.FormatTime(DateTime.Now),
                SQLHelper.SQLHelper.FormatTime(this.Timer.Elapsed));
        }
    }
}
