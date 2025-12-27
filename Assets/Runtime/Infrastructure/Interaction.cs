using System;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;
using Cursor = Runtime.Application.Cursor;

namespace Runtime.Infrastructure
{
    public abstract class Interaction: MonoBehaviour, Hoverable, IPointerEnterHandler, IPointerExitHandler
    {
        [Inject] private readonly CurrentDialogue _currentDialogue;
        [Inject] private readonly Cursor _cursor;
        public bool Interactable { get; private set; } = true;
        public virtual Sprite Icon => UIResources.Interact.Icon;

        private Button button;
        private bool isOver;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            if (button) button.onClick.AddListener(OnClick);
        }
        
        protected void OnDestroy()
        {
            if (button) button.onClick.RemoveAllListeners();
        }

        private void OnClick()
        {
            if (!isOver || !Interactable) return;
            Interact();
        }
        public void OnMouseOver()
        {
            if (!_currentDialogue.Hid) return;
            if (Input.GetMouseButton(0)) Interact();
        }
        
        public void Enable() => Interactable = true;
        public void Disable() => Interactable = false;

        public abstract void Interact();
        public void OnPointerEnter(PointerEventData eventData)
        {
            isOver = true;
            if (!Interactable) return;
            _cursor.ChangeSprite(Icon);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ExitPointer();
        }
        
        private void  ExitPointer()
        {
            isOver = false;
            _cursor.ChangeToDefault(Icon);
        }
    }
}