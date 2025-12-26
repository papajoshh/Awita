using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
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
        
        [Inject] private readonly Child _child;
        [Inject] private readonly Inventory _inventory;
        [Inject] private readonly ShowDialogue _showDialogue;
        
        public override void Interact()
        {
            if (_child.FirstLevelHidrationCompleted) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _child.Hidrate();
                _showDialogue.Start(dialogueCompleted);
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