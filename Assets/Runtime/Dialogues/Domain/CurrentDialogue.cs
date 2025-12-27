namespace Runtime.Dialogues.Domain
{
    public class CurrentDialogue
    {
        public string FirstLine => _currentDialogue.FirstLine;
        
        public string NextLine
        {
            get
            {
                _currentLineIndex++;
                return _currentLineIndex >= _currentDialogue.dialogueLines.Length ? null : 
                    _currentDialogue.dialogueLines[_currentLineIndex];
            }
        }
        
        public bool HasEnded => _currentLineIndex >= _currentDialogue.dialogueLines.Length - 1;
        
        private int _currentLineIndex = 0;
        private DialogueData _currentDialogue;
        public bool Hid { get; private set; }

        public CurrentDialogue()
        {
            Hid = true;
        }
        public void Start(DialogueData data)
        {
            if (!Hid) return;
            _currentDialogue = data;
            _currentLineIndex = 0;
            Hid = false;
        }

        public void End()
        {
            Hid = true;
        }
    }
}