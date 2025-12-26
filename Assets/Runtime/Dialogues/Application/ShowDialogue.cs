using Runtime.Dialogues.Domain;
using UnityEngine;

namespace Runtime.Application
{
    public static class ShowDialogue
    {
        private static Dialogue Displayer => _cacheDisplayer ??= GameObject.FindGameObjectWithTag("DialogueCanvas").GetComponent<Dialogue>();

        private static Dialogue _cacheDisplayer;
        public static void Start(DialogueData data)
        {
            CurrentDialogue.Start(data);
            Displayer.Display(CurrentDialogue.FirstLine);
        }

        public static void ShowNextLine()
        {
            if (CurrentDialogue.HasEnded)
            {
                Displayer.Hide();
            }
            else
            {
                Displayer.Display(CurrentDialogue.NextLine);
            }
            
        }
    }
}