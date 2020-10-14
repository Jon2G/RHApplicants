using EvaluadorRH.Controls;
using EvaluadorRH.ViewModels;
using HandyControl.Data;
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
    /// Lógica de interacción para MainTest.xaml
    /// </summary>
    public partial class MainTest : NavigationUserControl
    {
        public MainTestModel Model { get; set; }
        public MainTest(IRegionManager RegionManager) : base(RegionManager)
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo()
        {
            base.OnNavigatedTo();
            this.Model = GetParameter<MainTestModel>(nameof(Model));
            OnPropertyChanged(nameof(Model));
            this.Model.Start();
            Window.GetWindow(this).WindowState = WindowState.Maximized;
        }

        private void Continuar(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(new MessageBoxInfo()
            {
                IconBrushKey = ResourceToken.AccentBrush,
                IconKey = ResourceToken.AskGeometry,
                Message = "¿Está seguro de continuar, ya no podrá volver?",
                Caption = "Alerta",
                Button = MessageBoxButton.YesNo,
                YesContent = "No",
                NoContent = "Sí"
            }) == MessageBoxResult.No)
            {
                if (!this.Model.Next())
                {
                    this.PopAll();
                    this.Push<TestEnd>(new Dictionary<string, object>() { { nameof(Model), Model } });
                }
            }

        }


    }
}
