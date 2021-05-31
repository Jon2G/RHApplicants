using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit.Model;
using Kit.Sql.Attributes;

namespace EvaluadorRH.Classes.Tests
{
    public class MainTest : ModelBase

    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }
        private Applicant _Applicant;
        [Ignore]
        public Applicant Applicant
        {
            get => _Applicant;
            set
            {
                _Applicant = value;
                Raise(() => Applicant);
            }
        }

        public int ApplicantId
        {
            get => Applicant.Id;
            set
            {
                Applicant = Applicant.Get(value);
                Raise(() => ApplicantId);
            }
        }
        public DateTime Date { get; set; }

     
    }
}
