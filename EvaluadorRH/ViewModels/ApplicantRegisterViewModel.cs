using EvaluadorRH.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluadorRH.ViewModels
{
    public class ApplicantRegisterViewModel
    {
        public List<string> Languajes { get; set; }
        public List<string> Grades { get; set; }
        public List<School> Schools { get; set; }
        public Applicant Applicant { get; set; }

        public ApplicantRegisterViewModel()
        {
            this.Applicant = new Applicant();
            this.Schools = School.GetAll();
            this.Grades = Applicant.GetGrades();
            this.Languajes = Applicant.GetLanguajes();
        }
    }
}
