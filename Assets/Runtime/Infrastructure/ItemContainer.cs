using Runtime.Application;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;

namespace Runtime.Infrastructure
{
    public class ItemContainer: MonoBehaviour, Hoverable
    {
        [SerializeField] private Item item;
        public Sprite GetSprite => item.sprite;
        public Sprite Icon => UIResources.Grab.Icon;
        
        private void OnMouseOver()
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleInventory.AddItem(item.ID);
                Destroy(gameObject);
            }
        }
    }
}