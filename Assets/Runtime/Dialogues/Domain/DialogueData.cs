using UnityEngine;

namespace Runtime.Dialogues.Domain
{
    [CreateAssetMenu(fileName = "DialogueData", menuName = "DialogueData", order = 1)]
    public class DialogueData : ScriptableObject
    {
        public string ID => _id;
        public string[] dialogueLines => _dialogueLines;
    
        [SerializeField] private string _id;
        [SerializeField] private string[] _dialogueLines;
        public string FirstLine => dialogueLines[0];
    
    }
}
