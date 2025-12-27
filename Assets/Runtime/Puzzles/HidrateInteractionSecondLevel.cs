using System;
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
        [SerializeField] private PauseAnimation pauseAnimation;
        
        [Inject] private readonly Child _child;
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        private void Awake()
        {
            Disable();
        }

        public override void Interact()
        {
            if (_child.SecondLevelHidrationCompleted) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _child.Hidrate();
                animator.Play("DrinkWater");
                _showDialogue.OnShowNewLine += pauseAnimation.Resume;
                _showDialogue.Start(dialogueCompleted);
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddItem("EmptyGlass");

                if (_child.SecondLevelHidrationCompleted)
                {
                    Disable();
                    gameObject.SetActive(false);
                    //Gestión de ir al cuarto de baño
                }
                
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