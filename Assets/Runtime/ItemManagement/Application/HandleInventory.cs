using Runtime.Application;
using Runtime.Infrastructure;
using Runtime.ItemManagement.Domain;
using Zenject;
using static UnityEditor.Progress;

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

            if(string.Equals(id, "GlassFullOfWater"))
            {
                SelectItem(id);
            }
        }

        private void RemoveItem(string id)
        {
            _inventory.Remove(id);
            _pockets.Display(_inventory.ItemsInPockets);
        }
        
        public void RemoveItemOnHand()
        {
            var id = _inventory.ItemOnHand.ID;
            RemoveItem(id);
            DeselectItem();
        }
        
        public void SelectItem(string id)
        {
            _inventory.Select(id);
            _pockets.Highlight();
            _cursor.ChangeToItem(_inventory.ItemOnHand.sprite);
        }
        
        public void DeselectItem()
        {
            _inventory.Deselect();
            _pockets.Highlight();
            _cursor.DropItem();
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

        public void AddGlassOfWater()
        {
            AddItem("GlassFullOfWater");
        }

        public void AddEmptyGlass()
        {
            AddItem("EmptyGlass");
        }
        public void AddCoctelMolotov()
        {
            AddItem("CoctelMolotov");
        }
        public void AddBotellaDeAlcoholConPapel()
        {
            AddItem("BotellaDeAlcoholConPapel");
        }

        public void AddRed()
        {
            AddItem("Red");
        }

        public void AddAlcohol()
        {
            AddItem("Alcohol");
        }

        public void AddCassette()
        {
            AddItem("Cassette");
        }

        public void AddCortapizzas()
        {
            AddItem("Cortapizzas");
        }

        public void AddDrawerKey()
        {
            AddItem("DrawerKey");
        }

        public void AddHielo()
        {
            AddItem("Hielo");
        }
        
        public void AddLimpiaplatos()
        {
            AddItem("Limpiaplatos");
        }

        public void AddPalo()
        {
            AddItem("Palo");
        }

        public void AddPincitas()
        {
            AddItem("Pincitas");
        }
        
        public void AddQuesito()
        {
            AddItem("Quesito");
        }
        
        public void AddTijerasPodar()
        {
            AddItem("TijerasPodar");
        }

        public bool HasWhiskyOnHand()
        {
            return _inventory.HasitemOnHand("Alcohol");
        }

        public bool HasIceOnHand()
        {
            return _inventory.HasitemOnHand("Hielo");
        }
        public bool HassGlassOfWater()
        {
            return _inventory.HasitemOnHand("GlassFullOfWater");
        }
    }
}