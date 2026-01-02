using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class ShowDialogueInteraction:Interaction
    {
        [SerializeField] private DialogueData _dialogue;
        [SerializeField] private DialogueData _dialogueWithGlass;
        [SerializeField] private string itemOnHandToGetWater = "EmptyGlass";

        [Inject] private readonly ShowDialogue _showDialogue;
        [Inject] private readonly Inventory _inventory;

        public override void Interact()
        {
            if(!Interactable) return;

            if (_inventory.HasitemOnHand(itemOnHandToGetWater))
            {
                _showDialogue.Start(_dialogueWithGlass);
            }
            else
            {
                _showDialogue.Start(_dialogue);
            }
           
        }
    }
}