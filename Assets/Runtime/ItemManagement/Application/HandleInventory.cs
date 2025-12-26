using Runtime.ItemManagement.Domain;
using UnityEngine;

namespace Runtime.ItemManagement.Application
{
    public static class HandleInventory
    {
        private static Inventory _inventory;
        private static Pockets pockets => _cachePockets ??= GameObject.FindGameObjectWithTag("PocketCanvas").GetComponent<Pockets>();

        private static Pockets _cachePockets;
        public static void Initialize(Inventory inventory)
        {
            _inventory = inventory;
        }

        public static void AddItem(string id)
        {
            _inventory.Add(id);
            pockets.Update(_inventory.ItemsInPockets);
        }

        public static void RemoveItem(string id)
        {
            _inventory.Remove(id);
            pockets.Update(_inventory.ItemsInPockets);
        }
    }
}