using System.Collections.Generic;
using System.Linq;

namespace Runtime.ItemManagement.Domain
{
    public class Inventory
    {
        public List<Item> ItemsInPockets => Items.Values.ToList();
        private Dictionary<string, Item> Items { get; set; }
        private Catalogue _catalogue;

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
    }
}