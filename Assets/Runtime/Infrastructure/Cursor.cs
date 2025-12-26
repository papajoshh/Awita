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

        private void Awake()
        {
            Instance = this;
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
            cursorImage.sprite = sprite;
        }
        
        private void ChangeSpriteOnHover()
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                var hover = hit.collider.GetComponent<Hoverable>();
                if (hover != null)
                {
                    ChangeSprite(hover.Icon);
                }
            }
            else
            {
                // Reset to default cursor sprite if needed
            }
        }
    }
}
