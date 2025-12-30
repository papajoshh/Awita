using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class TermoPuzzle:Interaction
    {
        [SerializeField] private string itemOnHand = "Cortapizzas";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        
        [SerializeField] private string itemOnHandTiGetWater = "EmptyGlass";
        [SerializeField] private DialogueData dialogueWaterCompleted;
        [SerializeField] private DialogueData dialogueWaterNoItem;
        [SerializeField] private DialogueData dialogueWaterWrongItem;

        [SerializeField] private SpriteRenderer pizzaRenderer;
        [SerializeField] private SpriteRenderer boilerRenderer;
        [SerializeField] private Sprite fullBoilerSprite;
        [SerializeField] private Sprite emptyBoilerSprite;
        
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        //SFX
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioClip _audioClip_getWater;
        [Inject] private readonly AudioPlayer _audioPlayer;

        private bool pizzasWasRemoved = false;

        protected override void Awake()
        {
            boilerRenderer.sprite = fullBoilerSprite;
            pizzaRenderer.enabled = true;
        }
        public override void Interact()
        {
            if (!Interactable) return;
            GetWater();
            CutPizza();
            
        }

        private void CutPizza()
        {
            if (pizzasWasRemoved) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                RemovePizza();
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    _handleInventory.DeselectItem();
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
            if (!pizzasWasRemoved) return;
            if (_inventory.HasitemOnHand(itemOnHandTiGetWater))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddGlassOfWater();
                _audioPlayer.PlaySFX(_audioClip_getWater, 0.2f);
                _showDialogue.Start(dialogueWaterCompleted);
                boilerRenderer.sprite = emptyBoilerSprite;
                Disable();
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    _handleInventory.DeselectItem();
                    _showDialogue.Start(dialogueWaterWrongItem);
                }
                else
                {
                    _showDialogue.Start(dialogueWaterNoItem);
                }
            }
            
        }

        private void RemovePizza()
        {
            if (pizzasWasRemoved) return;

            pizzaRenderer.enabled = false;
            _handleInventory.RemoveItemOnHand();
            _audioPlayer.PlaySFX(_audioClip, 0.2f);
            pizzasWasRemoved = true;
        }
    }
}