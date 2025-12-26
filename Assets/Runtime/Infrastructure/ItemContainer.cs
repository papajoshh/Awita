using Runtime.Application;
using Runtime.Domain;
using UnityEngine;

namespace Runtime.Infrastructure
{
    public class ItemContainer: MonoBehaviour, Hoverable
    {
        private Item item;
        public Sprite GetSprite => item.sprite;
        public Sprite Icon => UIResources.Grab.Icon;
    }
}