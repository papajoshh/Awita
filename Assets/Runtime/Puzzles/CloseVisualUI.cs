using DG.Tweening;
using UnityEngine;

namespace Runtime.Infrastructure
{
    public class CloseVisualUI: Interaction
    {
        [SerializeField] private CanvasGroup canvasGroup;
        public override void Interact()
        {
            if(!Interactable) return;
            canvasGroup.DOFade(0, 1F).OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                canvasGroup.gameObject.SetActive(false);
            });
        }
    }
}