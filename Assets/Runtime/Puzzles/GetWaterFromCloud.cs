using DG.Tweening;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class GetWaterFromCloud: Interaction
    {
        [SerializeField] private string itemOnHand = "EmptyGlass";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private SpriteRenderer cloudRenderer;
        [SerializeField] private ExtractorInteraction extractor;
        [SerializeField] private GameObject lluvia;
        
        
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        //SFX
        [SerializeField] private AudioClip _audioClip_getWater;
        [SerializeField] private AudioClip _rainingAudio;
        [Inject] private readonly AudioPlayer _audioPlayer;

        protected override void Awake()
        {
            base.Awake();
            lluvia.SetActive(false);
        }
        public override void Interact()
        {
            if (!Interactable) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddGlassOfWater();
                _audioPlayer.PlaySFX(_audioClip_getWater, 0.2f);
                _showDialogue.Start(dialogueCompleted);
                extractor.Open();
                extractor.Disable();
                StopRaining();
                Disable();
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    _handleInventory.DeselectItem();
                    _showDialogue.Start(dialogueWrongItem);
                }
                else
                {
                    _showDialogue.Start(dialogueNoItem);
                }
            }
        }

        public void StartRaining()
        {
            lluvia.SetActive(true);
            _audioPlayer.PlaySFX(_rainingAudio, 0.2f, true);
            Enable();
        }

        private void StopRaining()
        {
            cloudRenderer.DOColor(new Color(1, 1, 1, 0), 2f);
            lluvia.SetActive(false);
            _audioPlayer.StopSFX(_rainingAudio);
        }
    }
}