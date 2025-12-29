using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class GetCoctelMolotovInteraction: Interaction
    {
        [SerializeField] private string itemOnHand = "BotellaDeAlcoholConPapel";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private AudioClip audioClip;
        
        [Inject] private readonly AudioPlayer _audioPlayer;
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        public override void Interact()
        {
            if (!Interactable) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddCoctelMolotov();
                _audioPlayer.PlaySFX(audioClip, 0.5f);
                _showDialogue.Start(dialogueCompleted);
                Disable();
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    _showDialogue.Start(dialogueWrongItem);
                }
                else
                {
                    _showDialogue.Start(dialogueNoItem);
                }
            }
        }
    }
}