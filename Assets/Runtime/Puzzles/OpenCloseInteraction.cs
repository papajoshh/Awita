using UnityEngine;

namespace Runtime.Infrastructure
{
    public class OpenCloseInteraction: Interaction
    {
        [SerializeField] private GameObject openedDrawer;
        [SerializeField] private GameObject closedDrawer;
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
            Toggle();
        }

        private void Toggle()
        {
            if(closed) Open();
            else Close();
        }
        
        private void OnDisable()
        {
            closed = true;
            closedDrawer.SetActive(true);
            openedDrawer.SetActive(false);
        }

        private void Close()
        {
            closed = true;
            closedDrawer.SetActive(true);
            openedDrawer.SetActive(false);
        }

        private void Open()
        {
            closed = false;
            closedDrawer.SetActive(false);
            openedDrawer.SetActive(true);
        }
    }
}