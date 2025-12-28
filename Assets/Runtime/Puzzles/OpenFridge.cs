using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class OpenFridge: Interaction
    {
        [SerializeField] private SpriteRenderer openedCloset;
        [SerializeField] private SpriteRenderer closedCloset;
        [SerializeField] private AudioClip openAudio;
        [SerializeField] private AudioClip closeAudio;
        [SerializeField] private ItemContainer quesoItem;
        [SerializeField] private ItemContainer hieloItem;
        
        [Inject] private readonly AudioPlayer _audioPlayer;

        private int count;
        protected override void Awake()
        {
            base.Awake();
            Close(false);
            quesoItem.OnRecollect += CountItemsRecollect;
            hieloItem.OnRecollect += CountItemsRecollect;
        }
        
        private void OnDestroy()
        {
            quesoItem.OnRecollect -= CountItemsRecollect;
            hieloItem.OnRecollect -= CountItemsRecollect;
            
            StopAllCoroutines();
        }
        
        
        private void CountItemsRecollect()
        {
            count++;
            if (count >= 2)
            {
                StartCoroutine(CloseWithDelay());
            }
        }

        private IEnumerator CloseWithDelay()
        {
            yield return new WaitForSeconds(0.75f);
            Close();
        }

        public override void Interact()
        {
            if (!Interactable) return;
            Open();
            Disable();
        }
        

        private void Close(bool playSound = true)
        {
            if(playSound) _audioPlayer.PlaySfx(closeAudio, 0.2f);
            quesoItem.Disable();
            hieloItem.Disable();
            openedCloset.color = new Color (1, 1, 1, 0);
            closedCloset.color = Color.white;
        }

        private void Open()
        {
            quesoItem.Enable();
            _audioPlayer.PlaySfx(openAudio, 0.2f);
            openedCloset.DOColor(Color.white, 0.75f);
            closedCloset.DOColor(new Color (1, 1, 1, 0), 0.75f);
        }
    }
}