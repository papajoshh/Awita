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
        [SerializeField] private PauseAnimation pauseKidAnimation;
        
        [Inject] private readonly Child _child;
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;
        [Inject] private readonly TransitionToRoomCanvas _transition;

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
                pauseAnimation.OnEnded += DisableAndGoToBathroom;
                _showDialogue.OnShowNewLine += pauseAnimation.Resume;
                _showDialogue.OnShowNewLine += pauseKidAnimation.Resume;
                _showDialogue.Start(dialogueCompleted);
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddGlassOfWater();
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
        
        private void DisableAndGoToBathroom()
        {
            if (!_child.ThirdLevelHidrationCompleted) return;
            pauseAnimation.OnEnded -= DisableAndGoToBathroom;
            Disable();
        }
    }
}