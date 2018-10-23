using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using System.Diagnostics;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;
using CrossWordPuzzle.Game;
using Windows.UI;

namespace CrossWordPuzzle.ViewModel
{
    public class MainPageData
    {

        public char[][] _testBoard = new char[][]
        {
            new char[] { ' ', 'N', 'O', 'T', 'E', ' ' },
            new char[] { 'X', 'I', ' ', ' ', 'G', ' ' },
            new char[] { ' ', 'C', ' ', 'E', 'G', 'O' },
            new char[] { ' ', 'E', ' ', ' ', ' ', ' ' }
        };

        private ObservableCollection<ObservableCollection<Cell>> _displayBoard = new ObservableCollection<ObservableCollection<Cell>>
        {
            new ObservableCollection<Cell> { new Cell { Letter = ' ' }, new Cell { Letter = 'N' }, new Cell { Letter = 'O' }, new Cell { Letter = 'T' }, new Cell { Letter = 'E' }, new Cell { Letter = ' ' } },
            new ObservableCollection<Cell> { new Cell { Letter = 'X' }, new Cell { Letter = 'I' }, new Cell { Letter = ' ' }, new Cell { Letter = ' ' }, new Cell { Letter = 'G' }, new Cell { Letter = ' ' } },
            new ObservableCollection<Cell> { new Cell { Letter = ' ' }, new Cell { Letter = 'C' }, new Cell { Letter = ' ' }, new Cell { Letter = 'E' }, new Cell { Letter = 'G' }, new Cell { Letter = 'O' } },
            new ObservableCollection<Cell> { new Cell { Letter = ' ' }, new Cell { Letter = 'E' }, new Cell { Letter = ' ' }, new Cell { Letter = ' ' }, new Cell { Letter = ' ' }, new Cell { Letter = ' ' } },
        };

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


        public ObservableCollection<ObservableCollection<Cell>> GameBoardLettersToDisplayBoard(char[,] board)
        {
            var returnBoard = new ObservableCollection<ObservableCollection<Cell>>();

            for (var i = 0; i < board.GetLength(0); i++)
            {

                var newRow = new ObservableCollection<Cell>();

                for (var j = 0; j < board.GetLength(1); j++)
                {
                    var cell = new Cell { Letter = board[i, j] };

                    if (board[i, j] == Board.Instance()._emptyChar)
                    {
                        cell.Colour = new SolidColorBrush(Color.FromArgb(255, 120, 165, 240));
                        cell.ReadOnly = true;
                    }
                    else
                    {
                        cell.Colour = new SolidColorBrush(Color.FromArgb(255, 240, 240, 240));
                        cell.ReadOnly = false;
                    }

                    newRow.Add(cell);
                }

                returnBoard.Add(newRow);
            }

            return returnBoard;

        }

        private ObservableCollection<Cell> _testList = new ObservableCollection<Cell>
        {
            new Cell { Letter = ' ' },
            new Cell { Letter = 'N' },
            new Cell { Letter = 'O' },
            new Cell { Letter = 'T' },
            new Cell { Letter = 'E' },
            new Cell { Letter = ' ' }
        };

        public ObservableCollection<Cell> MyTestList
        {
            get { return _testList; }

            set { _testList = value; }

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
                    row += board[i][j].Letter + " ";
                }
                Debug.WriteLine(row);
            }
        }

        public void PrintList(ObservableCollection<Cell> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                Debug.WriteLine(list[i].Letter);
            }
        }

        public void ModifyList()
        {
            MyTestList[0].Letter = 'N';
            MyTestList[1].Letter = 'E';
            MyTestList[2].Letter = 'W';
            MyTestList[3].Letter = 'L';
            MyTestList[4].Letter = 'I';
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

    public class Cell : INotifyPropertyChanged
    {
        private char _letter;

        public char Letter
        {
            get { return _letter; }

            set
            {
                _letter = value;

                PropChangedHandler("Letter");
            }
        }

        private SolidColorBrush _colour;

        public SolidColorBrush Colour
        {
            get { return _colour; }

            set
            {
                _colour = value;
                PropChangedHandler("Colour");
            }

        }

        private bool _readOnly;

        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                _readOnly = value;
                PropChangedHandler("ReadOnly");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void PropChangedHandler (string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }

    public class Words
    {
        public string Word { get; set; }

    }

    public class BoardRow
    {
        //public List<char> row { get; set; } = new List<char>();
        public ObservableCollection<char> Row { get; set; } = new ObservableCollection<char> { };

    }
}
