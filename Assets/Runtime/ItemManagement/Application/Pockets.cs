using System.Collections.Generic;
using Runtime.ItemManagement.Domain;

namespace Runtime.ItemManagement.Application
{
    public interface Pockets
    {
        void Update(List<Item> items);
    }
}