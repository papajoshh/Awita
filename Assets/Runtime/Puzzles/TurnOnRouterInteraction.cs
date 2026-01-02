using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Runtime.Infrastructure
{
    public class TurnOnRouterInteraction: Interaction
    {
        [SerializeField] private SpriteRenderer turnOnRouter;
        [SerializeField] private SpriteRenderer turnOffRouter;
        [SerializeField] private AudioClip _audioClip;

        [Inject] private readonly AudioPlayer _audioPlayer;
        [Inject] private readonly Router _router;
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
            turnOnRouter.color = new Color (1, 1, 1, 0);
            turnOffRouter.color = Color.white;
        }

        private void Open()
        {
            _router.Connect();
            turnOnRouter.color = Color.white;
            turnOffRouter.color = new Color (1, 1, 1, 0);
            _audioPlayer.PlaySFX(_audioClip, 0.2f);
        }
    }
}