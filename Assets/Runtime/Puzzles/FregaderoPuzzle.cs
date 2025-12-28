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

        [SerializeField] private ItemContainer cortapizzas;
        [SerializeField] private SpriteRenderer fregaPlatosRenderer;
        [SerializeField] private Sprite initialPlatosSuciosSprite;
        [SerializeField] private Sprite platosJabonososSprite;
        [SerializeField] private Sprite cleanPlatosSprite;
        [SerializeField] private AudioClip cleanAudio;
        
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;
        [Inject] private readonly AudioPlayer _audioPlayer;

        private bool absolutelyClean = false;
        private int numberOfCleans = 0;
        
        protected override void Awake()
        {
            fregaPlatosRenderer.sprite = initialPlatosSuciosSprite;
            cortapizzas.Disable();
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
            _audioPlayer.PlaySfx(cleanAudio);
            if (numberOfCleans <= 1)
            {
                fregaPlatosRenderer.sprite = platosJabonososSprite;
            }
            else
            {
                absolutelyClean = true;
                fregaPlatosRenderer.sprite = cleanPlatosSprite;
            }

            if (!absolutelyClean) return;
            _showDialogue.Start(dialogueSecondCompleted);
            _handleInventory.RemoveItemOnHand();
            cortapizzas.Enable();
            Disable();
        }
    }
}