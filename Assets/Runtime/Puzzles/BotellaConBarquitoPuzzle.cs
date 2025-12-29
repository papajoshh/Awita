using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class BotellaConBarquitoPuzzle:Interaction
    {
        [SerializeField] private string itemOnHand = "Pincitas";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        
        [SerializeField] private string itemOnHandTiGetWater = "EmptyGlass";
        [SerializeField] private DialogueData dialogueWaterCompleted;
        [SerializeField] private DialogueData dialogueWaterNoItem;
        [SerializeField] private DialogueData dialogueWaterWrongItem;
        
        [SerializeField] private Sprite[] barquitoStages;
        [SerializeField] private SpriteRenderer botellaRenderer;
        
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        //SFX
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioClip _audioClip_getWater;
        [Inject] private readonly AudioPlayer _audioPlayer;

        private int dismountings = 0;
        private int dismountingsNeeded = 2;
        private bool completelyDismounted = false;

        protected override void Awake()
        {
            botellaRenderer.sprite = barquitoStages[0];
        }
        public override void Interact()
        {
            if (!Interactable) return;
            GetWater();
            CompletelyDismountShip();
            
        }

        private void CompletelyDismountShip()
        {
            if (completelyDismounted) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                DismountShip();
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
            if (!completelyDismounted) return;
            if (_inventory.HasitemOnHand(itemOnHandTiGetWater))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddGlassOfWater();
                _audioPlayer.PlaySFX(_audioClip_getWater, 0.2f);
                _showDialogue.Start(dialogueWaterCompleted);
                botellaRenderer.sprite = barquitoStages[barquitoStages.Length - 1];
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

        private void DismountShip()
        {
            dismountings++;
            botellaRenderer.sprite = barquitoStages[dismountings];
            _audioPlayer.PlaySFX(_audioClip, 0.2f);

            if (dismountings < dismountingsNeeded || completelyDismounted) return;
            _handleInventory.RemoveItemOnHand();
            completelyDismounted = true;
        }
    }
}