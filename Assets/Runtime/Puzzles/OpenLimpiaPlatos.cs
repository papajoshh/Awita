using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class OpenLimpiaPlatos: Interaction
    {
        [SerializeField] private SpriteRenderer openedCloset;
        [SerializeField] private SpriteRenderer closedCloset;
        [SerializeField] private AudioClip openAudio;
        [SerializeField] private ItemContainer itemContainer;
        
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
            itemContainer.Disable();
            openedCloset.color = new Color (1, 1, 1, 0);
            closedCloset.color = Color.white;
        }

        private void Open()
        {
            itemContainer.Enable();
            _audioPlayer.PlaySFX(openAudio, 0.2f);
            openedCloset.DOColor(Color.white, 0.75f);
            closedCloset.DOColor(new Color (1, 1, 1, 0), 0.75f);
        }
    }
}