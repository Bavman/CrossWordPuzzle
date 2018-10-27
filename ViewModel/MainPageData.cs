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
using CrossWordPuzzle.Model;
using Windows.UI;
using System.Runtime.CompilerServices;

namespace CrossWordPuzzle.ViewModel
{
    public class MainPageData
    {

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
                    var cell = new Cell { Letter = board[i, j] };

                    if (board[i, j] == ' ')
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
