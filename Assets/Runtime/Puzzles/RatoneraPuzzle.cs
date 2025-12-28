using DG.Tweening;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] private Image backgroundCloseImage;
        [SerializeField] private SpriteRenderer ratoneraInRoomClosedRenderer;
        [SerializeField] private SpriteRenderer ratoneraInRoomOpenedRenderer;
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
            backgroundCloseImage.DOColor(Color.clear, 0.75f);
            ratoneraInRoomClosedRenderer.DOColor(Color.clear, 0.75f);
            ratoneraInRoomOpenedRenderer.DOColor(Color.white, 0.75f);
            pincitas.SetActive(true);
        }

        private void Close()
        {
            backgroundCloseImage.color = Color.white;
            ratoneraInRoomClosedRenderer.color = Color.white;
            ratoneraInRoomOpenedRenderer.color = Color.clear;
            ;
            pincitas.SetActive(false);
        }
    }
}