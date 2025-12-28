using Runtime.Application;
using Runtime.Dialogues.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class ShowDialogueInteraction:Interaction
    {
        [SerializeField] private DialogueData _dialogue;
        
        [Inject] private readonly ShowDialogue _showDialogue;
        public override void Interact()
        {
            if(!Interactable) return;
            _showDialogue.Start(_dialogue);
        }
    }
}