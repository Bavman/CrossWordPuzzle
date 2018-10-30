using CrossWordPuzzle.Model;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

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
            MainPage.ReceiveStatusEvent += WriteStatus;
        }

        private void WriteStatus(object status, StatusReceiverEventArgs e)
        {
            Debug.WriteLine("EventDelegate "+status.ToString());
        }


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


        public ObservableCollection<ObservableCollection<Cell>> GameBoardLettersToDisplayBoard(char[,] board)
        {
            var returnBoard = new ObservableCollection<ObservableCollection<Cell>>();

            for (var i = 0; i < board.GetLength(0); i++)
            {

                var newRow = new ObservableCollection<Cell>();

                for (var j = 0; j < board.GetLength(1); j++)
                {
                    var cell = new Cell { LetterOut = board[i, j] };

                    if (board[i, j] == ' ')
                    {
                        cell.BackgroundColour = new SolidColorBrush(Color.FromArgb(255, 120, 165, 240));
                        cell.ReadOnly = true;
                    }
                    else
                    {
                        cell.BackgroundColour = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
                        cell.ReadOnly = false;
                    }

                    newRow.Add(cell);
                }

                returnBoard.Add(newRow);
            }

            return returnBoard;

        }


        public void PrintBoard(ObservableCollection<ObservableCollection<char>> board)
        {
            for (var i = 0; i < board.Count; i++)
            {

                var row = String.Empty;

                for (var j = 0; j < board[i].Count; j++)
                {
                    row += board[i][j]+" ";
                }
                Debug.WriteLine(row);
            }
        }


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

        public void PrintList(ObservableCollection<Cell> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                Debug.WriteLine(list[i].LetterOut);
            }
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
