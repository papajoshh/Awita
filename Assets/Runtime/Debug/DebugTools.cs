using Runtime.Infrastructure;
using Runtime.ItemManagement.Application;
using UnityEngine;
using Zenject;

namespace Runtime.Debug
{
    public class DebugTools:MonoBehaviour
    {
        [Inject] private readonly TransitionToRoomCanvas transitionToRoomCanvas;
        [Inject] private readonly HandleInventory _handleInventory;
        private void Update()
        {
            #if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.Alpha1)) transitionToRoomCanvas.GoToRoom();
            if(Input.GetKeyDown(KeyCode.Alpha2)) transitionToRoomCanvas.GoToBathroom();
            if(Input.GetKeyDown(KeyCode.Alpha3)) transitionToRoomCanvas.GoToKitchen();
            
            HandleGetInventory();
            if(Input.GetKeyDown(KeyCode.Space)) SkipIntro();
            #endif
        }

        private void SkipIntro()
        {
            FindAnyObjectByType<HidrateInteractionFirstLevel>().SkipIntro();
        }
        private string _nameEntered = "";

        private bool clearWord;
        private void HandleGetInventory()
        {
            HandlePWords();
            HandleLWords();
            HandleVWords();
            HandleCWords();
            HandleSimpleWords();
            if (!clearWord) return;
            clearWord = false;
            _nameEntered = "";
        }

        private void HandleSimpleWords()
        {
            if (!string.IsNullOrEmpty(_nameEntered)) return;
            
            if(Input.GetKeyDown(KeyCode.A))    
            {
                _handleInventory.AddAlcohol();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                _handleInventory.AddBotellaDeAlcoholConPapel();
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                _nameEntered += "C";
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                _nameEntered += "L";
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                _handleInventory.AddEmptyGlass();
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                _handleInventory.AddHielo();
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                _nameEntered += "P";
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                _handleInventory.AddQuesito();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                _handleInventory.AddRed();
            }
            if (Input.GetKeyDown(KeyCode.T))
            {
                _handleInventory.AddTijerasPodar();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                _nameEntered += "V";
            }
        }
        private void HandleCWords()
        {
            if(string.IsNullOrEmpty(_nameEntered)) return;
            if (_nameEntered[0] != 'C') return;
            if (Input.GetKeyDown(KeyCode.A))
            {
                _handleInventory.AddCassette();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                _handleInventory.AddCortapizzas();
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                _handleInventory.AddCoctelMolotov();
            }

            clearWord = Input.anyKeyDown;
        }
        private void HandleVWords()
        {
            if(string.IsNullOrEmpty(_nameEntered)) return;
            if (_nameEntered[0] != 'V') return;
            if (Input.GetKeyDown(KeyCode.A))
            {
                _handleInventory.AddGlassOfWater();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                _handleInventory.AddEmptyGlass();
            }
            else if (Input.GetKeyDown(KeyCode.L))
            {
                _handleInventory.AddGlassOfWater();
            }
            clearWord = Input.anyKeyDown;
        }
        private void HandleLWords()
        {
            if(string.IsNullOrEmpty(_nameEntered)) return;
            if (_nameEntered[0] != 'L') return;
            if (Input.GetKeyDown(KeyCode.L))
            {
                _handleInventory.AddDrawerKey();
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                _handleInventory.AddLimpiaplatos();
            }
            clearWord = Input.anyKeyDown;
        }
        private void HandlePWords()
        {
            if(string.IsNullOrEmpty(_nameEntered)) return;
            if (_nameEntered[0] != 'P') return;
            if (Input.GetKeyDown(KeyCode.A))
            {
                _handleInventory.AddPalo();
            }
            else if (Input.GetKeyDown(KeyCode.I))
            {
                _handleInventory.AddPincitas();
            }
            clearWord = Input.anyKeyDown;
        }
    }
}