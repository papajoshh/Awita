using DG.Tweening;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class PeceraPuzzle: Interaction
    {
        [SerializeField] private string itemOnHandToGetFish = "Red";
        [SerializeField] private DialogueData dialogueFishCompleted;
        [SerializeField] private DialogueData dialogueFishWithGlassCompleted;
        [SerializeField] private DialogueData dialogueFishNoItem;
        [SerializeField] private DialogueData dialogueFishWrongItem;
        [SerializeField] private SpriteRenderer peceraRenderer;
        [SerializeField] private SpriteRenderer fishRenderer;
        [SerializeField] private Sprite peceraConAgua;
        [SerializeField] private Sprite peceraVacia;
        [SerializeField] private Sprite peceraWithFish;
        
        [SerializeField] private string itemOnHand = "EmptyGlass";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
    
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        private bool fishIsOut;

        protected override void Awake()
        {
            peceraRenderer.sprite = peceraConAgua;
        }
        public override void Interact()
        {
            if (!Interactable) return;
            GetFishOut();
            GetWater();
        }
        private void GetFishOut()
        {
            if (fishIsOut) return;
            if (_inventory.HasitemOnHand(itemOnHandToGetFish))
            {
                _handleInventory.RemoveItemOnHand();
                peceraRenderer.sprite = peceraConAgua;
                fishRenderer.DOFade(0, 0.25f);
                _showDialogue.Start(dialogueFishCompleted);
                fishIsOut = true;
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    if (_inventory.HasitemOnHand(itemOnHand))
                    {
                        _showDialogue.Start(dialogueFishWithGlassCompleted);
                    }
                    else
                    {
                        _showDialogue.Start(dialogueFishWrongItem);
                    }
                    
                }
                else
                {
                    _showDialogue.Start(dialogueFishNoItem);
                }
            }
        }
        private void GetWater()
        {
            if (!fishIsOut) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                peceraRenderer.DOFade(0, 0.25f);
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddItem("GlassFullOfWater");
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