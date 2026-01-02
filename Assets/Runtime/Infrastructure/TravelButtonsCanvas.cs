using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class TravelButtonsCanvas: MonoBehaviour
    {
        
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private GameObject roomButtons;
        [SerializeField] private GameObject bathroomButtons;
        [SerializeField] private GameObject kitchenButtons;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform _anchorShowed;
        [SerializeField] private RectTransform _anchorHid;
        
        [Inject] private readonly TransitionToRoomCanvas transition;

        private bool unlocked;

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
            unlocked = true;
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
        
        public void Show()
        {
            if (!unlocked) return;
            rectTransform.DOAnchorPosY(_anchorShowed.anchoredPosition.y, 0.75f);
        }

        public void Hide()
        {
            if (!unlocked) return;
            rectTransform.DOAnchorPosY(_anchorHid.anchoredPosition.y, 0.75f);
        }
    }
}