using EvaluadorRH.Classes;
using HandyControl.Data;
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
using MessageBox = HandyControl.Controls.MessageBox;

namespace EvaluadorRH.Views
{
    /// <summary>
    /// Lógica de interacción para TestConfirmation.xaml
    /// </summary>
    public partial class ApplicantRegister : NavigationUserControl
    {

        private readonly IRegionManager RegionManager;
        public ApplicantRegister(IRegionManager RegionManager)
        {
            this.RegionManager = RegionManager; 
        

            InitializeComponent();
        }

        private void Go_Click(object sender, RoutedEventArgs e)
        {
            this.Model.Applicant.Register();
            GoBack();
        }

        private void Card_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show(new MessageBoxInfo()
            {
                IconBrushKey = ResourceToken.AccentBrush,
                IconKey = ResourceToken.AskGeometry,
                Message = "¿Descartar este registro?",
                Caption = "Atención",
                Button = MessageBoxButton.YesNo,
                //YesContent = "No",
                //NoContent = "Sí"
            }) == MessageBoxResult.Yes)
            {
                var region = this.RegionManager.Regions["ContentRegion"];
                object ordersView = region.ActiveViews.FirstOrDefault(x => x is ApplicantRegister);
                if (ordersView != null)
                {
                    region.Remove(ordersView);
                }
                this.RegionManager.RequestNavigate("ContentRegion", nameof(LogIn));
            }
        }
        private void GoBack()
        {
            var region = this.RegionManager.Regions["ContentRegion"];
            object ordersView = region.ActiveViews.FirstOrDefault(x => x is ApplicantRegister);
            if (ordersView != null)
            {
                region.Remove(ordersView);
            }

            NavigationParameters parmeters = new NavigationParameters {{"Applicant", this.Model.Applicant}};
            this.RegionManager.RequestNavigate("ContentRegion", nameof(TestConfirmation), parmeters);
        }
    }
}
