using EvaluadorRH.Classes;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit;

namespace EvaluadorRH.ViewModels
{
    public class AdminViewModel:ViewModelBase<AdminViewModel>
    {
        public string AdminName { get; set; }
        public ObservableCollection<Applicant> Applicants { get; set; }
        public AdminViewModel()
        {
            this.Applicants = new ObservableCollection<Applicant>(Applicant.GetApplicants());
        }

    }
}
