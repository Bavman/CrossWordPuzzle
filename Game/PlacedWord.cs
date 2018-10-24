using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossWordPuzzle.Game
{
    public class PlacedWord
    {
        public Tuple<int, int> StartPos;
        public string Word;
        public WordDirection Direction;
        public string Definition;
    }
}
