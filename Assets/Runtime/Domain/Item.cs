using UnityEngine;

namespace Runtime.Domain
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
    public class Item: ScriptableObject
    {
        public Sprite sprite;
    }
}