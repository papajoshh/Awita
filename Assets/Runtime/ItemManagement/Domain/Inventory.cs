using System.Collections.Generic;

namespace Runtime.ItemManagement.Domain
{
    public static class Inventory
    {
        private static Dictionary<string, Item> Items { get; set; }
        private static Catalogue _catalogue;
        
        public static void Initialize(Catalogue catalogue)
        {
            Items = new Dictionary<string, Item>();
            _catalogue = catalogue;
        }

        public static void AddItem(string id)
        {
            if(Items.ContainsKey(id)) throw new System.Exception("Duplicate item");
            Items.Add(id, _catalogue.Item(id));
        }

        public static void RemoveItem(string id)
        {
            if(!Items.Remove(id)) throw new System.Exception("Item not found");
        }

        public static Item GetItem(string id)
        {
            return Items[id];
        }
    }
}