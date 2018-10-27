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
using System.Reflection;
using CrossWordPuzzle.Model;
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
            
            Board.Instance().InitializeBoard();

            _boardLayout.StartPlaceAllWords();

            Board.Instance().DisplayBoard(Board.Instance().CrossWordboard);
            _mainPageData.DisplayBoard = _mainPageData.GameBoardLettersToDisplayBoard(Board.Instance().CrossWordboard);

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
