using System.Collections;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
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
        [Inject] private readonly Child _child;

        //SFX
        [SerializeField] private AudioClip _audioClip_getWater;
        [FormerlySerializedAs("sirenasClip")] [SerializeField] private AudioClip sirenasLoopClip;
        [SerializeField] private AudioClip fireLoopClip;
        [Inject] private readonly AudioPlayer _audioPlayer;

        private bool isDone;
        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopAllCoroutines();
        }
        public override void Interact()
        {
            if (!Interactable) return;
            if (isDone)
            {
                _showDialogue.Start(_child.GetPhraseOfDryPlace());
                return;
            }
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                _handleInventory.RemoveItemOnHand();
                _handleInventory.AddGlassOfWater();
                _audioPlayer.PlaySFX(_audioClip_getWater, 0.2f);
                _showDialogue.Start(dialogueCompleted);
                FirefightersGoOut();
                StartCoroutine(StartToExtinguishFire());
                Disable();
            }
            else
            {
                if (_handleInventory.HasGlassOfWater())
                {
                    _showDialogue.Start(_child.GetPhraseOfWaterOnGlass());
                    return;
                }
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

        private IEnumerator StartToExtinguishFire()
        {
            yield return new WaitForSeconds(5f);
            fire.SetActive(false);
            _audioPlayer.StopSFX(fireLoopClip);
            houseRenderer.sprite = calcinadaHouseSprite;
        }
        private void FirefightersGoOut()
        {
            _audioPlayer.StopSFX(sirenasLoopClip, true);
            fireAnimator.Play("Calcinada");
        }
    }
}