using System.Collections.Generic;
using System.Linq;
using Runtime.Infrastructure;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Cursor = Runtime.Infrastructure.Cursor;

namespace Runtime
{
    public class Main: MonoBehaviour
    {
        private List<Item> AllItems;
        private void Awake()
        {
            UIResources.Initialize();
            
            AllItems = Resources.LoadAll<Item>("Items").ToList();
            var catalogue = new Catalogue(AllItems);
            var inventory = new Inventory(catalogue);
            
            HandleInventory.Initialize(inventory);
        }

        private void Start()
        {
            Cursor.Instance.Initialize();
        }
    }
}