using UnityEngine;

namespace Runtime.Infrastructure
{
    public class MoveAndDisableUIInteraction:Interaction
    {
        [SerializeField] private GameObject openedGO;
        [SerializeField] private GameObject closedGO;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private bool startClosed = true;
        
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
            Close();
        }
        
        private void OnDisable()
        {
            closed = true;
            closedGO.SetActive(true);
            openedGO.SetActive(false);
        }

        private void Close()
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
            closed = true;
            closedGO.SetActive(true);
            openedGO.SetActive(false);
        }

        private void Open()
        {
            closed = false;
            closedGO.SetActive(false);
            openedGO.SetActive(true);
        }
    }
}