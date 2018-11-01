using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrossWordPuzzle.Model
{
    public class Score : INotifyPropertyChanged
    {

        private int _value;

        public int Value
        {
            get { return _value; }
            set
            {
                _value = value;

                PropChangedHandler();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void PropChangedHandler([CallerMemberName] String propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
