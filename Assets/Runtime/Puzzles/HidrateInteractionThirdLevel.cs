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
        [SerializeField] private PauseAnimation pauseAnimation;
        [SerializeField] private HidrateInteractionThirdLevel hidrateInteraction;
        
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
                pauseAnimation.OnEnded += DisableAndGoToBathroom;
                _showDialogue.OnShowNewLine += pauseAnimation.Resume;
                _showDialogue.Start(dialogueCompleted);
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddItem("EmptyGlass");
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
            Disable();
            _transition.GoToRoom("bathroom");
            hidrateInteraction.Enable();
        }
    }
}