using DG.Tweening;
using Febucci.UI;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Dialogues.Infrastructure
{
    public class DialogueCanvas: MonoBehaviour, Dialogue
    {
        [Inject] private readonly ShowDialogue _showDialogue;
        [Inject] private readonly CurrentDialogue _currentDialogue;
        [Inject] private readonly TravelButtonsCanvas _travelButtonsCanvas;
        
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
            button = GetComponent<Button>();
            button.onClick.AddListener(Tap);
            OnHide();
        }

        private void Update()
        {
            if (Time.frameCount % 30 != 0) return;
            if (!isVisible && !_currentDialogue.Hid)_currentDialogue.End();
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
                canvasGroup.DOFade(1, 0.25f).OnComplete(() =>OnShow(line));
                _travelButtonsCanvas.Hide();
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
            _travelButtonsCanvas.Show();
            rectTransform.DOMove(hidePosition.position, 0.25f).OnComplete(OnHide);
        }

        private void OnShow(string line)
        {
            TypeLine(line);
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        private void OnHide()
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            PauseAnimation.cacheResume = true;
            KidPauseAnimation.cacheResume = true;
            TypeLine("");
        }
    }
}