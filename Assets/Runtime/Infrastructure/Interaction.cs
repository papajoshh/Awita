using System;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public abstract class Interaction: MonoBehaviour, Hoverable
    {
        [Inject] private readonly CurrentDialogue _currentDialogue;
        public bool Interactable { get; private set; } = true;
        public virtual Sprite Icon => UIResources.Interact.Icon;

        public void OnMouseOver()
        {
            if (!_currentDialogue.Hid) return;
            if (Input.GetMouseButton(0)) Interact();
        }
        
        public void Enable() => Interactable = true;
        public void Disable() => Interactable = false;

        public abstract void Interact();
    }
}