using Runtime.ItemManagement.Domain;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.ItemManagement.Infrastructure
{
    public class ItemCell: MonoBehaviour
    {
        [SerializeField] private Image _image;
        
        public void Set(Item item)
        {
            Show();
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
    }
}