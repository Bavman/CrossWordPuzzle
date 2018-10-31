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

        List<string> _foundWords = new List<string>();

        BoardLayout _boardLayout = new BoardLayout();
        MainPageData _mainPageData = new MainPageData();
        SolveBoard _boardCheckWords = new SolveBoard();

        public MainPage()
        {
            this.InitializeComponent();

            //LetterCell.AddHandler(TappedEvent, new TappedEventHandler(LetterCell_Tapped), true);
            
            BoardCrossWord.Instance().InitializeBoard(12, 12, ' ');

            SetupCrosswordPuzzle();
        }


        private void SetupCrosswordPuzzle()
        {

            _boardLayout.StartPlaceAllWords();

            BoardCrossWord.Instance().DisplayBoard(BoardCrossWord.Instance().CrossWordboard.Layout);

            _mainPageData.DisplayBoard = _mainPageData.GameBoardLettersToDisplayBoard(BoardCrossWord.Instance().CrossWordboard.Layout);

            _mainPageData.Definitions = new ObservableCollection<Definition>(_boardLayout.Definitions);

            _boardLayout.AssignDefinitionLocations(_boardLayout.SortedPlacedWords, _mainPageData.DisplayBoard);
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

        private void ButtonPrintList_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Print Board");
            _mainPageData.PrintBoard(_mainPageData.DisplayBoard);
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

        private void LetterCell_TextChanged(object sender, TextChangedEventArgs e)
        {
            var changePos = _boardCheckWords.GetUpdatePosition(_mainPageData.DisplayBoard, BoardCrossWord.Instance().CrossWordboardCheck);
            _boardCheckWords.CheckWord(_mainPageData.DisplayBoard, _boardLayout.PlacedWords, _foundWords);
        }


        private void OnReceiveStatus()
        {
            if (ReceiveStatusEvent != null)
            {
                ReceiveStatusEvent(this, new StatusReceiverEventArgs());
            }
        }


        private void ButtonRegenBoard_Click(object sender, RoutedEventArgs e)
        {
            _foundWords.Clear();
            BoardCrossWord.Instance().ResetBoard(BoardCrossWord.Instance().CrossWordboard, ' ');
            //BoardCrossWord.Instance().ResetBoard(_mainPageData.DisplayBoard, '\0');
            SetupCrosswordPuzzle();
        }
    }
}
