﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Media;

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
