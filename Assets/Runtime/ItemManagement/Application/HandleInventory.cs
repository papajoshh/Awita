using Runtime.ItemManagement.Domain;
using Cursor = Runtime.Infrastructure.Cursor;

namespace Runtime.ItemManagement.Application
{
    public class HandleInventory
    {
        private readonly Inventory _inventory;
        private readonly Pockets _pockets;
        private readonly Cursor _cursor;
        
        public HandleInventory(Inventory inventory, Pockets pockets, Cursor cursor)
        {
            _inventory = inventory;
            _pockets = pockets;
            _cursor = cursor;
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
        
        public void SelectItem(string id)
        {
            _inventory.Select(id);
            _pockets.Highlight();
            //_cursor
        }
        
        public void DeselectItem()
        {
            _inventory.Deselect();
            _pockets.Highlight();
            //_cursor.Clear();
        }

        public void ToogleItem(string id)
        {
            if (_inventory.HasitemOnHand(id))
            {
                DeselectItem();
            }
            else
            {
                SelectItem(id);
            }
        }
    }
}