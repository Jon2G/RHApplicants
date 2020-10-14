using HandyControl.Controls;
using Plugin.Xamarin.Tools.Shared.Reflection;
using SQLHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tools;
using MessageBox = HandyControl.Controls.MessageBox;

namespace EvaluadorRH.Classes
{
    public class AppData : ViewModelBase<AppData>
    {
        public const string ContentRegion = "ContentRegion";
        public static AppData Instace { get; private set; }
        public static SQLHLite SQLHLite { get; private set; }
        public Admin _Admin;
        public Admin Admin { get => _Admin; set { _Admin = value; OnPropertyChanged(); } }
        private AppData() { }
        public static void Init()
        {
            AppData.Instace = new AppData();
            SQLHelper.SQLHelper sqlhelper = SQLHelper.SQLHelper.Init(Environment.CurrentDirectory, Debugger.IsAttached);
            Log.Init(sqlhelper.LibraryPath, CriticalLog);
            AppData.SQLHLite = new SQLHLite("1.0.2", "Evaluador.db");
            AppData.SQLHLite.OnCreateDB += AppData.Instace.CreateDb;
            AppData.SQLHLite.RevisarBaseDatos();
        }

        private static void CriticalLog(object sender, EventArgs e)
        {
            if (sender is string s)
            {
                MessageBox.Show(s, "Error critico!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateDb(object sender, EventArgs e)
        {
            if (sender is SQLite.SQLiteConnection SQLHLite)
            {
                AppData.SQLHLite.OnCreateDB -= AppData.Instace.CreateDb;
                try
                {
                    string sql = String.Empty;
                    using (ReflectionCaller caller = new ReflectionCaller())
                    {
                        caller.GetAssembly(this.GetType());
                        using (Stream stream = caller.GetResource("EditaLite.sql"))
                        {
                            using (StreamReader reader = new System.IO.StreamReader(stream, Encoding.ASCII))
                            {
                                sql = reader.ReadToEnd();
                            }
                        }
                    }
                    AppData.SQLHLite.Batch(sql);
                    AppData.SQLHLite.OnCreateDB -= AppData.Instace.CreateDb;
                }
                catch (Exception ex)
                {
                    Log.LogCritical(ex);
                }
            }
        }
    }
}
