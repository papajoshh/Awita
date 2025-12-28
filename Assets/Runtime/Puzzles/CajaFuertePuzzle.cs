using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Infrastructure
{
    public class CajaFuertePuzzle:MonoBehaviour
    {
        [SerializeField] private TeclaCajaFuerte[] teclas;
        [SerializeField] private Image button;
        [SerializeField] private Sprite buttonDefaultSprite;
        [SerializeField] private Sprite buttonPressedSprite;
        [SerializeField] private Image[] codeIndicators;
        [SerializeField] private CanvasGroup closedSafeBoxCanvasGroup;
        [SerializeField] private CanvasGroup openedSafeBoxCanvasGroup;
        [SerializeField] private Sprite indicatorEmptySprite;
        [SerializeField] private Sprite indicatorFilledSprite;
        private const string correctCode = "2711";
        private string enteredCode = "";

        //SFX
        [SerializeField] private AudioClip _audioClip_tecla;
        [SerializeField] private AudioClip _audioClip_correct;
        [SerializeField] private AudioClip _audioClip_error;
        [SerializeField] private AudioClip _audioClip_openSafe;
        [Inject] private readonly AudioPlayer _audioPlayer;

        private void Awake()
        {
            foreach (var tecla in teclas)
            {
                tecla.OnTeclaPresionada += OnGetNumber;
            }
        }

        private void OnDestroy()
        {
            foreach (var tecla in teclas)
            {
                tecla.OnTeclaPresionada -= OnGetNumber;
            }
            StopAllCoroutines();
        }

        private void OnGetNumber(int numero)
        {
            enteredCode += numero.ToString();
            UpdateIndicators();
            _audioPlayer.PlaySfx(_audioClip_tecla, 0.2f);
            if (enteredCode.Length != correctCode.Length) return;
            if(string.Equals(enteredCode, correctCode))
            {
                OpenSafebox();
            }
            else
            {
                ResetSafebox();
            }
        }

        private void UpdateIndicators()
        {
            for (int i = 0; i < codeIndicators.Length; i++)
            {
                if (i < enteredCode.Length)
                {
                    codeIndicators[i].sprite = indicatorFilledSprite;
                }
                else
                {
                    codeIndicators[i].sprite = indicatorEmptySprite;
                }
            }
        }

        private void OpenSafebox()
        {
            button.sprite = buttonPressedSprite;
            closedSafeBoxCanvasGroup.interactable = false;
            closedSafeBoxCanvasGroup.blocksRaycasts = false;
            _audioPlayer.PlaySfx(_audioClip_correct, 0.2f);

            openedSafeBoxCanvasGroup.DOFade(1, 0.75f).OnComplete(() =>
            {
                _audioPlayer.PlaySfx(_audioClip_openSafe, 0.2f);
                openedSafeBoxCanvasGroup.interactable = true;
                openedSafeBoxCanvasGroup.blocksRaycasts = true;
            });
        }

        private void ResetSafebox()
        {
            enteredCode = "";

            _audioPlayer.PlaySfx(_audioClip_error, 0.2f);
            StartCoroutine(BlinkButton());
        }

        private IEnumerator BlinkButton()
        {
            button.sprite = buttonPressedSprite;
            yield return new WaitForSeconds(0.4f);
            UpdateIndicators();
            button.sprite = buttonDefaultSprite;
        }
    }
}