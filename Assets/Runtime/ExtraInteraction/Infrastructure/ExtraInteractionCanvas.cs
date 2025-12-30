using DG.Tweening;
using Runtime.ExtraInteraction.Application;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.ExtraInteraction.Infrastructure
{
    public class ExtraInteractionCanvas : MonoBehaviour, ExtraInteractionPopup
    {
        [Inject] private readonly ShowPopupInteraction showPopupInteraction;
        
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private GameObject containerPuzzles;
        [SerializeField] private Button closeButton;
        
        private GameObject currentPuzzle;
        
        private void Awake()
        {
            closeButton.onClick.AddListener(Hide);
            OnHideComplete();
        }

        private void Start()
        {
            var numberOfChilds = containerPuzzles.transform.childCount;
            for (int i = 0; i < numberOfChilds; i++)
            { 
                containerPuzzles.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            closeButton.onClick.RemoveListener(Hide);
        }

        public void Show(int puzzleIndex)
        {
            canvasGroup.blocksRaycasts = true;

            currentPuzzle =  containerPuzzles.transform.GetChild(puzzleIndex).gameObject;
            currentPuzzle.SetActive(true);
            
            canvasGroup.DOFade(1, 0.5F).OnComplete(OnShowComplete);
        }

        private void Hide()
        {
            canvasGroup.interactable = false;
            canvasGroup.DOFade(0, 0.5F).OnComplete(OnHideComplete);
        }
        
        private void OnShowComplete()
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
        }

        private void OnHideComplete()
        {
            currentPuzzle?.SetActive(false);
            currentPuzzle = null;
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
