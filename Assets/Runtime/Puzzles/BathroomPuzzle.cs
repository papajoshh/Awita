using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class BathroomPuzzle:Interaction
    {
        [SerializeField] private string itemOnHand = "Hielo";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        
        [SerializeField] private string itemOnHandTiGetWater = "EmptyGlass";
        [SerializeField] private DialogueData dialogueWaterCompleted;
        [SerializeField] private DialogueData dialogueWaterNoItem;
        [SerializeField] private DialogueData dialogueWaterWrongItem;
        
        [SerializeField] private SpriteRenderer bathroomRenderer;
        [SerializeField] private Sprite initialBathroomSprite;
        [SerializeField] private Sprite iceBathroomSprite;
        [SerializeField] private Sprite emptyBathroomSprite;
        
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        private bool iceOnBathroom = false;

        protected override void Awake()
        {
            bathroomRenderer.sprite = initialBathroomSprite;
        }
        public override void Interact()
        {
            if (!Interactable) return;
            PutIceOnBathroom();
            GetWater();
        }

        private void PutIceOnBathroom()
        {
            if (iceOnBathroom) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                PutIces();
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

        private void GetWater()
        {
            if (!iceOnBathroom) return;
            if (_inventory.HasitemOnHand(itemOnHandTiGetWater))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddItem("GlassFullOfWater");
                _showDialogue.Start(dialogueWaterCompleted);
                bathroomRenderer.sprite = emptyBathroomSprite;
                Disable();
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    _showDialogue.Start(dialogueWaterWrongItem);
                }
                else
                {
                    _showDialogue.Start(dialogueWaterNoItem);
                }
            }
            
        }

        private void PutIces()
        {
            bathroomRenderer.sprite = iceBathroomSprite;

            if (iceOnBathroom) return;
            _handleInventory.RemoveItemOnHand();
            iceOnBathroom = true;
        }
    }
}