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
        [SerializeField] private SpriteRenderer _ghostRenderer;
        [SerializeField] private Sprite _happyGhost;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private GameObject animation;

        [Inject] private readonly Inventory _inventory;
        [Inject] private readonly AudioPlayer _audioPlayer;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        public bool IsMusicPlaying { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            animation.SetActive(false);
        }
        public override void Interact()
        {
            if (!Interactable) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _handleInventory.RemoveItemOnHand();
                _audioPlayer.PlayMusic(_audioClip, 0.2f);
                IsMusicPlaying = true;
                _ghostRenderer.sprite = _happyGhost;
                animation.SetActive(true);
                Disable();
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    _showDialogue.Start(dialogueWrongItem);
                    _handleInventory.DeselectItem();
                }
                else
                {
                    _showDialogue.Start(dialogueNoItem);
                }
            }
        }
    }
}
