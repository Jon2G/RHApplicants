using HandyControl.Controls;
using Kit;
using Kit.Sql;
using Kit.Sql.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using EvaluadorRH.Classes.Tests;
using Kit.Model;
using Kit.Sql.Sqlite;
using MessageBox = HandyControl.Controls.MessageBox;

namespace EvaluadorRH.Classes
{
    public class AppData : ModelBase    {
        public const string ContentRegion = "ContentRegion";
        public static AppData Instace { get; private set; }
        public static SQLiteConnection SQLiteConnection { get; private set; }
        public Admin _Admin;
        public Admin Admin { get => _Admin; set { _Admin = value; Raise(()=>Admin); } }
        private AppData() { }
        public static void Init()
        {
            AppData.Instace = new AppData();
            Kit.WPF.Tools.Init();
            AppData.SQLiteConnection = new SQLiteConnection(new FileInfo(Path.Combine(Tools.Instance.LibraryPath, "Evaluador.db")),111);
            AppData.SQLiteConnection.CheckTables(
                typeof(Admin),
                typeof(School),
                typeof(Applicant),
                typeof(Test),
                typeof(WebTest),
                typeof(MainTest),
                typeof(TextSolution),
                typeof(WebSolution));

            

        }

    }
}
