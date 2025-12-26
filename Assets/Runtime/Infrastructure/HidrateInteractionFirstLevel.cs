using DG.Tweening;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Runtime.Infrastructure
{
    public class HidrateInteractionFirstLevel: Interaction, Hoverable, Interactable
    {
        [SerializeField] private string itemOnHand = "GlassOfWater";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private Animator animator;
        [SerializeField] private PauseAnimation pauseAnimation;
        
        [Inject] private readonly Child _child;
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;
        
        public override void Interact()
        {
            if (_child.FirstLevelHidrationCompleted) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _child.Hidrate();
                animator.Play("DrinkWater");
                _showDialogue.OnShowNewLine += pauseAnimation.Resume;
                _showDialogue.Start(dialogueCompleted);
                _handleInventory.RemoveItemOnHand();
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