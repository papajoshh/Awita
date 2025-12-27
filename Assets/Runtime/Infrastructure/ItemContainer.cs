using System;
using Runtime.Application;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Cursor = Runtime.Application.Cursor;

namespace Runtime.Infrastructure
{
    public class ItemContainer: MonoBehaviour, Hoverable, IPointerEnterHandler, IPointerExitHandler
    {
        [Inject] private readonly HandleInventory _controller;
        [Inject] private readonly Cursor _cursor;
        [SerializeField] private Item item;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Button button;
        private bool isOver;
        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            button = GetComponent<Button>();
            if(button) button.onClick.AddListener(OnClick);
            if (!spriteRenderer)
            {
                var image = GetComponent<Image>();
                image.sprite = item.sprite;
            }
            else
            {
                spriteRenderer.sprite = item.sprite;
            }
            
        }

        private void OnDestroy()
        {
            if(button) button.onClick.RemoveAllListeners();
        }

        private void OnClick()
        {
            if (!isOver) return;
            ExitPointer();
            RecollectItem();
        }
        public bool Interactable { get; private set; } = true;
        public Sprite GetSprite => item.sprite;
        public Sprite Icon => UIResources.Grab.Icon;
        
        private void OnMouseOver()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            RecollectItem();
        }

        private void RecollectItem()
        {
            _controller.AddItem(item.ID);
            Destroy(gameObject);
        }
        
        public void Enable() => Interactable = true;
        public void Disable() => Interactable = false;

        public void OnPointerEnter(PointerEventData eventData)
        {
            isOver = true;
            _cursor.ChangeSprite(Icon);
            _cursor.ChangeToItem(item.sprite);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExitPointer();
        }
        
        private void ExitPointer()
        {
            isOver = false;
            _cursor.ChangeToDefault(Icon);
            _cursor.DropItem(item.sprite);
        }
    }
}