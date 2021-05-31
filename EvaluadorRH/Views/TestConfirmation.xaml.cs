using EvaluadorRH.Classes;

using EvaluadorRH.ViewModels;
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
    /// Lógica de interacción para TestConfirmation.xaml
    /// </summary>
    public partial class TestConfirmation : NavigationUserControl
    {
        public TestConfirmationViewModel Model { get; set; }
        public TestConfirmation(IRegionManager RegionManager) : base(RegionManager)
        {
            this.Model = new TestConfirmationViewModel();
            InitializeComponent();
        }
        protected override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
            this.Model.Applicant = this.GetParameter<Applicant>(nameof(Applicant));
            this.DataContext = this.Model;
        }
        private void Go_Click(object sender, RoutedEventArgs e)
        {
            this.Push<MainTest>(new Dictionary<string, object>() { { nameof(Applicant), this.Model.Applicant } });
        }
    }
}
