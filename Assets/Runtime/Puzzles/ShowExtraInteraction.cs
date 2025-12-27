
using Runtime.ExtraInteraction.Application;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class ShowExtraInteraction: Interaction
    {
        [SerializeField] private string openExtraInteraction = "";
    
        [Inject] private readonly ShowPopupInteraction _showPopupInteraction;
        public override void Interact()
        {
            if (!Interactable) return;
            _showPopupInteraction.Execute(openExtraInteraction);
        }
    }
}