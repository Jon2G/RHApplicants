
using Kit;
using Kit.Sql;
using Kit.Sql.Helpers;
using System;
using System.Collections.Generic;
using Kit.Model;
using Kit.Sql.Attributes;
using Kit.Sql.Readers;
using System.Linq;

namespace EvaluadorRH.Classes
{
    [Table("APPLICANTS")]
    public class Applicant : ModelBase
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Ignore]
        public string FullName { get => $"{Name} {SurName1} {SurName2}"; }
        public string Name { get; set; }
        public string SurName1 { get; set; }
        public string SurName2 { get; set; }
        [Ignore]
        public School School { get; set; }

        public int SchoolId
        {
            get => School.Id;
            set => School = School.Get(value);
        }
        [Ignore]
        public string SchoolName
        {
            get => School?.Name??string.Empty;
            set
            {
                if (School is null)
                {
                    School = new School(value);
                    return;
                }
                School.Name = value;
                Raise(() => SchoolName);
            }
        }
        public string Grade { get; set; }
        public int Age { get; set; }
        public string FavoriteLanguaje { get; set; }
        public DateTime RegisterDate { get; set; }
        public Applicant(int Id, string Name, string SurName1, string SurName2, School School, string Grade, int Age, string FavoriteLanguaje,
            DateTime RegisterDate)
        {
            this.Id = Id;
            this.Name = Name;
            this.SurName1 = SurName1;
            this.SurName2 = SurName2;
            this.School = School;
            this.Grade = Grade;
            this.Age = Age;
            this.FavoriteLanguaje = FavoriteLanguaje;
            this.RegisterDate = RegisterDate;
        }

        internal List<string> GetGrades()
        {
            var grades = new List<string>()
            {
                "1ero","2do","3ro","4to","5to","6to","7mo","8vo","9no"
            };
            grades.AddRange(AppData.SQLiteConnection.Lista<string>("SELECT DISTINCT GRADE FROM APPLICANTS"));
            grades = grades.Distinct().ToList();
            return grades;
        }

        internal List<string> GetLanguajes()
        {
            var languajes = new List<string>()
            {
                "C#","Java","VBA","Phyton","C/C++","F#"
            };
            languajes.AddRange(AppData.SQLiteConnection.Lista<string>("SELECT DISTINCT FavoriteLanguaje FROM APPLICANTS"));
            languajes = languajes.Distinct().ToList();
            return languajes;
        }

        public Applicant()
        {

        }
        public void Register()
        {
            this.RegisterDate = DateTime.Now;
            if (this.School.Id<=0)
            {
                School.Save();
            }
            AppData.SQLiteConnection.Insert(this);
        }
        public static List<Applicant> GetApplicants() => AppData.SQLiteConnection.Table<Applicant>().ToList();

        public Applicant Get(int value) => AppData.SQLiteConnection.Find<Applicant>(value);
    }
}
