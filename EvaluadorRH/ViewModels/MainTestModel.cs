using EvaluadorRH.Classes;
using Kit.Sql.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using Kit;
using Kit.Sql;
using EvaluadorRH.Classes.Tests;
using CefSharp.Wpf;
using CefSharp;
using Kit.Model;
using Kit.Sql.Readers;

namespace EvaluadorRH.ViewModels
{
    public class MainTestModel : ModelBase
    {
        private IWebBrowser _Browser;

        public IWebBrowser Browser
        {
            get
            {
                if (_Browser is null)
                {
                    var browser = new ChromiumWebBrowser();
                    browser.BrowserSettings = BrowserSettings.Create();
                    browser.BrowserSettings.DefaultEncoding = "utf-8";
                    browser.IsBrowserInitializedChanged += Browser_IsBrowserInitializedChanged; ; ;
                    Browser = browser;
                }
                return _Browser;
            }
            private set
            {
                _Browser = value;
                Raise(() => Browser);
            }
        }

        private void Browser_IsBrowserInitializedChanged(object sender, System.Windows.DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool b && b && e.Property.Name == nameof(Browser.IsBrowserInitialized))
            {
                (sender as ChromiumWebBrowser).IsBrowserInitializedChanged -= Browser_IsBrowserInitializedChanged;
                if (ActualTest is not null)
                    Navigate(ActualTest);
            }
        }

        public ObservableCollection<Solution> Solutions { get; set; }
        public ObservableCollection<Test> Tests { get; set; }

        private int _TestIndex;

        public int TestIndex
        {
            get => _TestIndex;
            set
            {
                _TestIndex = value;
                Raise(() => TestIndex);
                Raise(() => ActualTest);
            }
        }

        public Test ActualTest
        {
            get
            {
                if (_TestIndex >= Tests.Count || _TestIndex < 0)
                {
                    return null;
                }

                var test = Tests[_TestIndex];
                Navigate(test);
                return test;
            }
        }

        private void Navigate(Test test)
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                if (Browser?.IsBrowserInitialized ?? false)
                {
                    //if (test is WebTest webTest)
                    //{
                    //    Browser.Load(webTest.Url);
                    //}
                    //else
                    //{
                        Browser.LoadHtml(test.Html);
                    //}
                }
            }, DispatcherPriority.Send);
        }

        public Solution ActualSolution
        {
            get
            {
                if (_TestIndex >= Tests.Count || _TestIndex < 0)
                {
                    return null;
                }
                var solution = Solutions[_TestIndex];
                return solution;
            }
        }

        private Stopwatch Timer;
        private System.Windows.Threading.DispatcherTimer DispatcherTimer;
        public string TimeString => $"{Timer?.Elapsed.Hours}:{Timer?.Elapsed.Minutes:00}:{Timer?.Elapsed.Seconds:00}";

        private MainTest _Test;

        public MainTest Test
        {
            get => _Test;
            set
            {
                _Test = value;
                Raise(() => Test);
                Raise(() => Applicant);
            }
        }

        public Applicant Applicant
        {
            get => Test.Applicant;
            set
            {
                Test.Applicant = value;
                Raise(() => Applicant);
            }
        }

        public MainTestModel()
        {
        }

        public MainTestModel(Applicant Applicant)
        {
            this._TestIndex = -1;
            this.Test = new MainTest()
            {
                Applicant = Applicant,
                Date = DateTime.Now
            };
            this.Tests = new ObservableCollection<Test>(Classes.Tests.Test.GetAll());
            this.Solutions = new ObservableCollection<Solution>();
        }

        public async void Start()
        {
            Locker.Hide();
            this.Timer = new Stopwatch();
            this.DispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            this.DispatcherTimer.Tick += (o, e) => Raise(() => TimeString);
            this.DispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            this.Timer.Start();
            this.DispatcherTimer.Start();
            AppData.SQLiteConnection.Insert(this.Test);
            await Next();
        }

        public async Task<bool> Next()
        {
            if (ActualSolution is not null)
            {
                await this.ActualSolution.Save(this.Test.Id);
            }

            this.TestIndex++;
            if (this.TestIndex < this.Tests.Count)
            {
                if (this.ActualTest is WebTest)
                {
                    this.Solutions.Add(new WebSolution());
                }
                else
                {
                    this.Solutions.Add(new TextSolution());
                }
                this.ActualSolution?.Start();
                Raise(() => ActualSolution);
                return true;
            }
            UpdateAndStop();
            return false;
        }

        private void UpdateAndStop()
        {
            if (Timer is null)
            {
                return;
            }
            this.Timer?.Stop();
            this.DispatcherTimer?.Stop();
            Locker.Show();
            AppData.SQLiteConnection.Update(this.Test);
        }
    }
}