using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Infrastructure;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

public class RefillGlass : Interaction
{
    [SerializeField] private string itemOnHand = "EmptyGlass";
    [SerializeField] private DialogueData dialogueCompleted;
    [SerializeField] private DialogueData dialogueNoItem;
    [SerializeField] private DialogueData dialogueWrongItem;
    
    [Inject] private readonly Inventory _inventory;
    [Inject] private HandleInventory _handleInventory;
    [Inject] private readonly ShowDialogue _showDialogue;
    
    public override void Interact()
    {
        if (!Interactable) return;
        if (_inventory.HasitemOnHand(itemOnHand))
        {
            _handleInventory.RemoveItemOnHand();
            _handleInventory.AddItem("GlassFullOfWater");
            _showDialogue.Start(dialogueCompleted);
            Disable();
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
