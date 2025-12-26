using System.Collections.Generic;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;

namespace Runtime.ItemManagement.Infrastructure
{
    public class PocketsCanvas: MonoBehaviour, Pockets
    {
        [SerializeField] private List<ItemCell> _itemCells;
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
    }
}