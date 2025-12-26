using TMPro;
using UnityEngine;

namespace Runtime.Dialogues.Infrastructure
{
    public class DialogueCanvas: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI lineText;
        public void Display(DialogueData data)
        {
            foreach (var line in data.dialogueLines)
            {
                Debug.Log(line);
            }
        }
    }
}