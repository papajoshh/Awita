

using UnityEngine;

namespace Runtime.Infrastructure
{
    public class WaterAtHomeWindowInteraction:Interaction
    {
        [SerializeField] private GameObject waterAtEntrance;
        
        protected override void Awake()
        {
            base.Awake();
            waterAtEntrance.SetActive(false);
        }
        public override void Interact()
        {
            if(!Interactable) return;
            waterAtEntrance.SetActive(true);
        }
    }
}