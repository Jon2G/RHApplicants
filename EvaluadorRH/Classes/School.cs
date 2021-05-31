using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit.Model;
using Kit.Sql.Attributes;

namespace EvaluadorRH.Classes
{
    public class School:ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }

        public static School Get(int Id) => AppData.SQLiteConnection.Find<School>(Id);
        public School()
        {

        }
        public School(int Id,string Name)
        {
            this.Name = Name;
            this.Id = Id;
        }
        public School(string Name):this(-1,Name)
        {
            
        }
        [InitTable]
        public static void InitTable()
        {
            AppData.SQLiteConnection.InsertAll(
                new School("ESIME Culhuacan - IPN"),
                new School("ESIME Zacatenco - IPN"),
                new School("ESCOM - Escuela Superior de Cómputo - IPN")
                );
        }

        internal static List<School> GetAll() => AppData.SQLiteConnection.Table<School>().ToList();
        public override string ToString()
        {
            return Name;
        }

        internal void Save()
        {
            AppData.SQLiteConnection.InsertOrReplace(this);
        }
    }
}
