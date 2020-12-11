using HandyControl.Controls;
using Kit;
using SQLHelper;
using SQLHelper.Reflection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            AppData.SQLHLite.SetDbScriptResource(typeof(AppData), "EditaLite.sql");
            AppData.SQLHLite.RevisarBaseDatos();
        }

        private static void CriticalLog(object sender, EventArgs e)
        {
            if (sender is string s)
            {
                MessageBox.Show(s, "Error critico!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
