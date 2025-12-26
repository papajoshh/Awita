using UnityEngine;

public class DialogueData : MonoBehaviour
{
    public string ID { get; private set; }
    public string[] dialogueLines {get; private set;}
    
    public DialogueData(string id, string[] lines)
    {
        ID = id;
        dialogueLines = lines;
    }
    
}
