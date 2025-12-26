using System.Collections.Generic;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.ItemManagement.Infrastructure
{
    public class PocketsCanvas: MonoBehaviour, Pockets
    {
        [SerializeField] private List<ItemCell> _itemCells;
        [Inject] private readonly Inventory _inventory;
        private void Awake()
        {
            Display(_inventory.ItemsInPockets);
        }

        public void Display(List<Item> items)
        {
            var auxiliarCount = 0;
            foreach (var item in items)
            {
                _itemCells[auxiliarCount].Set(item);
                auxiliarCount++;
            }
            for(; auxiliarCount < _itemCells.Count; auxiliarCount++)
            {
                _itemCells[auxiliarCount].Hide();
            }
        }

        public void Highlight()
        {
            foreach (var itemCell in _itemCells)
            {
                if (!itemCell.gameObject.activeSelf) continue;
                itemCell.HandleHighlight();
            }
        }
    }
}