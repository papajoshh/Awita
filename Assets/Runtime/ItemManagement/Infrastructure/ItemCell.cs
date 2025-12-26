using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.ItemManagement.Infrastructure
{
    public class ItemCell: MonoBehaviour
    {
        [Inject] private readonly Inventory _inventory;
        [Inject] private readonly HandleInventory _handleInventory;
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [SerializeField] private Image _highlight;

        private Item _item;
        private bool HasThisItem(string id) => string.Equals(_item.ID, id);
        
        private void Awake()
        {
            _button.onClick.AddListener(SelectItem);
            _highlight.enabled = false;
        }

        private void SelectItem()
        {
            _handleInventory.ToogleItem(_item.ID);
        }

        public void Set(Item item)
        {
            Show();
            _item = item;
            _image.sprite = item.sprite;
        }

        private void Show()
        {
            gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void HandleHighlight()
        {
            _highlight.enabled = _inventory.HasitemOnHand(_item.ID);
        }
    }
}