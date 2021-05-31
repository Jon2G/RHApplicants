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
    public partial class TestEnd : NavigationUserControl
    {
        public MainTestModel Model { get; set; }
        public TestEnd(IRegionManager RegionManager) : base(RegionManager)
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
            this.Model = GetParameter<MainTestModel>(nameof(Model));
            OnPropertyChanged(nameof(Model));
            Window.GetWindow(this).WindowState = WindowState.Normal;
        }
        private void Go_Click(object sender, RoutedEventArgs e)
        {
            this.Pop();
            AppData.Instace.Admin = null;
            this.Push<LogIn>();
        }
    }
}
