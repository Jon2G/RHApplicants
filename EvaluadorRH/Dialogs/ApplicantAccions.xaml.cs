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
    public partial class ApplicantAccions : Border
    { 
        public string ApplicantName { get; set; }
        public ApplicantAccions(string ApplicantName)
        {
            this.ApplicantName = ApplicantName;
            InitializeComponent();
        }
    }
}
