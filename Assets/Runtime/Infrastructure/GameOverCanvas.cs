using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Runtime.Infrastructure
{
    public class GameOverCanvas: MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI text;
        private void Awake()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0;
        }

        public void Show()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _canvasGroup.DOFade(1, 1f).OnComplete(ShowEnding);
        }

        private void ShowEnding()
        {
            text.text = "Me hubiera gustado que me dieras un poquito de agua";
        }
    }
}