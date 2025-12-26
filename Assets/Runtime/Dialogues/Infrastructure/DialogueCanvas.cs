using DG.Tweening;
using Febucci.UI;
using Runtime.Application;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Dialogues.Infrastructure
{
    public class DialogueCanvas: MonoBehaviour, Dialogue
    {
        [Inject] private readonly ShowDialogue _showDialogue;
        [SerializeField] private TextMeshProUGUI lineText;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private RectTransform showPosition;
        [SerializeField] private RectTransform hidePosition;
        [SerializeField] private TypewriterByCharacter writer;

        private Button button;
        private bool isVisible => canvasGroup.alpha > 0;
        private void Awake()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            button = GetComponent<Button>();
            button.onClick.AddListener(Tap);
            TypeLine("");
        }

        private void Tap()
        {
            if (writer.isShowingText)
            {
                writer.SkipTypewriter();
            }
            else
            {
                _showDialogue.ShowNextLine();
            }
        }

        public void Display(string line)
        {
            if (!isVisible)
            {
                canvasGroup.DOFade(1, 0.5f).OnComplete(() =>OnShow(line));
                rectTransform.DOMove(showPosition.position, 0.25f);
            }
            else
            {
                TypeLine(line);
            }
        }
        
        private void TypeLine(string line)
        {
            lineText.text = line;
        }

        public void Hide()
        {
            if (!isVisible) return;
            rectTransform.DOMove(hidePosition.position, 0.25f).OnComplete(OnHide);
        }

        private void OnShow(string line)
        {
            TypeLine(line);
            canvasGroup.interactable = true;
        }
        private void OnHide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            TypeLine("");
        }
    }
}