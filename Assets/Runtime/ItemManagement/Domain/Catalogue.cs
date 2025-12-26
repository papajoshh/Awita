using System.Collections.Generic;
using System.Linq;

namespace Runtime.ItemManagement.Domain
{
    public class Catalogue
    {
        public Dictionary<string, Item> Items { get; private set; }
        
        public Catalogue(List<Item> items)
        {
            Items = items.ToDictionary(item => item.ID, item => item);
        }
        
        public Item Item(string id)
        {
            if (Items.TryGetValue(id, out var item))
            {
                return item;
            }
            throw new KeyNotFoundException($"Item with ID {id} not found in catalogue.");
        }
    }
}