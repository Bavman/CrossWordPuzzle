using CrossWordPuzzle.Game;
using CrossWordPuzzle.Model;
using CrossWordPuzzle.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CrossWordPuzzle
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        BoardLayout _boardLayout = new BoardLayout();
        MainPageData _mainPageData = new MainPageData();


        public MainPage()
        {
            this.InitializeComponent();

            //LetterCell.AddHandler(TappedEvent, new TappedEventHandler(LetterCell_Tapped), true);
            
            BoardCrossWord.Instance().InitializeBoard(12, 12, ' ');

            _boardLayout.StartPlaceAllWords();

            BoardCrossWord.Instance().DisplayBoard(BoardCrossWord.Instance().CrossWordboard.Layout);

            _mainPageData.DisplayBoard = _mainPageData.GameBoardLettersToDisplayBoard(BoardCrossWord.Instance().CrossWordboard.Layout);

            _mainPageData.Definitions = new ObservableCollection<Definition>(_boardLayout.Definitions);

            _boardLayout.AssignDefinitionLocations(_boardLayout.SortedPlacedWords, _mainPageData.DisplayBoard);
            //FindChildren<TextBox>(TextBoxList, this.CrossWordItemsControl);
            //Debug.WriteLine(TextBoxList.Count);
            //Debug.WriteLine(TextBoxList[0].Name);
            //var test = VisualTreeHelper.GetChildrenCount(this.CrossWordItemsControl);
            //Debug.WriteLine("t "+test);
        }


        private void SolveBoard (ObservableCollection<ObservableCollection<Cell>> board)
        {
            for (var i = 0; i < board.Count; i++)
            {
                for (var j = 0; j < board[i].Count; j++)
                {
                    board[i][j].LetterIn = board[i][j].LetterOut;
                }
            }
        }

        internal static void FindChildren<T>(List<T> results, DependencyObject startNode)
          where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);
            Debug.WriteLine("count " + count);
            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType().GetTypeInfo().IsSubclassOf(typeof(T))))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }
                FindChildren<T>(results, current);
            }
        }


        private void ButtonPrintList_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Print Board");
            _mainPageData.PrintBoard(_mainPageData.DisplayBoard);
        }

        private void LetterCell_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Debug.WriteLine(e.OriginalSource);
            Debug.WriteLine("TEST");
        }

        private void LetterCell_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var originalSource = e.Pointer;
            Debug.WriteLine(originalSource);

        }

        private void ButtonSolve_Click(object sender, RoutedEventArgs e)
        {
            SolveBoard(_mainPageData.DisplayBoard);
        }

        private void ButtonTestGetWords_Click(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < 50; i++)
            {
                var random = new Random();
                var word = _boardLayout.RetrieveWord(_testUsedWords, 8);
                _testUsedWords.Add(word);
                Debug.WriteLine("Words Placed " + word);
            }
        }

        List<string> _testUsedWords = new List<string>();
    }
}
