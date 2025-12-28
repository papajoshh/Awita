using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class OpenLockedDrawer: Interaction
    {
        [SerializeField] private string neededItem = "";
        [SerializeField] private GameObject openedDrawer;
        [SerializeField] private GameObject closedDrawer;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private bool startClosed = true;
        
        [Inject] private readonly ShowDialogue _showDialogue;
        [Inject] private readonly Inventory _inventory;
        [Inject] private readonly HandleInventory _handleInventory;

        //SFX
        [SerializeField] private AudioClip _audioClip_key;
        [SerializeField] private AudioClip _audioClip_openDrawer;
        [SerializeField] private AudioClip _audioClip_closeDrawer;
        [Inject] private readonly AudioPlayer _audioPlayer;

        private bool unlocked;
        
        private bool closed;
        protected override void Awake()
        {
            base.Awake();
            if (startClosed)
            {
                Close();
            }
            else
            {
                Open();
            }
        }
        public override void Interact()
        {
            if(!Interactable) return;
            if (!unlocked)
            {
                Unlock();
            }
            else
            {
                Toggle();
            }
        }
        
        private void Unlock()
        {
            if (_inventory.HasitemOnHand(neededItem))
            {
                UnlockDrawer();
            }
            else
            {
                ShowNoUnlockDialogue();
            }
            
        }

        private void UnlockDrawer()
        {
            unlocked = true;
            _handleInventory.RemoveItemOnHand();
            _audioPlayer.PlaySFX(_audioClip_key, 0.2f);
            Toggle();
        }

        private void ShowNoUnlockDialogue()
        {
            _showDialogue.Start(_inventory.HasSomethingOnHand ? dialogueWrongItem : dialogueNoItem);
        }

        private void Toggle()
        {
            if(closed) Open();
            else Close();
        }
        
        private void OnDisable()
        {
            closed = true;
            closedDrawer.SetActive(true);
            openedDrawer.SetActive(false);
        }

        private void Close()
        {
            closed = true;
            closedDrawer.SetActive(true);
            openedDrawer.SetActive(false);
            _audioPlayer.PlaySFX(_audioClip_closeDrawer, 0.2f);
        }

        private void Open()
        {
            closed = false;
            closedDrawer.SetActive(false);
            openedDrawer.SetActive(true);
            _audioPlayer.PlaySFX(_audioClip_openDrawer, 0.2f);
        }
    }
}