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
        [SerializeField] private GameObject fridgeCloser;
        
        [Inject] private readonly AudioPlayer _audioPlayer;

        private int count;
        private FridgeCloser _closer;

        protected override void Awake()
        {
            base.Awake();
            Close(false);
            fridgeCloser.SetActive(false);
            _closer = fridgeCloser.GetComponent<FridgeCloser>();
        }

        private void OnEnable()
        {
            _closer.OnClose += CloseUsingDoor;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            _closer.OnClose -= CloseUsingDoor;
        }

        public override void Interact()
        {
            if (!Interactable) return;
            Open();
            Disable();
        }
        

        private void Close(bool playSound = true)
        {
            if (quesoItem != null)
                quesoItem.Disable();

            if (hieloItem != null)
                hieloItem.Disable();

            if (playSound) _audioPlayer.PlaySFX(closeAudio, 0.2f);
            openedCloset.color = new Color (1, 1, 1, 0);
            closedCloset.color = Color.white;
            fridgeCloser.SetActive(false);

            Enable();
        }

        private void Open()
        {
            if (quesoItem != null)
                quesoItem.Enable();
            
            if (hieloItem != null)
                hieloItem.Enable();

            _audioPlayer.PlaySFX(openAudio, 0.2f);
            openedCloset.DOColor(Color.white, 0.75f);
            closedCloset.DOColor(new Color (1, 1, 1, 0), 0.75f).OnComplete(() => {
                fridgeCloser.SetActive(true);
            });
        }

        private void CloseUsingDoor()
        { 
            Close();
        }
    }
}