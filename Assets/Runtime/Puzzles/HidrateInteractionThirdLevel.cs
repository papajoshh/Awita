using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class HidrateInteractionThirdLevel: Interaction
    {
        [SerializeField] private string itemOnHand = "GlassOfWater";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private Animator animator;
        [SerializeField] private Animator animatorKid;
        [SerializeField] private PauseAnimation pauseAnimation;
        [SerializeField] private KidPauseAnimation pauseKidAnimation;
        
        [Inject] private readonly Child _child;
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;
        [Inject] private readonly TransitionToRoomCanvas _transition;
        [Inject] private readonly GameOverCanvas _gameOver;
        [Inject] private readonly AudioPlayer _audioPlayer;

        protected override void Awake()
        {
            base.Awake();
            Disable();
        }
        public override void Interact()
        {
            if (_child.ThirdLevelHidrationCompleted) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _child.Hidrate();
                animator.Play("DrinkWater");
                animatorKid.Play("KidDrinkingAnimation");
                _showDialogue.OnShowNewLine += pauseAnimation.Resume;
                _showDialogue.OnShowNewLine += pauseKidAnimation.Resume;
                _showDialogue.OnEndDialogue += ShowEnding;
                _showDialogue.Start(_child.GetPhraseOfHidratation());
                if(_child.ThirdLevelHidrationCompleted) _audioPlayer.StopMusic();
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddEmptyGlass();
            }
            else if (_handleInventory.HasWhiskyOnHand())
            {
                _showDialogue.Start(_child.GetDialogueAlcohol());
            }
            else if (_handleInventory.HasIceOnHand())
            {
                _showDialogue.Start(_child.GetDialogueIce());
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    _handleInventory.DeselectItem();
                    _showDialogue.Start(_child.GetRandomWrongPhrase());
                }
                else
                {
                    _showDialogue.Start(_child.GetRandomWrongPhrase());
                }
            }
        }
        
        private void ShowEnding()
        {
            if (!_child.ThirdLevelHidrationCompleted) return;
            _showDialogue.OnEndDialogue += ShowEnding;
            _gameOver.Show();
            Disable();
        }
    }
}