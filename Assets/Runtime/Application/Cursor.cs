using UnityEngine;

namespace Runtime.Application
{
    public interface Cursor
    {
        void ChangeToItem(Sprite sprite);
        void DropItem();
    }
}