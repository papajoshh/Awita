using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class RadioCassetteInteraction : Interaction
    {
        [SerializeField] private string itemOnHand = "Cassete";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private AudioClip _audioClip;

        [Inject] private readonly Inventory _inventory;
        [Inject] private readonly AudioPlayer _audioPlayer;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        public bool IsMusicPlaying { get; private set; }
        public override void Interact()
        {
            if (!Interactable) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _handleInventory.RemoveItemOnHand();
                _audioPlayer.PlayMusic(_audioClip, 0.2f);
                IsMusicPlaying = true;
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
