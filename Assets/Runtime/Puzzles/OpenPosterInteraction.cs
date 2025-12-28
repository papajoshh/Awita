using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class OpenPosterInteraction: Interaction
    {
        [SerializeField] private SpriteRenderer openedPoster;
        [SerializeField] private SpriteRenderer closedPoster;
        [SerializeField] private AudioClip _audioClip;

        [Inject] private readonly AudioPlayer _audioPlayer;

        protected override void Awake()
        {
            base.Awake();
            Close();
        }

        public override void Interact()
        {
            if (!Interactable) return;
            Open();
            Disable();
        }
        

        private void Close()
        {
            openedPoster.color = Color.clear;
            closedPoster.color = Color.white;
        }

        private void Open()
        {
            openedPoster.DOColor(Color.white, 0.75f);
            closedPoster.DOColor(Color.clear, 0.75f);
            _audioPlayer.PlaySfx(_audioClip, 0.2f);
        }
    }
}