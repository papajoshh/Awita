using System;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using UnityEngine;

namespace Runtime.Infrastructure
{
    public class Interaction: MonoBehaviour, Hoverable
    {
        public Sprite Icon => UIResources.Interact.Icon;
        [SerializeField] private DialogueData dialogue;

        private void ShowText()
        {
            ShowDialogue.Start(dialogue);
        }

        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowText();
            }
        }
    }
}