
using Runtime.ExtraInteraction.Application;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class ShowExtraInteraction: Interaction
    {
        [SerializeField] private string openExtraInteraction = "";
        [SerializeField] private AudioClip _audioClip;

        [Inject] private readonly AudioPlayer _audioPlayer;

        [Inject] private readonly ShowPopupInteraction _showPopupInteraction;
        public override void Interact()
        {
            if (!Interactable) return;

            _showPopupInteraction.Execute(openExtraInteraction);
            _audioPlayer.PlaySfx(_audioClip, 0.2f);
        }
    }
}