using EvaluadorRH.Classes;
using EvaluadorRH.ViewModels;
using HandyControl.Data;
using Prism.Ioc;
using Prism.Regions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Unity;

namespace EvaluadorRH.Views
{
    public partial class MainWindow
    {
        private readonly IRegionManager RegionManager;
        public MainWindow(IRegionManager RegionManager)
        {
            this.RegionManager = RegionManager;
            InitializeComponent();
            if (this.RegionManager == null)
            {
                throw new ArgumentNullException(nameof(this.RegionManager));
            }

            this.RegionManager.RegisterViewWithRegion("ContentRegion", typeof(LogIn));


            AppData.Init();
        }

        #region Change Skin
        private void ButtonConfig_OnClick(object sender, RoutedEventArgs e) => PopupConfig.IsOpen = true;

        private void ButtonSkins_OnClick(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button button && button.Tag is SkinType tag)
            {
                PopupConfig.IsOpen = false;
                ((App)Application.Current).UpdateSkin(tag);
            }
        }
        #endregion

        private void Nuevo_Click(object sender, RoutedEventArgs e)
        {
            this.RegionManager.RequestNavigate("ContentRegion",
                new Uri(nameof(ApplicantRegister), UriKind.Relative));
        }
    }
}
