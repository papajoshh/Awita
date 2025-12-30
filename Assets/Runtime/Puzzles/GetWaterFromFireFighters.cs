using System.Collections;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Runtime.Infrastructure
{
    public class GetWaterFromFireFighters: Interaction
    {
        [SerializeField] private string itemOnHand = "EmptyGlass";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private Animator fireAnimator;
        [SerializeField] private GameObject fire;
        [SerializeField] private SpriteRenderer houseRenderer;
        [SerializeField] private Sprite calcinadaHouseSprite;
        
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        //SFX
        [SerializeField] private AudioClip _audioClip_getWater;
        [FormerlySerializedAs("sirenasClip")] [SerializeField] private AudioClip sirenasLoopClip;
        [SerializeField] private AudioClip fireLoopClip;
        [Inject] private readonly AudioPlayer _audioPlayer;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopAllCoroutines();
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
                fire.SetActive(false);
                _audioPlayer.StopSFX(fireLoopClip);
                houseRenderer.sprite = calcinadaHouseSprite;
                StartCoroutine(FirefightersGoOut());
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

        private IEnumerator FirefightersGoOut()
        {
            yield return new WaitForSeconds(5f);
            _audioPlayer.StopSFX(sirenasLoopClip, true);
            fireAnimator.Play("Calcinada");
        }
    }
}