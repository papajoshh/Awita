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
using Runtime.Application;
using Runtime.ExtraInteraction.Application;
using Runtime.ExtraInteraction.Domain;
using Cursor = Runtime.Application.Cursor;

namespace Runtime
{
    public class MainInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            UIResources.Initialize();
            Container.Bind<CurrentDialogue>().AsSingle();
            Container.Bind<Child>().FromInstance(Child.NewBorn()).AsSingle();
            Container.Bind<CursorCanvas>().FromComponentInHierarchy().AsSingle();
            var allItems = Resources.LoadAll<Item>("Items").ToList();
            var allExtraPuzzles = Resources.LoadAll<ExtraInteractionPuzzle>("ExtraInteractionPuzzles").ToList();
            Container.Bind<List<Item>>().FromInstance(allItems).AsSingle();
            Container.Bind<List<ExtraInteractionPuzzle>>().FromInstance(allExtraPuzzles).AsSingle();
            
            Container.Bind<ExtraInteractionPuzzleCatalogue>().AsSingle();
            Container.Bind<Catalogue>().AsSingle();
            Container.Bind<Inventory>().AsSingle();
            
            Container.Bind<HandleInventory>().AsSingle();
            Container.Bind<ShowDialogue>().AsSingle();
            Container.Bind<ShowPopupInteraction>().AsSingle();
            Container.Bind<TransitionToRoomCanvas>().FromComponentInHierarchy().AsSingle();
            Container.Bind<TravelButtonsCanvas>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Pockets>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Dialogue>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ExtraInteractionPopup>().FromComponentInHierarchy().AsSingle();
            Container.Bind<Cursor>().FromComponentInHierarchy().AsSingle();
        }
    }
}