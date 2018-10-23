using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using System.Threading.Tasks;
using CrossWordPuzzle.Game;
using System.Diagnostics;
using CrossWordPuzzle.ViewModel;
using System.ComponentModel;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CrossWordPuzzle
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        Board _board;
        BoardLayout _boardLayout = new BoardLayout();

        MainPageData _mainPageData = new MainPageData();

        private ObservableCollection<Words> _myStringList = new ObservableCollection<Words>()
        {
            new Words { Word="Hey" },
            new Words { Word="You" },
            new Words { Word="Are" },
            new Words { Word="Awesome" },


        };

        public ObservableCollection<Words> MyStringList
        {
            get
            {
                return _myStringList;
            }
            set
            {

                Debug.WriteLine("List Return Value: " + _myStringList);
                _myStringList = value;

            }

        }

        public MainPage()
        {
            this.InitializeComponent();

            //DataContext = this;

            _board = Board.Instance();
            _board.InitializeBoard();
            //_board.ForcePlaceWord();

            _boardLayout.PlaceAllWords();


            var convertedBoard2Arrays = _board.ConvertBoardTo2Arrays(_board.CrossWordboard);

            var mainPageData = new MainPageData();

            //mainPageData.Board = convertedBoard2Arrays;

            //var convertedBoardMultdimensionalArrays = _board.ConvertBoardToMultidimensionalArray(mainPageData.Board);

            _board.DisplayBoard(_board.CrossWordboard);
            //MainPageData.Board = mainPageData.ArrayMultidimensionalToIObservableCollection(_board.CrossWordboard);
        }


        private void Button_UpdateUI_Click(object sender, RoutedEventArgs e)
        {
            var mainPageData = new MainPageData();
            //MainPageData.Board = mainPageData.ArrayMultidimensionalToIObservableCollection(_board.CrossWordboard);
        }



        private void DisplayTupleList(List<Tuple<int, int>> tuppleList)
        {

            for (int i = 0; i < tuppleList.Count; i++)
            {

                Debug.WriteLine("TuppleList {0}, {1} ", tuppleList[i].Item1, tuppleList[i].Item2);

            }

        }


        private void ButtonPrintList_Click(object sender, RoutedEventArgs e)
        {
            _mainPageData.PrintBoard(_mainPageData.Board);
        }
    }
}
