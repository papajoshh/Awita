using System.Collections.Generic;
using Runtime.Infrastructure;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Cursor = Runtime.Infrastructure.Cursor;

namespace Runtime
{
    public class Main: MonoBehaviour
    {
        [SerializeField] private List<Item> AllItems;
        private void Awake()
        {
            UIResources.Initialize();
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