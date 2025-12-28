using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class BonsaiPuzzle:Interaction
    {
        [SerializeField] private string itemOnHand = "TijerasPodar";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private AudioClip _audioClip;

        [SerializeField] private string itemOnHandTiGetWater = "EmptyGlass";
        [SerializeField] private DialogueData dialogueWaterCompleted;
        [SerializeField] private DialogueData dialogueWaterNoItem;
        [SerializeField] private DialogueData dialogueWaterWrongItem;
        
        [SerializeField] private Sprite[] bonsaiStages;
        [SerializeField] private SpriteRenderer bonsaiRenderer;
        [SerializeField] private GameObject stick;
        
        [Inject] private readonly Inventory _inventory;
        [Inject] private readonly AudioPlayer _audioPlayer;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        private int cuts = 0;
        private int cutsNeeded = 3;
        private bool completelyCut = false;

        protected override void Awake()
        {
            stick.SetActive(false);
            bonsaiRenderer.sprite = bonsaiStages[0];
        }
        public override void Interact()
        {
            if (!Interactable) return;
            GetWater();
            CutAllBonsaiToAshes();
            
        }

        private void CutAllBonsaiToAshes()
        {
            if (completelyCut) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                CutBonsai();
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
            if (!completelyCut) return;
            if (_inventory.HasitemOnHand(itemOnHandTiGetWater))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddItem("GlassFullOfWater");
                _showDialogue.Start(dialogueWaterCompleted);
                bonsaiRenderer.sprite = bonsaiStages[bonsaiStages.Length - 1];
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

        private void CutBonsai()
        {
            cuts++;
            bonsaiRenderer.sprite = bonsaiStages[cuts];

            _audioPlayer.PlaySfx(_audioClip, 0.2f);

            if (cuts == 1)
            {
                stick.SetActive(true);
            }
            else if (cuts >= cutsNeeded && !completelyCut)
            {
                _handleInventory.RemoveItemOnHand();
                _showDialogue.Start(dialogueCompleted);
                completelyCut = true;
            }
        }
    }
}