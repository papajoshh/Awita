using DG.Tweening;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Infrastructure;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

public class GetRed : Interaction
{
    [SerializeField] private string itemOnHand = "Palo";
    [SerializeField] private DialogueData dialogueCompleted;
    [SerializeField] private DialogueData dialogueNoItem;
    [SerializeField] private DialogueData dialogueWrongItem;
    [SerializeField] private SpriteRenderer telaraña;
    
    [Inject] private readonly Inventory _inventory;
    [Inject] private HandleInventory _handleInventory;
    [Inject] private readonly ShowDialogue _showDialogue;
    
    public override void Interact()
    {
        if (!Interactable) return;
        if (_inventory.HasitemOnHand(itemOnHand))
        {
            _handleInventory.RemoveItemOnHand();
            _handleInventory.AddItem("Red");
            telaraña.DOFade(0, 0.25f);
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
