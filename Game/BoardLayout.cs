using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using CrossWordPuzzle.Core;
using System.Diagnostics;
using CrossWordPuzzle.ViewModel;

namespace CrossWordPuzzle.Game
{
    public class BoardLayout
    {

        public List<PlacedWord> PlacedWords = new List<PlacedWord>();
        public List<string> Definitions = new List<string>();

        private int[] _wordSizes = 
        {
            10, 9, 8, 8, 7, 7, 7, 6, 6, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 3,
            9, 9, 8, 8, 7, 7, 7, 6, 6, 5, 5, 5, 4, 4, 4, 4, 3, 3, 3, 3, 3
        };

        private List<string> _usedWords = new List<string>();

        private int _wordsPlaced;
        private int _minWordsPlaced = 13;

        // Keeps trying to solve board until the _minWordsPlaced count is reached 
        public void StartPlaceAllWords()
        {
            PlacedWords.Clear();
            _wordsPlaced = 0;
            var solved = false;

            while (solved == false)
            {
                PlaceWords();

                if (_wordsPlaced >= _minWordsPlaced)
                {
                    solved = true;
                    break;
                }

                // Reset the board and start again if min word count in not met
                if (!solved)
                {
                    BoardCrossWord.Instance().ResetBoard();

                    _wordsPlaced = 0;
                    _usedWords = new List<string>();
                    solved = false;
                }
            }

            // Sort and find definition of placed words
            Definitions = PrepDefinitionLists();

            Debug.WriteLine("Words Placed " + _wordsPlaced);
        }

        // Sort and find definition of placed words
        public List<string> PrepDefinitionLists()
        {
            var sortedHorizontalPlacedWords = SortPlacedWords(PlacedWords, WordDirection.Horizontal);
            var sortedVerticalPlacedWords = SortPlacedWords(PlacedWords, WordDirection.Vertical);

            var HorizontalWordAndDefinitionList = new List<PlacedWord>();
            var VerticalWordAndDefinitionList = new List<PlacedWord>();

            HorizontalWordAndDefinitionList = FindDefinitions(sortedHorizontalPlacedWords);
            HorizontalWordAndDefinitionList.Insert(0, (new PlacedWord { Definition = "ACROSS" }));

            VerticalWordAndDefinitionList = FindDefinitions(sortedVerticalPlacedWords);
            VerticalWordAndDefinitionList.Insert (0 , (new PlacedWord { Definition = "DOWN" }));

            var joinedLists = HorizontalWordAndDefinitionList.Concat(VerticalWordAndDefinitionList).ToList();

            var definitions = joinedLists.Select(d => d.Definition).ToList();

            return definitions;
        }

        // Get new word from WordList Words
        public string RetrieveWord(List<string> usedWords, int letterCount)
        {

            var wordList = WordList.Instance().Words.ToArray();

            var wordArray = wordList.Where(w => w.Length == letterCount).
                Except(usedWords).
                ToArray();

            var random = new Random();

            var word = string.Empty;

            if (wordArray == null)
            {
                var opResults = new OperationResult() { Success = false };
                opResults.AddMessage("Ran out of words");

                return null;
            }

            if (wordArray.Length == 0)
            {
                var opResults = new OperationResult() { Success = false };
                opResults.AddMessage("No words with the desired lettercount");

                return null;
            }

            return wordArray[random.Next(0, wordArray.Length)]; ;

        }


        int _count;

        private void PlaceWords()
        {
            _count++;
            PlacedWords.Clear();
            // First Word
            var word = RetrieveWord(new List<string> { }, _wordSizes[0]);
           
            var random = new Random();
            var horizontalPos = random.Next(0, 1);
            var vertiacalPos = random.Next(4, 6);
            var randomStartPos = new Tuple<int, int>(horizontalPos, vertiacalPos);

            Debug.WriteLine("Deets word {0}, pos {1},{2}, count{3}", word, randomStartPos.Item1, randomStartPos.Item2, _count);

            // Place first word
            var placedWord = BoardCrossWord.Instance().PlaceWord(word, randomStartPos, WordDirection.Horizontal);
            if (placedWord != null)
            {
                PlacedWords.Add(placedWord);
            }
            else
            {
                Debug.WriteLine("IsNull");
            }
            
            _usedWords.Add(word);

            // Rest of board
            var direction = WordDirection.Vertical;

            // Sequence through word size array
            for (var i = 1; i < _wordSizes.Length; i++)
            {

                var wordAndPosList = FindWordAndPositions(_wordSizes[i], direction);

                if (wordAndPosList != null)
                {

                    var startPositions = new List<Tuple<int, int>>(wordAndPosList.StartPosList);

                    word = wordAndPosList.Word;

                    var usedCharsIndex = new List<int> { };

                    if (startPositions.Count > 0)
                    {

                        var whileCount = 0;

                        // Try start positions until word placement is found
                        while (whileCount < startPositions.Count)
                        {
                            // Randomly itterate through start positions
                            var listIndex = ReturnRandomNumberExcludingArrayInts(startPositions.Count, usedCharsIndex);
                            usedCharsIndex.Add(listIndex);

                            var startPos = startPositions[listIndex];

                            placedWord = BoardCrossWord.Instance().PlaceWord(word, startPos, direction);

                            if (placedWord != null)
                            {

                                _usedWords.Add(word);

                                if (direction == WordDirection.Horizontal)
                                {
                                    direction = WordDirection.Vertical;
                                }
                                else
                                {
                                    direction = WordDirection.Horizontal;
                                }

                                PlacedWords.Add(placedWord);
                                
                                _wordsPlaced++;

                                // Break from while if wordcount reached
                                if (_wordsPlaced >= _minWordsPlaced)
                                {
                                    break;
                                }

                                break;
                            }

                            whileCount ++;
                        }   

                    }

                }
                
            }

        }


        private WordAndStartPositions FindWordAndPositions(int wordLength, WordDirection direction)
        {
            var startPosAttempts = 0;
            var word = String.Empty;
            var usedWords = new List<string>(_usedWords);
            var starPosList = new List<Tuple<int, int>> {};
            var maxCounts = 25;


            // Attempt to find start positions
            while (startPosAttempts < maxCounts)
            {
                word = RetrieveWord(usedWords, wordLength);

                var wordCharCount = 0;

                // Attemps cycling through letters
                while (wordCharCount < word.Length)
                {

                    var posList = BoardCrossWord.Instance().ReturnWordStartPositions(word, wordCharCount, direction);
                    starPosList.AddRange(posList);

                    wordCharCount ++;
                }

                if (starPosList.Count > 0)
                {

                    break;
                }

                usedWords.Add(word);
               
                startPosAttempts ++;
            }

            // Return null of no position found
            if (starPosList.Count == 0)
            {
                Debug.WriteLine("____________failed to find startPos");
                return null;
            }

            var wordAndPosList = new WordAndStartPositions
            {
                StartPosList = new List<Tuple<int, int>>(starPosList),
                Word = word
            };


            return wordAndPosList;

        }

        // Returns a random number from a int List considering the used numbers
        public int ReturnRandomNumberExcludingArrayInts(int length, List<int> usedNumbers)
        {
            var sequenceArray = Enumerable.Range(0, length).ToArray();

            var availableInts = sequenceArray.Except(usedNumbers).ToArray();


            var random = new Random();

            var result = availableInts[random.Next(0, availableInts.Length)];
            
            return result;
        }

        public List<PlacedWord> SortPlacedWords(List<PlacedWord> placedWords, WordDirection direction)
        {
            
            var placedWordsSorted = new List<PlacedWord>();
            // Sorted Horizontal or vertical
            if (direction == WordDirection.Horizontal)
            {
                placedWordsSorted = placedWords.Where(w => w.Direction == direction)
                    .OrderBy(w => w.StartPos.Item2).ThenBy(w => w.StartPos.Item1).ToList();
            }
            if (direction == WordDirection.Vertical)
            {
                placedWordsSorted = placedWords.Where(w => w.Direction == direction)
                    .OrderBy(w => w.StartPos.Item1).ThenBy(w => w.StartPos.Item2).ToList();
            }

            // Sort in ascending
            return placedWordsSorted;
        }

        public List<PlacedWord> FindDefinitions (List<PlacedWord> placedWords)
        {
            var wordAndDefinitions = new List<PlacedWord>();

            for (var i = 0; i < placedWords.Count; i++)
            {
                var definitionArray = WordList.Instance().WordAndDefinitions.Where(d => d.Word == placedWords[i].Word).ToArray();

                if (definitionArray == null)
                {
                    return null;
                }
                var definition = definitionArray[0].Definition;

                wordAndDefinitions.Add(new PlacedWord
                {
                    Word = placedWords[i].Word,
                    StartPos = placedWords[i].StartPos,
                    Direction = placedWords[i].Direction,
                    DefinitionIndex = i + 1,
                    Definition = definition,
                });
            }

            return wordAndDefinitions;
        }

        // Generate board that holds the across and down definition numbers
        public Board GenerateBoardLocations(List<PlacedWord> placedWords)
        {
            var Board = new Board(
                BoardCrossWord.Instance().CrossWordboard.Width,
                BoardCrossWord.Instance().CrossWordboard.Height,
                BoardCrossWord.Instance().CrossWordboard.EmptyChar);

            for (var i = 0; i < placedWords.Count; i++)
            {
                if (placedWords[i].Direction == WordDirection.Horizontal)
                {
                    //Board.Layout[]
                } else
                {

                }
            }


            return Board;
        } 

    }

}
