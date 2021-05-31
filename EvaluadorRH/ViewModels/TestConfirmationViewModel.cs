using EvaluadorRH.Classes;
using Kit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluadorRH.ViewModels
{
    public class TestConfirmationViewModel : ModelBase
    {
        private Applicant _Applicant;
        public Applicant Applicant
        {
            get => _Applicant; internal set
            {
                {
                    _Applicant = value;
                    Raise(() => Applicant);
                }
            }
        }
        public TestConfirmationViewModel()
        {

        }
    }
}
