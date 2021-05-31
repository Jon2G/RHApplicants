using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvaluadorRH.Classes.Tests
{
    public class TextSolution : Solution
    {
        public string Languaje { get; set; }
        private string _Answer;
        public string Answer
        {
            get => _Answer;
            set { _Answer = value; Raise(() => Answer); }
        }
        private bool _Finished;
        public bool Finished
        {
            get => _Finished;
            set
            {
                _Finished = value;
                OnPropertyChanged();
            }
        }


        public override Solution Get(int value)
        {
            return AppData.SQLiteConnection.Find<WebSolution>(value);
        }



    }
}
