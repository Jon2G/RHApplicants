using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit.Model;
using Kit.Sql.Attributes;
using Kit.Sql.Helpers;

namespace EvaluadorRH.Classes.Tests
{

    public abstract class Solution : ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private readonly Stopwatch Timer;
        private readonly System.Windows.Threading.DispatcherTimer DispatcherTimer;
        private string _TimeString;
        public string TimeString
        {
            get => _TimeString ?? $"{Timer.Elapsed.Hours}:{Timer.Elapsed.Minutes:00}:{Timer.Elapsed.Seconds:00}";
            set
            {
                _TimeString = value;
                Raise(() => TimeString);
            }
        }

        private DateTime StartDate;
        public int MainTestId { get; set; }

 
        public Solution()
        {
            Timer = new Stopwatch();
            DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            DispatcherTimer.Tick += (s, e) => Raise(() => TimeString);
            DispatcherTimer.Interval = new TimeSpan(0, 0, 1);

        }

        public abstract Solution Get(int value);

        public void Start()
        {
            StartDate = DateTime.Now;
            DispatcherTimer.Start();
            Timer.Start();
        }
        public void Stop()
        {
            DispatcherTimer.Stop();
            Timer.Stop();
        }
        internal virtual async Task Save(int MainTestId)
        {
            await Task.Yield();
            this.MainTestId = MainTestId;
            Stop();
            AppData.SQLiteConnection.Insert(this);
           
            //AppData.SQLiteConnection.EXEC(
            //    "INSERT INTO COMPLETED_TESTS(MAIN_TEST_ID,TEST_ID,SOLUTION,START_DATE,END_DATE,TIME_ELAPSED) VALUES(?,?,?,?,?,?)",
            //    MainTestId, Id, Solution,
            //    SQLHelper.FormatTime(StartDate),
            //    SQLHelper.FormatTime(DateTime.Now),
            //    SQLHelper.FormatTime(Timer.Elapsed));
        }

    }
}
