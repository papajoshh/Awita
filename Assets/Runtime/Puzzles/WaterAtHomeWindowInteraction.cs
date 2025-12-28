

using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class WaterAtHomeWindowInteraction:Interaction
    {
        [SerializeField] private GameObject waterAtEntrance;
        [SerializeField] private AudioClip pedidoClip;
        
        [Inject] private readonly AudioPlayer _audioPlayer;
        
        protected override void Awake()
        {
            base.Awake();
            waterAtEntrance.SetActive(false);
        }
        public override void Interact()
        {
            if(!Interactable) return;
            waterAtEntrance.SetActive(true);
            _audioPlayer.PlaySFX(pedidoClip);
        }
    }
}