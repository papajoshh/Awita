using System;
using DG.Tweening;
using Runtime.Domain;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.Infrastructure
{
    public class HidratationCanvas: MonoBehaviour
    {
        [Inject] private readonly Child _child;
        [SerializeField] private Image[] hidratationImages;

        private int count = -1;
        private void Awake()
        {
            _child.OnHidrate += UpdateHidratationDisplay;
            count = -1;
            foreach (var image in hidratationImages)
            {
                image.color = new Color (1, 1, 1, 0);
            }
        }
        
        private void OnDestroy()
        {
            _child.OnHidrate -= UpdateHidratationDisplay;
        }
        
        private void UpdateHidratationDisplay()
        {
            count++;
            hidratationImages[count].DOColor(Color.white, 0.25f);
        }
    }
}