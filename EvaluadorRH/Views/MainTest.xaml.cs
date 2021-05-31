using CefSharp;
using CefSharp.Wpf;
using EvaluadorRH.Classes;
using EvaluadorRH.Classes.Tests;
using EvaluadorRH.ViewModels;
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
    /// Lógica de interacción para MainTest.xaml
    /// </summary>
    public partial class MainTest : NavigationUserControl
    {
        public MainTestModel _Model;
        public MainTestModel Model { get => _Model; set { _Model = value; Raise(() => Model); } }
        public MainTest(IRegionManager RegionManager) : base(RegionManager)
        {
            this.DataContext = this;
            InitializeComponent();

        }
        protected override void OnNavigatedTo()
        {
            //base.OnNavigatedTo();
            this.Model = new MainTestModel(GetParameter<Applicant>(nameof(Applicant)));
            this.DataContext = this.Model;
            Raise(() => Model);

            if (Application.Current.MainWindow is not null)
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            this.Model.Start();
        }

        private async void Continuar(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(new MessageBoxInfo()
            {
                IconBrushKey = ResourceToken.AccentBrush,
                IconKey = ResourceToken.AskGeometry,
                Message = "¿Está seguro de continuar, ya no podrá volver?",
                Caption = "Alerta",
                Button = MessageBoxButton.YesNo,
                //YesContent = "No",
                //NoContent = "Sí"
            }) == MessageBoxResult.Yes)
            {
                if (!await this.Model.Next())
                {
                    this.PopAll();
                    this.Push<TestEnd>(new Dictionary<string, object>() { { nameof(Model), Model } });
                }
            }

        }


    }
}
