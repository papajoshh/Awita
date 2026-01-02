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
        [SerializeField] private AudioClip _extractorOn;
        [SerializeField] private SpriteRenderer cloudRenderer;
        [SerializeField] private GetWaterFromCloud _getWaterFromCloud;
        [SerializeField] private GameObject _humoEffect;

        [Inject] private readonly AudioPlayer _audioPlayer;
        [Inject] private readonly Extractor _extractor;
        [Inject] private readonly Cloud _cloud;
        
        private Tween _tween;
        
        protected override void Awake()
        {
            base.Awake();
            
            turnOnExtractor.enabled = false;
            turnOffExtractor.enabled = true;
            cloudRenderer.color = new Color(1, 1, 1, 0);
            _getWaterFromCloud.Disable();
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
            if (_extractor.IsExtracting)
                Open();
            else 
                Close();
        }
        private void Close()
        {
            if (turnOffExtractor.enabled) return;
            turnOnExtractor.enabled = false;
            turnOffExtractor.enabled = true;
            _audioPlayer.PlaySFX(_audioClip);
            _audioPlayer.StopSFX(_extractorOn);
            _tween?.Kill();
            if(!_cloud.IsRaining) _tween = cloudRenderer.DOColor(new Color(1, 1, 1, 0), 0.75f);
            _humoEffect.SetActive(false);
        }

        public void Open()
        {
            if (turnOnExtractor.enabled) return;
            _audioPlayer.PlaySFX(_audioClip);
            _audioPlayer.PlaySFX(_extractorOn, 0.1f, true);
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
            _getWaterFromCloud.StartRaining();
        }
    }
}