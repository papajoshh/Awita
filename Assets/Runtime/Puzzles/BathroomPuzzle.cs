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
        [SerializeField] private DialogueData dialogueWrongWithGlass;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private GameObject animation;
        
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

        //SFX
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private AudioClip _audioClip_getWater;
        [Inject] private readonly AudioPlayer _audioPlayer;

        private bool iceOnBathroom = false;

        protected override void Awake()
        {
            bathroomRenderer.sprite = initialBathroomSprite;
            animation.SetActive(true);
        }
        public override void Interact()
        {
            if (!Interactable) return;
            GetWater();
            PutIceOnBathroom();
            
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
                    if (_inventory.HasitemOnHand(itemOnHandTiGetWater))
                    {
                        _showDialogue.Start(dialogueWrongWithGlass);
                    }
                    else
                    {
                        _showDialogue.Start(dialogueWrongItem);
                    }
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
                _audioPlayer.PlaySFX(_audioClip_getWater, 0.2f);
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
            animation.SetActive(false);
            if (iceOnBathroom) return;
            _handleInventory.RemoveItemOnHand();
            iceOnBathroom = true;
            _audioPlayer.PlaySFX(_audioClip, 0.2f);
        }
    }
}