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

namespace EvaluadorRH.Dialogs
{
    /// <summary>
    /// Lógica de interacción para TextDialog.xaml
    /// </summary>
    public partial class TextDialog : Border
    {
        public string Text { get; set; }
        public ImageSource Image { get; set; }
        public TextDialog(string Text, string Image)
        {
            this.Image = new BitmapImage(new Uri($"pack://application:,,,/EvaluadorRH;component/Resources/{Image}", UriKind.Absolute)); 
            this.Text = Text;
            InitializeComponent();
        }
    }
}
