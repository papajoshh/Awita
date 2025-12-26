using Runtime.ItemManagement.Domain;
using UnityEngine;

namespace Runtime.ItemManagement.Application
{
    public class HandleInventory
    {
        private readonly Inventory _inventory;
        private readonly Pockets _pockets;
        
        public HandleInventory(Inventory inventory, Pockets pockets)
        {
            _inventory = inventory;
            _pockets = pockets;
        }

        public void AddItem(string id)
        {
            _inventory.Add(id);
            _pockets.Display(_inventory.ItemsInPockets);
        }

        public void RemoveItem(string id)
        {
            _inventory.Remove(id);
            _pockets.Display(_inventory.ItemsInPockets);
        }
    }
}