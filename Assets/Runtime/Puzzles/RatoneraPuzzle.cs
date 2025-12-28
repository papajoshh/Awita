using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Infrastructure
{
    public class RatoneraPuzzle: Interaction
    {
        [SerializeField] private string itemOnHand = "Quesito";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private SpriteRenderer ratoneraInRoomRenderer;
        [SerializeField] private Sprite ratoneraOpenInRoomSprite;
        [SerializeField] private Sprite ratoneraClosedInRoomSprite;
        [SerializeField] private Sprite ratoneraOpenSprite;
        [SerializeField] private Sprite ratoneraClosedSprite;
        [SerializeField] private GameObject pincitas;
        
        [Inject] private readonly Ratonera _ratonera;
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        protected override void Awake()
        {
            base.Awake();
            if (_ratonera.IsOpen)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        public override void Interact()
        {
            if (!Interactable) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _handleInventory.RemoveItemOnHand();
                _showDialogue.Start(dialogueCompleted);
                Open();
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

        private void Open()
        {
            backgroundImage.sprite = ratoneraOpenSprite;
            ratoneraInRoomRenderer.sprite = ratoneraOpenInRoomSprite;
            pincitas.SetActive(true);
        }

        private void Close()
        {
            backgroundImage.sprite = ratoneraClosedSprite;
            ratoneraInRoomRenderer.sprite = ratoneraClosedInRoomSprite;
            pincitas.SetActive(false);
        }
    }
}