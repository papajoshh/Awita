using System;
using DG.Tweening;
using Febucci.UI;
using Runtime.Dialogues.Domain;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Infrastructure
{
    public class GameOverCanvas: MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private AudioClip endingMusic;
        [SerializeField] private DialogueData endingDialogue;
        [SerializeField] private TypewriterByCharacter writer;
        [SerializeField] private Button nextLineButton;
        
        [Inject] private CurrentDialogue _currentDialogue;
        [Inject] private readonly AudioPlayer _audioPlayer;
        private void Awake()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0;
            nextLineButton.onClick.AddListener(Tap);
        }

        private void OnDestroy()
        {
            nextLineButton.onClick.RemoveAllListeners();
        }

        private void Update()
        {
            #if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Show();
            }
            #endif
        }

        public void Show()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
            _canvasGroup.DOFade(1, 1f).OnComplete(ShowEnding);
        }

        private void ShowEnding()
        {
            _audioPlayer.PlayMusic(endingMusic);
            _currentDialogue.Start(endingDialogue);
            TypeLine(_currentDialogue.FirstLine);
        }
        
        private void TypeLine(string line)
        {
            text.text = text.text = line.Replace("\\n", "\n");
        }
        
        private void Tap()
        {
            if (writer.isShowingText)
            {
                writer.SkipTypewriter();
            }
            else
            {
                ShowNextLine();
            }
        }
        
        private void ShowNextLine()
        {
            if (_currentDialogue.HasEnded)
            {
                _currentDialogue.End();
            }
            else
            {
                TypeLine(_currentDialogue.NextLine);
            }
        }
    }
}