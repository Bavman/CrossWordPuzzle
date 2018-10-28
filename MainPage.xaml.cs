using CrossWordPuzzle.Game;
using CrossWordPuzzle.Model;
using CrossWordPuzzle.ViewModel;
using System.Collections.Generic;
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


            for (var i = 0; i < _boardLayout.Definitions.Count; i++)
            {

                if (_boardLayout.Definitions[i] == "ACROSS" || _boardLayout.Definitions[i] == "DOWN")
                {
                    _mainPageData.Definitions.Add(new Definition { Phrase = _boardLayout.Definitions[i], FontWeight = "Bold"});
                }
                else
                {
                    _mainPageData.Definitions.Add(new Definition { Phrase = _boardLayout.Definitions[i], FontWeight = "Normal" });
                }
                
            }
            var TextBoxList = new List<TextBox>();

            //FindChildren<TextBox>(TextBoxList, this.CrossWordItemsControl);
            //Debug.WriteLine(TextBoxList.Count);
            //Debug.WriteLine(TextBoxList[0].Name);
            //var test = VisualTreeHelper.GetChildrenCount(this.CrossWordItemsControl);
            //Debug.WriteLine("t "+test);
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
    }
}
