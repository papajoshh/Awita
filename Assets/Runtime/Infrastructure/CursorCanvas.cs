
using Runtime.Application;
using UnityEngine;
using UnityEngine.UI;
using Cursor = Runtime.Application.Cursor;

namespace Runtime.Infrastructure
{
    public class CursorCanvas : MonoBehaviour, Cursor
    {
        [SerializeField] private Image cursorImage;
        [SerializeField] private Image itemImage;
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private Vector2 hotSpot = Vector2.zero;

        private Camera _camera;
        private Sprite currentSprite;
        
        private void Awake()
        {
            ChangeSprite(UIResources.DefaultCursor.Icon);
            _camera = Camera.main;
            itemImage.enabled = false;
        }

        private void Update()
        {
            FollowMouse();
            ChangeSpriteOnHover();
        }

        private void FollowMouse()
        {
            Vector2 mousePosition = Input.mousePosition;
            rectTransform.position = mousePosition + hotSpot;
        }

        private void ChangeSprite(Sprite sprite)
        {
            if (sprite == cursorImage.sprite) return;
            cursorImage.sprite = sprite;
        }

        private void ChangeToDefault()
        {
            
        }
        private void ChangeSpriteOnHover()
        {
            if (!_camera) return;
            var hit = Physics2D.Raycast(_camera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider)
            {
                var hover = hit.collider.GetComponent<Hoverable>();
                if (hover is { Interactable: true })
                {
                    ChangeSprite(hover.Icon);
                }
            }
            else
            {
                ChangeSprite(UIResources.DefaultCursor.Icon);
            }
        }
        
        public void ChangeToItem(Sprite sprite)
        {
            itemImage.enabled = true;
            itemImage.sprite = sprite;
        }

        public void DropItem()
        {
            itemImage.enabled = false;
        }
    }
}
