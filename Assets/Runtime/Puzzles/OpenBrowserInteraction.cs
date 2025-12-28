using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class OpenBrowserInteraction:Interaction
    {
        [SerializeField] private GameObject noInternetWindow;
        [SerializeField] private CanvasGroup noInternetCanvasGroup;
        [SerializeField] private GameObject waterAtHomeWindow;
        [SerializeField] private CanvasGroup waterAtHomeCanvasGroup;
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
                waterAtHomeCanvasGroup.alpha = 1;
                waterAtHomeCanvasGroup.interactable = true;
                waterAtHomeCanvasGroup.blocksRaycasts = true;
            }
            else
            {
                noInternetWindow.SetActive(true);
                noInternetCanvasGroup.alpha = 1;
                waterAtHomeCanvasGroup.interactable = true;
                waterAtHomeCanvasGroup.blocksRaycasts = true;
            }
        }
    }
}