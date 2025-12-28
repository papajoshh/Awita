using System;
using DG.Tweening;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class CasaSecaPuzzle: Interaction
    {
        [SerializeField] private string itemOnHand = "CoctelMolotov";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private Animator casaEnLlamas;
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private AudioClip sirenasClipLoop;
        [SerializeField] private AudioClip fireClipLoop;
        [SerializeField] private GetWaterFromFireFighters getWaterFromFireFighters;
        
        [Inject] private readonly AudioPlayer _audioPlayer;
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        private float timeInFlames = 0;
        private bool inFlames = false;
        private bool firefightersArrived = false;
        
        protected override void Awake()
        {
            base.Awake();
            getWaterFromFireFighters.Disable();
        }
        private void Update()
        {
            if(inFlames) timeInFlames += Time.deltaTime;
            if (timeInFlames > 10) FirefightersArrive();
        }

        public override void Interact()
        {
            if (!Interactable) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddItem("Red");
                _audioPlayer.PlaySFX(audioClip, 0.5f);
                _audioPlayer.PlaySfxWithDelay(sirenasClipLoop, 0.2f, true, 9f);
                _audioPlayer.PlaySfxWithDelay(fireClipLoop, 0.2f, true, 9f);
                _showDialogue.Start(dialogueCompleted);
                casaEnLlamas.Play("Llamas");
                inFlames = true;
                Disable();
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    _showDialogue.Start(dialogueWrongItem);
                }
                else
                {
                    _showDialogue.Start(dialogueNoItem);
                }
            }
        }
        
        private void FirefightersArrive()
        {
            getWaterFromFireFighters.Enable();
        }
    }
}