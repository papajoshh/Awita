using System.Collections.Generic;
using System.Linq;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
using Runtime.Infrastructure;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime
{
    public class MainInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            UIResources.Initialize();
            Container.Bind<CurrentDialogue>().AsSingle();
            Container.Bind<Child>().FromInstance(Child.NewBorn()).AsSingle();
            
            var allItems = Resources.LoadAll<Item>("Items").ToList();
            Container.Bind<List<Item>>().FromInstance(allItems).AsSingle();

            Container.Bind<Catalogue>().AsSingle();
            Container.Bind<Inventory>().AsSingle();
            
            Container.Bind<HandleInventory>().AsSingle();
            Container.Bind<ShowDialogue>().AsSingle();

            Container.Bind<Pockets>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Dialogue>().FromComponentInHierarchy().AsSingle();
        }
    }
}