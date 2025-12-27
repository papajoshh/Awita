using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class TransitionToRoomButton: MonoBehaviour
    {
        [SerializeField] private string roomName;
        [Inject] private readonly TransitionToRoomCanvas transitionToRoomCanvas;
        private void Awake()
        {
            var button = GetComponent<UnityEngine.UI.Button>();
            button.onClick.AddListener(() =>
            {
                transitionToRoomCanvas.GoToRoom(roomName);
            });
        }
        
    }
}