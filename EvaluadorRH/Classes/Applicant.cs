
using Kit;
using SQLHelper;
using System;
using System.Collections.Generic;

namespace EvaluadorRH.Classes
{
    public class Applicant : ViewModelBase<Applicant>
    {
        public int Id { get; private set; }
        public string FullName { get => $"{Name} {SurName1} {SurName2}"; }
        public string Name { get; set; }
        public string SurName1 { get; set; }
        public string SurName2 { get; set; }
        public string School { get; set; }
        public string Grade { get; set; }
        public int Age { get; set; }
        public string FavoriteLanguaje { get; set; }
        public DateTime RegisterDate { get; set; }
        public Applicant(int Id, string Name, string SurName1, string SurName2, string School, string Grade, int Age, string FavoriteLanguaje,
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
        public Applicant()
        {
            this.Id = -1;
        }
        public void Register()
        {
            using (var con = AppData.SQLHLite.Conecction())
            {
                AppData.SQLHLite.EXEC(con,
                    "INSERT INTO APPLICANTS(NAME,SURNAME1,SURNAME2,SCHOOL,GRADE,AGE,FAVORITE_LANGUAJE,REGISTER_DATE) VALUES(?,?,?,?,?,?,?,?)"
                    , Name, SurName1, SurName2, School, Grade, Age, FavoriteLanguaje, SQLHelper.SQLHelper.FormatTime(DateTime.Now));
                this.Id = AppData.SQLHLite.LastScopeIdentity(con);
            }
        }
        public static List<Applicant> GetApplicants()
        {
            List<Applicant> applicants = new List<Applicant>();
            using (IReader reader = AppData.SQLHLite.Leector("SELECT ID,NAME,SURNAME1,SURNAME2,SCHOOL,GRADE,AGE,FAVORITE_LANGUAJE,REGISTER_DATE FROM APPLICANTS ORDER BY ID DESC"))
            {
                while (reader.Read())
                {
                    applicants.Add(new Applicant(
                        Convert.ToInt32(reader[0]),
                        Convert.ToString(reader[1]),
                        Convert.ToString(reader[2]),
                        Convert.ToString(reader[3]),
                        Convert.ToString(reader[4]),
                        Convert.ToString(reader[5]),
                        Convert.ToInt32(reader[6]),
                        Convert.ToString(reader[7]),
                        DateTime.Parse(Convert.ToString(reader[8]))
                        ));
                }
            }
            return applicants;
        }
    }
}
