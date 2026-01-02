using System;
using Runtime.Dialogues.Domain;
using Random = UnityEngine.Random;

namespace Runtime.Domain
{
    public class Child
    {
        public bool FirstLevelHidrationCompleted => levelOfHidration >= 1;
        public bool SecondLevelHidrationCompleted => levelOfHidration >= 4;
        public bool ThirdLevelHidrationCompleted => levelOfHidration >= 12;
        private int levelOfHidration;
        private DialogueData[] _wrongDialogues;
        private DialogueData[] _rightDialogues;
        private DialogueData _alcoholDialogue;
        private DialogueData _iceDialogue;
        
        public event Action OnHidrate;
        public static Child NewBorn(DialogueData[] wrongDialogues, DialogueData[] rightDialogues, DialogueData alcoholDialogue, DialogueData iceDialogue)
        {
            return new Child()
            {
                levelOfHidration = 0,
                _wrongDialogues = wrongDialogues,
                _rightDialogues = rightDialogues,
                _alcoholDialogue = alcoholDialogue,
                _iceDialogue = iceDialogue
            };
        }

        public void Hidrate()
        {
            levelOfHidration++;
            OnHidrate?.Invoke();
        }
        
        public DialogueData GetRandomWrongPhrase()
        {
            return _wrongDialogues[Random.Range(0, _wrongDialogues.Length)];
        }

        public DialogueData GetDialogueAlcohol()
        {
            return _alcoholDialogue;
        }

        public DialogueData GetDialogueIce()
        {
            return _iceDialogue;
        }

        public DialogueData GetPhraseOfHidratation()
        {
            return _rightDialogues[levelOfHidration];
        }
    }
}