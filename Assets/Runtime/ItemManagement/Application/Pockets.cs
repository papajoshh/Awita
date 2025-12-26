using System.Collections.Generic;
using Runtime.ItemManagement.Domain;

namespace Runtime.ItemManagement.Application
{
    public interface Pockets
    {
        void Display(List<Item> items);
        void Highlight();
    }
}