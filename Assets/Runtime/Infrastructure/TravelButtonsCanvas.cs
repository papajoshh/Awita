using System;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class TravelButtonsCanvas: MonoBehaviour
    {
        [SerializeField] private GameObject roomButtons;
        [SerializeField] private GameObject bathroomButtons;
        [SerializeField] private GameObject kitchenButtons;
        [SerializeField] private CanvasGroup canvasGroup;

        [Inject] private readonly TransitionToRoomCanvas transition;

        private void Awake()
        {
            canvasGroup.alpha = 0;
        }
        
        public void UnlockButtons()
        {
            if (canvasGroup.alpha >= 1) return;
            roomButtons.SetActive(false);
            bathroomButtons.SetActive(true);
            kitchenButtons.SetActive(false);
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }
        
        public void UpdateCurrentState()
        {
            var currentState = transition.CurrentRoom;
            switch (currentState)
            {
                case "room":
                    roomButtons.SetActive(true);
                    bathroomButtons.SetActive(false);
                    kitchenButtons.SetActive(false);
                    break;
                case "bathroom":
                    roomButtons.SetActive(false);
                    bathroomButtons.SetActive(true);
                    kitchenButtons.SetActive(false);
                    break;
                case "kitchen":
                    roomButtons.SetActive(false);
                    bathroomButtons.SetActive(false);
                    kitchenButtons.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}