using UnityEngine;

namespace Runtime.ItemManagement.Domain
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item: ScriptableObject
    {
        public string ID;
        public Sprite sprite;
    }
}