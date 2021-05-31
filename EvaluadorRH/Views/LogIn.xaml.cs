using EvaluadorRH.Classes;
using EvaluadorRH.Dialogs;
using EvaluadorRH.ViewModels;
using HandyControl.Controls;
using Kit.WPF.Prims;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvaluadorRH.Views
{
    /// <summary>
    /// Lógica de interacción para LogIn.xaml
    /// </summary>
    public partial class LogIn : NavigationUserControl
    {
        public LoginModel Model { get; set; }

        public LogIn(IRegionManager RegionManager) : base(RegionManager)
        {
            this.Model = new LoginModel();
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            this.Model.Password = this.Password.Password;
            this.Password.Password = string.Empty;
            if (this.Model.Login() is Admin admin)
            {
                this.Model.Clear();
                AppData.Instace.Admin = admin;
                this.Push<AdminView>();
            }
            else
            {
                this.Model.Clear();
                Dialog.Show(new TextDialog("Usuario o contraseña incorrectos", "error.png"));
                return;
            }

        }

    }
}
