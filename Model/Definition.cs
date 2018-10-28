using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CrossWordPuzzle.Model
{
    public class Definition : INotifyPropertyChanged
    {
        private string _phrase;
        public string Phrase
        {
            get { return _phrase; }
            set
            {
                _phrase = value;
                PropChangedHandler();
            }
        }

        private string _index;
        public string Index
        {
            get { return _index; }
            set
            {
                _index = value;
                PropChangedHandler();
            }
        }


        private string _fontWeight;

        public string FontWeight
        {
            get { return _fontWeight; }
            set
            {
                _fontWeight = value;
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
