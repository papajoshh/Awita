using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class HidrateInteractionFirstLevel: Interaction, Hoverable, Interactable
    {
        public Sprite Icon => UIResources.Interact.Icon;
        public string ItemOnHand => "GlassOfWater";
        public DialogueData dialogue;

        [Inject] private readonly Child _child;
        [Inject] private readonly ShowDialogue _showDialogue;
        
        public override void Interact()
        {
            _child.Hidrate();
            _showDialogue.Start(dialogue);
        }
    }
}