using DG.Tweening;
using UnityEngine;

namespace Runtime.Infrastructure
{
    public class OpenPosterInteraction: Interaction
    {
        [SerializeField] private SpriteRenderer openedPoster;
        [SerializeField] private SpriteRenderer closedPoster;
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
        }
    }
}