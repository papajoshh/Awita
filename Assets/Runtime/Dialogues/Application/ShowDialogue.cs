namespace Runtime.Application
{
    public class ShowDialogue
    {
        public void Run(DialogueData dialogue)
        {
            foreach (var line in dialogue.dialogueLines)
            {
                UnityEngine.Debug.Log(line);
            }
        }
    }
}