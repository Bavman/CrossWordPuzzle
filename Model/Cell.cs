using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media;

namespace CrossWordPuzzle.Model
{
    public class Cell : INotifyPropertyChanged
    {
        private char _letter;

        public char Letter
        {
            get { return _letter; }

            set
            {
                _letter = value;

                PropChangedHandler();
            }
        }

        private SolidColorBrush _colour;

        public SolidColorBrush Colour
        {
            get { return _colour; }

            set
            {
                _colour = value;
                PropChangedHandler();
            }

        }

        private bool _readOnly;

        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
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
