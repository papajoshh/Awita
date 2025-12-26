using System.Collections.Generic;
using System.Linq;

namespace Runtime.ItemManagement.Domain
{
    public class Inventory
    {
        public List<Item> ItemsInPockets => Items.Values.ToList();
        public bool HasSomethingOnHand => ItemOnHand != null;
        private Dictionary<string, Item> Items { get; set; }
        private Catalogue _catalogue;
        public Item ItemOnHand;
        
        public Inventory(Catalogue catalogue)
        {
            Items = new Dictionary<string, Item>();
            _catalogue = catalogue;
        }
        
        public void Add(string id)
        {
            if(Items.ContainsKey(id)) throw new System.Exception("Duplicate item");
            Items.Add(id, _catalogue.Item(id));
        }

        public void Remove(string id)
        {
            if(!Items.Remove(id)) throw new System.Exception("Item not found");
        }

        public Item Item(string id)
        {
            return Items[id];
        }

        public void Select(string id)
        {
            ItemOnHand = Item(id);
        }
        
        public void Deselect()
        {
            ItemOnHand = null;
        }
        
        public bool HasitemOnHand(string id)
        {
            return string.Equals(ItemOnHand?.ID, id);
        }

    }
}