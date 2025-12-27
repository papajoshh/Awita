using UnityEngine;

namespace Runtime.ExtraInteraction.Domain
{
    [CreateAssetMenu(fileName = "ExtraInteractionPuzzle", menuName = "ExtraInteractionPuzzle", order = 0)]
    public class ExtraInteractionPuzzle: ScriptableObject
    {
        public string PuzzleName;
        public int PuzzleIndex;
    }
}