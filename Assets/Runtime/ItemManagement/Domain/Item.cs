using Runtime.Dialogues.Domain;
using UnityEngine;

namespace Runtime.ItemManagement.Domain
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Item")]
    public class Item: ScriptableObject
    {
        public string ID;
        public Sprite sprite;
        public DialogueData dialogue;
    }
}