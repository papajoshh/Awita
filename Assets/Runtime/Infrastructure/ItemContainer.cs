using Runtime.Application;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class ItemContainer: MonoBehaviour, Hoverable
    {
        [Inject] private readonly HandleInventory _controller;
        [SerializeField] private Item item;
        public Sprite GetSprite => item.sprite;
        public Sprite Icon => UIResources.Grab.Icon;
        
        private void OnMouseOver()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            _controller.AddItem(item.ID);
            Destroy(gameObject);
        }
    }
}