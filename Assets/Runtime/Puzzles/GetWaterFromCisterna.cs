using DG.Tweening;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class GetWaterFromCisterna:Interaction
    {
        [SerializeField] private RadioCassetteInteraction _radioCasseteInteraction;
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        
        [SerializeField] private string itemOnHandTiGetWater = "EmptyGlass";
        [SerializeField] private DialogueData dialogueWaterCompleted;
        [SerializeField] private DialogueData dialogueWaterNoItem;
        [SerializeField] private DialogueData dialogueWaterWrongItem;

        [SerializeField] private SpriteRenderer ghostRenderer;
        [SerializeField] private SpriteRenderer cisternaRenderer;
        [SerializeField] private Sprite initialCisternaSprite;
        [SerializeField] private Sprite cisternaWithTapRemovedSprite;
        [SerializeField] private Sprite emptyCisternaSprite;
        
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        private bool ghostIsGone = false;
        private bool tapRemoved = false;
        protected override void Awake()
        {
            cisternaRenderer.sprite = initialCisternaSprite;
            ghostRenderer.color = Color.clear;
        }
        public override void Interact()
        {
            if (!Interactable) return;
            PutMusicToGhostToSayGoodbye();
            GetWater();
            RemoveTapCisterna();
            
        }

        private void PutMusicToGhostToSayGoodbye()
        {
            if (ghostIsGone) return;
            ghostRenderer.DOFade(1, 0.25f).OnComplete(InteractWithGhost);
        }

        private void InteractWithGhost()
        {
            if (_radioCasseteInteraction.IsMusicPlaying)
            {
                SayGoodbyeToGhost();
            }
            else
            {
                _showDialogue.OnEndDialogue += FadeGhost;
                Disable();
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

        private void FadeGhost()
        {
            _showDialogue.OnEndDialogue -= FadeGhost;
            Enable();
            ghostRenderer.DOFade(0, 0.25f);
        }

        private void RemoveTapCisterna()
        {
            if (!ghostIsGone) return;
            cisternaRenderer.sprite = cisternaWithTapRemovedSprite;
            tapRemoved = true;
        }
        private void GetWater()
        {
            if (!ghostIsGone) return;
            if (!tapRemoved) return;
            if (_inventory.HasitemOnHand(itemOnHandTiGetWater))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddItem("GlassFullOfWater");
                _showDialogue.Start(dialogueWaterCompleted);
                cisternaRenderer.sprite = emptyCisternaSprite;
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

        private void SayGoodbyeToGhost()
        {
            _showDialogue.Start(dialogueCompleted);
            _showDialogue.OnEndDialogue += GoodbyeToGhost;
            
        }

        private void GoodbyeToGhost()
        {
            _showDialogue.OnEndDialogue -= GoodbyeToGhost;
            ghostRenderer.DOFade(0, 0.25f);

            if (ghostIsGone) return;
            ghostIsGone = true;
        }
    }
}