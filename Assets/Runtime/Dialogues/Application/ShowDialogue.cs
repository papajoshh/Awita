using Runtime.Dialogues.Domain;

namespace Runtime.Application
{
    public class ShowDialogue
    {
        private readonly Dialogue _displayer;
        private readonly CurrentDialogue _currentDialogue;

        public ShowDialogue(Dialogue displayer, CurrentDialogue currentDialogue)
        {
            _displayer = displayer;
            _currentDialogue = currentDialogue;
        }
        public void Start(DialogueData data)
        {
            _currentDialogue.Start(data);
            _displayer.Display(_currentDialogue.FirstLine);
        }

        public void ShowNextLine()
        {
            if (_currentDialogue.HasEnded)
            {
                _displayer.Hide();
                _currentDialogue.End();
            }
            else
            {
                _displayer.Display(_currentDialogue.NextLine);
            }
            
        }
    }
}