using System.Collections.Generic;

namespace Runtime.ExtraInteraction.Domain
{
    public class ExtraInteractionPuzzleCatalogue
    {
        Dictionary<string, int> puzzles = new Dictionary<string, int>();
        
        public ExtraInteractionPuzzleCatalogue(List<ExtraInteractionPuzzle> puzzleList)
        {
            foreach (var puzzle in puzzleList)
            {
                puzzles.Add(puzzle.PuzzleName, puzzle.PuzzleIndex);
            }
        }

        public int GetPuzzleIndexById(string id) => puzzles[id];
    }
}