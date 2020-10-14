using EvaluadorRH.Classes;
using EvaluadorRH.Controls;
using EvaluadorRH.Dialogs;
using EvaluadorRH.ViewModels;
using HandyControl.Controls;
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
    /// Lógica de interacción para TestConfirmation.xaml
    /// </summary>
    public partial class AdminView : NavigationUserControl
    {
        public AdminViewModel Model { get; set; }
        public AdminView(IRegionManager RegionManager) : base(RegionManager)
        {
            this.Model = new AdminViewModel();
            InitializeComponent();
        }

        private void Evaluar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Applicant app)
            {
                this.Pop();
                NavigationParameters parmeters = new NavigationParameters();
                parmeters.Add("Applicant", app);
                this.Push<TestConfirmation>(parmeters);
            }
        }

        private void Ver_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is Applicant app)
            {

            }
        }

        private void GoBack(object sender, MouseButtonEventArgs e)
        {
            this.Pop();
        }
    }
}
