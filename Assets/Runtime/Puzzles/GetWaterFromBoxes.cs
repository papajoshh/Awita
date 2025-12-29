using DG.Tweening;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class GetWaterFromBoxes:Interaction
    {
        [SerializeField] private string itemOnHandToGetWater = "EmptyGlass";
        [SerializeField] private DialogueData dialogueWaterCompleted;
        [SerializeField] private DialogueData dialogueWaterNoItem;
        [SerializeField] private DialogueData dialogueWaterWrongItem;

        [SerializeField] private SpriteRenderer boxesRenderer;
        [SerializeField] private SpriteRenderer boxesWithWaterOutRenderer;
        [SerializeField] private SpriteRenderer boxesEmptyRenderer;
        
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        //SFX
        [SerializeField] private AudioClip audio_abrir_cajas_de_carton;
        [SerializeField] private AudioClip _audioClip_getWater;
        
        [Inject] private readonly AudioPlayer _audioPlayer;

        private bool waterIsOut = false;
        protected override void Awake()
        {
            boxesRenderer.color = Color.white;
            boxesWithWaterOutRenderer.color = new Color (1, 1, 1, 0);
            boxesEmptyRenderer.color = new Color (1, 1, 1, 0);
        }
        public override void Interact()
        {
            if (!Interactable) return;
            GetWater();
            GetOutWater();
        }

        private void GetOutWater()
        {
            if (waterIsOut) return;
            boxesRenderer.DOColor(new Color(1, 1, 1, 0), 0.75f);
            boxesWithWaterOutRenderer.DOColor(Color.white, 0.75f);
            _audioPlayer.PlaySFX(audio_abrir_cajas_de_carton, 0.2f);
            waterIsOut = true;
        }
        private void GetWater()
        {
            if (!waterIsOut) return;
            if (_inventory.HasitemOnHand(itemOnHandToGetWater))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddGlassOfWater();
                _audioPlayer.PlaySFX(_audioClip_getWater, 0.2f);
                _showDialogue.Start(dialogueWaterCompleted);
                boxesWithWaterOutRenderer.DOColor(new Color(1, 1, 1, 0), 0.75f);
                boxesEmptyRenderer.DOColor(Color.white, 0.75f);
                Disable();
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    _showDialogue.Start(dialogueWaterWrongItem);
                }
                else
                {
                    _showDialogue.Start(dialogueWaterNoItem);
                }
            }
            
        }
    }
}