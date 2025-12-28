using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class OpenBrowserInteraction:Interaction
    {
        [SerializeField] private GameObject noInternetWindow;
        [SerializeField] private GameObject waterAtHomeWindow;
        [Inject] private readonly Router _router;
        
        protected override void Awake()
        {
            base.Awake();
            noInternetWindow.SetActive(false);
            waterAtHomeWindow.SetActive(false);
        }
        public override void Interact()
        {
            if (!Interactable) return;
            if (_router.IsConnected)
            {
                waterAtHomeWindow.SetActive(true);
            }
            else
            {
                noInternetWindow.SetActive(true);
            }
        }
    }
}