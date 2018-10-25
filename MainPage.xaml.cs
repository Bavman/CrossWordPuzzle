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

        BoardLayout _boardLayout = new BoardLayout();
        MainPageData _mainPageData = new MainPageData();


        public MainPage()
        {
            this.InitializeComponent();

            Board.Instance().InitializeBoard();

            _boardLayout.StartPlaceAllWords();

            Board.Instance().DisplayBoard(Board.Instance().CrossWordboard);
            _mainPageData.DisplayBoard = _mainPageData.GameBoardLettersToDisplayBoard(Board.Instance().CrossWordboard);

            for (var i = 0; i < _boardLayout.Definitions.Count; i++)
            {
                _mainPageData.Definitions.Add( new Definition { Phrase = _boardLayout.Definitions[i] });
            }

        }



        private void ButtonPrintList_Click(object sender, RoutedEventArgs e)
        {
            _mainPageData.PrintBoard(_mainPageData.DisplayBoard);
        }

    }
}
