using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
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
    [Inject] private readonly Child _child;

    //SFX
    [SerializeField] private AudioClip _audioClip_getWater;
    [Inject] private readonly AudioPlayer _audioPlayer;

    private bool isDone;
    public override void Interact()
    {
        if (!Interactable) return;
        if (isDone)
        {
            _showDialogue.Start(_child.GetPhraseOfDryPlace());
            return;
        }
        if (_inventory.HasitemOnHand(itemOnHand))
        {
            _handleInventory.RemoveItemOnHand();
            _handleInventory.AddGlassOfWater();
            _audioPlayer.PlaySFX(_audioClip_getWater, 0.2f);
            _showDialogue.Start(dialogueCompleted);
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
                _showDialogue.Start(dialogueWrongItem);
            }
            else
            {
                _showDialogue.Start(dialogueNoItem);
            }
        }
    }
}
