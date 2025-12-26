using System;
using Runtime.Application;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Infrastructure
{
    public class Cursor : MonoBehaviour
    {
        public static Cursor Instance { get; private set; }
        [SerializeField] private Image cursorImage;
        [SerializeField] private Vector2 hotSpot = Vector2.zero;

        private Camera _camera;
        private Sprite currentSprite;
        private void Awake()
        {
            Instance = this;
            ChangeSprite(UIResources.DefaultCursor.Icon);
            _camera = Camera.main;
        }

        private void Update()
        {
            FollowMouse();
            ChangeSpriteOnHover();
        }

        private void FollowMouse()
        {
            Vector2 mousePosition = Input.mousePosition;
            cursorImage.rectTransform.position = mousePosition + hotSpot;
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
                if (hover != null)
                {
                    ChangeSprite(hover.Icon);
                }
            }
            else
            {
                ChangeSprite(UIResources.DefaultCursor.Icon);
            }
        }
    }
}
