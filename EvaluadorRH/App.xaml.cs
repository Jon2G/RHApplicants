using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;
using System.Windows;
using System;
using Prism.Ioc;
using EvaluadorRH.Views;
namespace EvaluadorRH
{
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<LogIn>(nameof(LogIn));
            containerRegistry.RegisterForNavigation<TestConfirmation>(nameof(TestConfirmation));
            containerRegistry.RegisterForNavigation<ApplicantRegister>(nameof(ApplicantRegister));
            containerRegistry.RegisterForNavigation<AdminView>(nameof(AdminView));
            containerRegistry.RegisterForNavigation<MainTest>(nameof(MainTest));
            containerRegistry.RegisterForNavigation<TestEnd>(nameof(TestEnd));
        }
        internal void UpdateSkin(SkinType skin)
        {
            SharedResourceDictionary.SharedDictionaries.Clear();
            Resources.MergedDictionaries.Add(ResourceHelper.GetSkin(skin));
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
            });
            Current.MainWindow?.OnApplyTemplate();
        }
    }
}
