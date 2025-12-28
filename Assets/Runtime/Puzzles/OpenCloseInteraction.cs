using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class OpenCloseInteraction: Interaction
    {
        [SerializeField] private GameObject openedDrawer;
        [SerializeField] private GameObject closedDrawer;
        [SerializeField] private bool startClosed = true;
        [SerializeField] private AudioClip _audioClip_open;
        [SerializeField] private AudioClip _audioClip_close;

        [Inject] private readonly AudioPlayer _audioPlayer;
        private bool closed;

        protected override void Awake()
        {
            base.Awake();
            if (startClosed)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        public override void Interact()
        {
            if (!Interactable) return;
            Toggle();
        }

        private void Toggle()
        {
            if(closed) Open();
            else Close();
        }
        
        private void OnDisable()
        {
            closed = true;
            closedDrawer.SetActive(true);
            openedDrawer.SetActive(false);
        }

        private void Close()
        {
            closed = true;
            closedDrawer.SetActive(true);
            openedDrawer.SetActive(false);
            _audioPlayer.PlaySFX(_audioClip_close, 0.2f);
        }

        private void Open()
        {
            closed = false;
            closedDrawer.SetActive(false);
            openedDrawer.SetActive(true);
            _audioPlayer.PlaySFX(_audioClip_open, 0.2f);
        }
    }
}