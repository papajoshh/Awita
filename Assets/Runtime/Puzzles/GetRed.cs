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
    [SerializeField] private AudioClip audio_telaraña;
    
    [Inject] private readonly Inventory _inventory;
    [Inject] private readonly AudioPlayer _audioPlayer;
    [Inject] private HandleInventory _handleInventory;
    [Inject] private readonly ShowDialogue _showDialogue;
    
    public override void Interact()
    {
        if (!Interactable) return;
        if (_inventory.HasitemOnHand(itemOnHand))
        {
            _handleInventory.RemoveItemOnHand();
            _handleInventory.AddRed();
            _showDialogue.Start(dialogueCompleted);
            telaraña.DOFade(0, 0.25f);
            _audioPlayer.PlaySFX(audio_telaraña);
            Disable();
        }
        else
        {
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
