using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kit;
using Kit.WPF;

namespace EvaluadorRH.Classes
{
    public class Admin:ViewModelBase<Admin>
    {
  
        public readonly int Id;
        public string User { get; private set; }
        public string Name { get; private set; }
        public Admin(int Id, string User, string Name)
        {
            this.Id = Id;
            this.User = User;
            this.Name = Name;
        }

    }
}
