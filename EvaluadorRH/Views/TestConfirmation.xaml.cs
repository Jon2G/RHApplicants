using EvaluadorRH.Classes;
using EvaluadorRH.Controls;
using EvaluadorRH.ViewModels;
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
        public MainTestModel Model { get; set; }
        public Applicant Applicant { get; set; }
        public TestConfirmation(IRegionManager RegionManager) : base(RegionManager)
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
            this.Applicant = this.GetParameter<Applicant>(nameof(Applicant));
            this.Model = new MainTestModel(this.Applicant);
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(Applicant));
        }
        private void Go_Click(object sender, RoutedEventArgs e)
        {
            this.Push<MainTest>(new Dictionary<string, object>() { { nameof(Model), this.Model } });
        }
    }
}
