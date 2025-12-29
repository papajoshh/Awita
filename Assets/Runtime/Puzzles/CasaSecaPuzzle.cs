using System.Collections;
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
        [SerializeField] private GameObject fire;
        
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

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopAllCoroutines();
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
                _audioPlayer.PlaySFX(audioClip, 0.5f);
                _audioPlayer.PlaySfxWithDelay(sirenasClipLoop, 0.2f, true, 15f);
                _audioPlayer.PlaySfxWithDelay(fireClipLoop, 0.2f, true, 15f);
                _showDialogue.Start(dialogueCompleted);
                Disable();
                StartCoroutine(ThrowCoctel());
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

        private IEnumerator ThrowCoctel()
        {
            yield return new WaitForSeconds(1f);
            casaEnLlamas.Play("Fire");
            fire.SetActive(true);
            inFlames = true;
        }
    }
}