using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class ExtractorInteraction: Interaction
    {
        [SerializeField] private SpriteRenderer turnOnExtractor;
        [SerializeField] private SpriteRenderer turnOffExtractor;
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private SpriteRenderer cloudRenderer;
        [SerializeField] private GameObject lluvia;
        [SerializeField] private GetWaterFromCloud _getWaterFromCloud;
        [SerializeField] private GameObject _humoEffect;

        [Inject] private readonly AudioPlayer _audioPlayer;
        [Inject] private readonly Extractor _extractor;
        [Inject] private readonly Cloud _cloud;
        
        private Tween _tween;
        
        protected override void Awake()
        {
            base.Awake();
            Close();
            _getWaterFromCloud.Disable();
            lluvia.SetActive(false);
        }

        private void Update()
        {
            if(_extractor.IsExtracting) _extractor.Update(Time.deltaTime);
            if (_extractor.IsExtracting && cloudRenderer.color.a >= 1f)
            {
                StartRaining();
            }
        }

        public override void Interact()
        {
            if (!Interactable) return;
            Toggle();
        }


        private void Toggle()
        {
            _extractor.Toogle();
            _audioPlayer.PlaySFX(_audioClip, 0.2f);
            if (_extractor.IsExtracting)
                Open();
            else 
                Close();
        }
        private void Close()
        {
            turnOnExtractor.enabled = false;
            turnOffExtractor.enabled = true;
            _tween?.Kill();
            if(!_cloud.IsRaining) _tween = cloudRenderer.DOColor(new Color(1, 1, 1, 0), 0.75f);
            _humoEffect.SetActive(false);
        }

        private void Open()
        {
            _humoEffect.SetActive(true);
            turnOnExtractor.enabled = true;
            turnOffExtractor.enabled = false;
            _tween?.Kill();
            if(!_cloud.IsRaining) _tween = cloudRenderer.DOColor(Color.white, 15);
        }

        private void StartRaining()
        {
            if (_cloud.IsRaining) return;
            _cloud.StartRaining();
            lluvia.SetActive(true);
            _getWaterFromCloud.Enable();
        }
    }
}