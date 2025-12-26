using System.Collections.Generic;
using System.Linq;
using Runtime.Domain;
using Runtime.Infrastructure;
using Runtime.ItemManagement.Domain;
using UnityEngine;

namespace Runtime
{
    public class Main: MonoBehaviour
    {
        private List<Item> AllItems;
        private void Awake()
        {
            UIResources.Initialize();
            
            AllItems = Resources.LoadAll<Item>("Items").ToList();
            Child.NewBorn();
            var catalogue = new Catalogue(AllItems);
            var inventory = new Inventory(catalogue);
        }

        private void Start()
        {
            //Cursor.Instance.Initialize();
        }
    }
}