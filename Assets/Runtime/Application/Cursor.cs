using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Application
{
    public interface Cursor
    {
        void ChangeToDefault(Sprite sprite);
        void ChangeSprite(Sprite sprite);
        void ChangeToItem(Sprite sprite);
        void DropItem();
        void DropItem(Sprite sprite);
        EventSystem EventSystem { get; }
    }
}