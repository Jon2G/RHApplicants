using EvaluadorRH.Classes;
using Plugin.Xamarin.Tools.Shared.Classes;
using Prism.Events;
using Prism.Regions;
using SQLHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tools;

namespace EvaluadorRH.ViewModels
{
    public class LoginModel : ViewModelBase<LoginModel>
    {
        private string _Usuario;
        public string Usuario
        {
            get => _Usuario;
            set
            {
                _Usuario = value;
                OnPropertyChanged();
            }
        }
        private string _Password;
        public string Password
        {
            get => _Password;
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }
        public LoginModel()
        {

        }

        public Admin Login()
        {
            Admin admin = null;
            if (string.IsNullOrEmpty(this.Usuario))
            {
                return admin;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                return admin;
            }
            using (IReader read = AppData.SQLHLite.Leector(
                $"SELECT ID,USUARIO,NAME FROM ADMINISTRADORES WHERE USUARIO='{this.Usuario}' AND PASSWORD='{this.Password}'"))
            {
                if (read.Read())
                {
                    admin = new Admin(Convert.ToInt32(read[0]),Convert.ToString(read[1]), Convert.ToString(read[2]));
                }
            }
            return admin;
        }

        internal void Clear()
        {
            this.Usuario = string.Empty;
            this.Password = string.Empty;
        }
    }
}
