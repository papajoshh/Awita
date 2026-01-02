using DG.Tweening;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class FregaderoPuzzle:Interaction
    {
        [SerializeField] private string itemOnHand = "Limpiaplatos";
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        
        [SerializeField] private DialogueData dialogueSecondCompleted;
        [SerializeField] private DialogueData dialogueSecondNoItem;
        [SerializeField] private DialogueData dialogueSecondWrongItem;

        [SerializeField] private GameObject cleanDishes;
        [SerializeField] private SpriteRenderer initialPlatosSuciosRenderer;
        [SerializeField] private SpriteRenderer platosJabonososRenderer;
        [SerializeField] private SpriteRenderer cleanPlatosRenderer;
        [SerializeField] private AudioClip cleanAudio;
        
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;
        [Inject] private readonly AudioPlayer _audioPlayer;

        private bool absolutelyClean = false;
        private int numberOfCleans = 0;
        
        protected override void Awake()
        {
            initialPlatosSuciosRenderer.color = Color.white;
            platosJabonososRenderer.color = new Color(1, 1, 1, 0);
            cleanPlatosRenderer.color = new Color(1, 1, 1, 0);
            cleanDishes.SetActive(false);
        }
        public override void Interact()
        {
            if (!Interactable) return;
            CleanDishes();
        }

        private void CleanDishes()
        {
            if (absolutelyClean) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                Clean();
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    if (numberOfCleans == 0)
                    {
                        _handleInventory.DeselectItem();
                        _showDialogue.Start(dialogueWrongItem);
                    }
                    else
                    {
                        _showDialogue.Start(dialogueSecondWrongItem);
                    }
                    
                }
                else
                {
                    if (numberOfCleans == 0)
                    {
                        _showDialogue.Start(dialogueNoItem);
                    }
                    else
                    {
                        _showDialogue.Start(dialogueSecondNoItem);
                    }
                    
                }
            }
        }
        private void Clean()
        {
            numberOfCleans++;
            _audioPlayer.PlaySFX(cleanAudio);
            if (numberOfCleans <= 1)
            {
                initialPlatosSuciosRenderer.DOColor(new Color(1, 1, 1, 0), 0.5f);
                platosJabonososRenderer.DOColor(Color.white, 0.5f);
            }
            else
            {
                absolutelyClean = true;
                platosJabonososRenderer.DOColor(new Color(1, 1, 1, 0), 0.5f);
                cleanPlatosRenderer.DOColor(Color.white, 0.5f);
            }

            if (!absolutelyClean) return;
            _showDialogue.Start(dialogueSecondCompleted);
            _handleInventory.RemoveItemOnHand();
            cleanDishes.SetActive(true);
            Disable();
        }
    }
}