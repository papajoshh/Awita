using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class ShowDialogueWithItem:Interaction
    {
        [SerializeField] private string itemOnHand = "EmptyGlass";
        [SerializeField] private DialogueData _dialogue;
        
        [Inject] private readonly ShowDialogue _showDialogue;
        [Inject] private Inventory _handleInventory;
        public override void Interact()
        {
            if(!Interactable) return;
            if (!_handleInventory.HasitemOnHand(itemOnHand)) return;
            _showDialogue.Start(_dialogue);
        }
    }
}