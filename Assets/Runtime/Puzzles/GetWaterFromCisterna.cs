using DG.Tweening;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
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
        [Inject] private readonly Child _child;

        //SFX
        [SerializeField] private AudioClip _audioClip_move_cisterna;
        [SerializeField] private AudioClip _audioClip_ghost_appears;
        [SerializeField] private AudioClip _audioClip_getWater;
        [Inject] private readonly AudioPlayer _audioPlayer;

        private bool ghostIsGone = false;
        private bool tapRemoved = false;
        private bool ghostIsSeen = false;
        private bool isDone;
        protected override void Awake()
        {
            cisternaRenderer.sprite = initialCisternaSprite;
            ghostRenderer.color = new Color (1, 1, 1, 0);
        }
        public override void Interact()
        {
            if (!Interactable) return;
            GhostAppearsForFirstTime();
            GetWater();
            RemoveTapCisterna();
        }

        private void GhostAppearsForFirstTime()
        {
            if (ghostIsGone) return;
            if (ghostIsSeen)
            {
                InteractWithGhost();
                return;
            }
            _audioPlayer.PlaySFX(_audioClip_ghost_appears, 0.2f);
            ghostIsSeen = true;
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
        private void RemoveTapCisterna()
        {
            if (!ghostIsGone) return;
            if (tapRemoved) return;

            cisternaRenderer.sprite = cisternaWithTapRemovedSprite;
            _audioPlayer.PlaySFX(_audioClip_move_cisterna, 0.2f);
            tapRemoved = true;
        }
        private void GetWater()
        {
            if (!ghostIsGone) return;
            if (!tapRemoved) return;
            if (isDone)
            {
                _showDialogue.Start(_child.GetPhraseOfDryPlace());
                return;
            }
            if (_inventory.HasitemOnHand(itemOnHandTiGetWater))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddGlassOfWater();
                _audioPlayer.PlaySFX(_audioClip_getWater, 0.2f);
                _showDialogue.Start(dialogueWaterCompleted);
                cisternaRenderer.sprite = emptyCisternaSprite;
                isDone = true;
            }
            else
            {
                if (_handleInventory.HasGlassOfWater())
                {
                    _showDialogue.Start(_child.GetPhraseOfWaterOnGlass());
                    return;
                }
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