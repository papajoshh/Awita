using Runtime.ExtraInteraction.Domain;

namespace Runtime.ExtraInteraction.Application
{
    public class ShowPopupInteraction
    {
        private readonly ExtraInteractionPopup _popup;
        private readonly ExtraInteractionPuzzleCatalogue _puzzles;
        public ShowPopupInteraction(ExtraInteractionPopup popup, ExtraInteractionPuzzleCatalogue puzzles)
        {
            _popup = popup;
            _puzzles = puzzles;
        }
        
        public void Execute(string id)
        {
            var index = _puzzles.GetPuzzleIndexById(id);
            _popup.Show(index);
        }
    }
}