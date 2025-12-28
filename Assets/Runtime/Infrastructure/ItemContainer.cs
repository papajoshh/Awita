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

        [Inject] private readonly ShowDialogue _showDialogue;
        private Button button;
        private bool isOver;
        private bool glassWaterShowed = false;
        private Collider2D collider
        {
            get
            {
                if(cacheCollider == null)
                    cacheCollider = GetComponent<Collider2D>();
                return cacheCollider;
            }
        }

        private Collider2D cacheCollider;

        protected virtual void Awake()
        {
            
        }
        protected virtual void Start()
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
            if (!isOver || !Interactable) return;
            ExitPointer();
            RecollectItem();
        }
        public bool Interactable { get; private set; } = true;
        public Sprite GetSprite => item.sprite;
        public Sprite Icon => UIResources.Grab.Icon;
        
        private void OnMouseOver()
        {
            if (!Interactable || !Input.GetMouseButtonDown(0)) return;
            RecollectItem();
        }

        private void RecollectItem()
        {
            _controller.AddItem(item.ID);
            if (string.Equals(item.ID, "GlassFullOfWater"))
            {
                if(!glassWaterShowed)_showDialogue.Start(item.dialogue);
                glassWaterShowed = true;
            }
            else if (!string.Equals(item.ID, "EmptyGlass"))
            {
                _showDialogue.Start(item.dialogue);
            }
            Destroy(gameObject);
        }
        
        public void Enable()
        {
            Interactable = true;
            collider.enabled = true;
        }

        public void Disable()
        {
            Interactable = false;
            collider.enabled = false;
        } 

        public void OnPointerEnter(PointerEventData eventData)
        {
            isOver = true;
            if (!Interactable) return;
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