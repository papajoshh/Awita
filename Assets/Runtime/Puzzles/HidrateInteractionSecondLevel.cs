using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class HidrateInteractionSecondLevel: Interaction
    {
        [SerializeField] private string itemOnHand = "GlassOfWater";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private Animator animator;
        [SerializeField] private Animator animatorKid;
        [SerializeField] private PauseAnimation pauseAnimation;
        [SerializeField] private KidPauseAnimation pauseKidAnimation;
        [SerializeField] private HidrateInteractionThirdLevel hidrateInteraction;
        
        [Inject] private readonly Child _child;
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;
        [Inject] private readonly TransitionToRoomCanvas _transition;

        public override void Interact()
        {
            if (_child.SecondLevelHidrationCompleted) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _child.Hidrate();
                animator.Play("DrinkWater");
                animatorKid.Play("KidDrinkingAnimation");
                _showDialogue.OnShowNewLine += pauseAnimation.Resume;
                _showDialogue.OnShowNewLine += pauseKidAnimation.Resume;
                _showDialogue.OnEndDialogue += DisableAndGoToBathroom;
                _showDialogue.Start(_child.GetPhraseOfHidratation());
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
        
        private void DisableAndGoToBathroom()
        {
            if (!_child.SecondLevelHidrationCompleted) return;
            _showDialogue.OnEndDialogue -= DisableAndGoToBathroom;
            Disable();
            _transition.GoToRoom("bathroom");
            hidrateInteraction.Enable();
        }
    }
}