using CrossWordPuzzle.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using CrossWordPuzzle.Game;
using System.Collections.Generic;

namespace CrossWordPuzzle.ViewModel
{
    public delegate void StatusReceiverHandler(object sender, StatusReceiverEventArgs e);

    public class StatusReceiverEventArgs : EventArgs
    {
        public StatusReceiverEventArgs()
        {

        }

    }

    public class MainPageData
    {
        public MainPageData()
        {
            // MainPage.ReceiveStatusEvent += WriteStatus;
        }

        public Score DisplayScore = new Score();

        private ObservableCollection<ObservableCollection<Cell>> _displayBoard;

        public ObservableCollection<ObservableCollection<Cell>> DisplayBoard
        {
            get
            {
                return _displayBoard;
            }
            set
            {
                _displayBoard = value;
            }
        }

        private ObservableCollection<Definition> _definitions = new ObservableCollection<Definition>();

        public ObservableCollection<Definition> Definitions
        {
            get { return _definitions; }
            set { _definitions = value; }
        }

        // Initializes definition list
        public void AssignDefinitionList (IEnumerable<Definition> definitions)
        {

            Definitions.Clear();

            var definitionArray = definitions.ToArray();

            for (var i = 0; i < definitionArray.Length; i++)
            {
                Definitions.Add(definitionArray[i]);
            }

        }

        // Initializes display board
        public void InitializeDisplayBoard (int width, int height)
        {
            var board = new ObservableCollection<ObservableCollection<Cell>>();

            for (var i = 0; i < height; i++)
            {

                var newRow = new ObservableCollection<Cell>();

                for (var j = 0; j < width; j++)
                {
                    var cell = new Cell { };

                    newRow.Add(cell);
                }

                board.Add(newRow);
            }

            DisplayBoard = board;
        }

        // Assigs generated words from 
        public void AssignCrosswordDisplayBoard (Board board)
        {

            for (var i = 0; i < board.Layout.GetLength(0); i++)
            {

                for (var j = 0; j < board.Layout.GetLength(1); j++)
                {
                    DisplayBoard[i][j].LetterOut = board.Layout[i, j];
                    DisplayBoard[i][j].LetterIn = '\0';
                    DisplayBoard[i][j].DefinitionLocation = String.Empty;
                    DisplayBoard[i][j].FontWeight = "Normal";
                    

                    if (board.Layout[i, j] == ' ')
                    {
                        DisplayBoard[i][j].BackgroundColour = new SolidColorBrush(Color.FromArgb(255, 120, 165, 240));
                        DisplayBoard[i][j].IsReadOnly = true;
                    }
                    else
                    {
                        DisplayBoard[i][j].BackgroundColour = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
                        DisplayBoard[i][j].IsReadOnly = false;
                    }

                }

            }

        }

        // Print Display Board to console
        public void PrintBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            for (var i = 0; i < board.Count; i++)
            {
                var row = String.Empty;

                for (var j = 0; j < board[i].Count; j++)
                {
                    row += board[i][j].LetterIn + " ";
                }
                Debug.WriteLine(row);
            }
        }

        // Print list to console
        public void PrintList(ObservableCollection<Cell> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                Debug.WriteLine(list[i].LetterOut);
            }
        }

        // Test method for events
        private void WriteStatus(object status, StatusReceiverEventArgs e)
        {
            Debug.WriteLine("EventDelegate " + status.ToString());
        }
    }

    public class ArrayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string val = value.ToString();
            var charArray = val.ToCharArray();

            return charArray.First();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }


    
}
