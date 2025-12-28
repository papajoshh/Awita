using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

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
            openedSafeBoxCanvasGroup.DOFade(1, 0.75f).OnComplete(() =>
            {
                openedSafeBoxCanvasGroup.interactable = true;
                openedSafeBoxCanvasGroup.blocksRaycasts = true;
            });
        }

        private void ResetSafebox()
        {
            enteredCode = "";
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