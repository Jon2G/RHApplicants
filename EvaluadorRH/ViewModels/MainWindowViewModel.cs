using Prism.Mvvm;
namespace EvaluadorRH.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Blumitech";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
