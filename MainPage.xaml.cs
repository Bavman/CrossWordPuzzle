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
        public static event StatusReceiverHandler ReceiveStatusEvent;

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

            //ContentPresenter contentPresenter = FindVisualChild<ContentPresenter>(this.Definitions);

            //Debug.WriteLine(contentPresenter.Dispatcher);
            //DataTemplate yourDataTemplate = contentPresenter.ContentTemplate;

            // Debug.WriteLine(userControl);

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

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
        where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
            where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
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
            var random = new Random();
            for (var i = 0; i < 50; i++)
            {
                
                var randomInt = random.Next(0, 50);
                Debug.WriteLine("Random Int " + randomInt);
            }
        }

        List<string> _testUsedWords = new List<string>();

        private void LetterCell_TextChanged(object sender, TextChangedEventArgs e)
        {
            var changePos = GetUpdatePosition(_mainPageData.DisplayBoard, BoardCrossWord.Instance().CrossWordboardCheck);
            Debug.WriteLine("changedPos " + changePos);
        }


        private void OnReceiveStatus()
        {
            if (ReceiveStatusEvent != null)
            {
                ReceiveStatusEvent(this, new StatusReceiverEventArgs());
            }
        }


        private Tuple<int,int> GetUpdatePosition(ObservableCollection<ObservableCollection<Cell>> displayBoardIn, Board compareBoard)
        {
            var updatePos = new Tuple<int, int>(0,0);


            for (var i = 0; i < displayBoardIn.Count; i++)
            {
                for (var j = 0; j < displayBoardIn[i].Count; j++)
                {
                    if (displayBoardIn[i][j].LetterIn != compareBoard.Layout[i, j])
                    {
                        updatePos = new Tuple<int,int>(j, i);
                        compareBoard.Layout[i, j] = displayBoardIn[i][j].LetterIn;
                    }
                }
            }

            return updatePos;
        }
    }
}
