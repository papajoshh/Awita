using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class TransitionToRoomCanvas: MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        [Inject] private readonly TravelButtonsCanvas _travelButtonsCanvas;
        public string CurrentRoom { get; private set; } = "room";
        private Camera _mainCamera;
        private void Awake()
        {
            _mainCamera = Camera.main;
            canvasGroup.blocksRaycasts = false;
        }

        public void GoToRoom(string roomName)
        {
            var sequence = DOTween.Sequence();
            canvasGroup.blocksRaycasts = true;
            sequence.Append(canvasGroup.DOFade(1, 0.3f).OnComplete(() => ChangeCameraToRoom(roomName)));
            sequence.Append(canvasGroup.DOFade(0, 0.3f).SetDelay(0.5f).OnComplete(() => canvasGroup.blocksRaycasts = false));
        }

        public void GoToRoom()
        {
            GoToRoom("room");
        }
        public void GoToBathroom()
        {
            GoToRoom("bathroom");
        }
        public void GoToKitchen()
        {
            GoToRoom("kitchen");
        }
        private void ChangeCameraToRoom(string roomName)
        {
            CurrentRoom = roomName;
            _travelButtonsCanvas.UnlockButtons();
            _travelButtonsCanvas.UpdateCurrentState();
            var roomTransform = roomName switch
            {
                "room" => new Vector3(0, 0, -10),
                "bathroom" => new Vector3(20, 0, -10),
                "kitchen" => new Vector3(40, 0, -10),
                _ => throw new ArgumentOutOfRangeException(nameof(roomName), roomName, null)
            };
            _mainCamera.transform.parent.position = roomTransform;
        }
    }
}