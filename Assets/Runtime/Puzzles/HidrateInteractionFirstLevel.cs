using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class HidrateInteractionFirstLevel: Interaction
    {
        [SerializeField] private string itemOnHand = "GlassOfWater";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private Animator animator;
        [SerializeField] private PauseAnimation pauseAnimation;
        [SerializeField] private Interaction[] roomInteractions;
        [SerializeField] private ItemContainer[] roomitems;
        
        [Inject] private readonly Child _child;
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        protected override void Awake()
        {
            base.Awake();
            foreach (var interaction in roomInteractions)
            {
                interaction.Disable();
            }

            foreach (var item in roomitems)
            {
                item.Disable();
            }
        }
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
                _handleInventory.AddItem("EmptyGlass");
                foreach (var interaction in roomInteractions)
                {
                    interaction.Enable();
                }

                foreach (var item in roomitems)
                {
                    item.Enable();
                }
                Disable();
                gameObject.SetActive(false);
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