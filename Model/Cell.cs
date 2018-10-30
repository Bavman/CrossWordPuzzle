using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media;
using CrossWordPuzzle.Game;
using CrossWordPuzzle.ViewModel;

namespace CrossWordPuzzle.Model
{

    public class Cell : INotifyPropertyChanged
    {
        private char _letterOut;
        public char LetterOut
        {
            get { return _letterOut; }

            set
            {
                _letterOut = value;

                PropChangedHandler();
            }
        }

        private char _letterIn;
        public char LetterIn
        {
            get { return _letterIn; }
            set
            {
                _letterIn = value;
                
                PropChangedHandler();
            }
        }

        private string _definitionLocation;
        public string DefinitionLocation
        {
            get { return _definitionLocation; }

            set
            {
                _definitionLocation = value;

                PropChangedHandler();
            }
        }

        private SolidColorBrush _backgroundColour;
        public SolidColorBrush BackgroundColour
        {
            get { return _backgroundColour; }

            set
            {
                _backgroundColour = value;
                PropChangedHandler();
            }

        }

        private SolidColorBrush _foregroundColour;
        public SolidColorBrush ForegroundColour
        {
            get { return _foregroundColour; }

            set
            {
                _foregroundColour = value;
                PropChangedHandler();
            }

        }

        private bool _isReadOnly;
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set
            {
                _isReadOnly = value;
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
