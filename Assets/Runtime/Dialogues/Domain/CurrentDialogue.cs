namespace Runtime.Dialogues.Domain
{
    public static class CurrentDialogue
    {
        public static string FirstLine => _currentDialogue.FirstLine;
        
        public static string NextLine
        {
            get
            {
                _currentLineIndex++;
                return _currentLineIndex >= _currentDialogue.dialogueLines.Length ? null : 
                    _currentDialogue.dialogueLines[_currentLineIndex];
            }
        }
        
        public static bool HasEnded => _currentLineIndex >= _currentDialogue.dialogueLines.Length - 1;
        
        private static int _currentLineIndex = 0;
        private static DialogueData _currentDialogue;
        public static void Start(DialogueData data)
        {
            _currentDialogue = data;
            _currentLineIndex = 0;
        }
    }
}