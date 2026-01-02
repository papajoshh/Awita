using Runtime.Application;
using Runtime.Dialogues.Domain;
using Runtime.Domain;
using Runtime.ItemManagement.Application;
using Runtime.ItemManagement.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class HidrateInteractionFirstLevel: Interaction
    {
        [SerializeField] private string itemOnHand = "GlassOfWater";
        [SerializeField] private DialogueData dialogueCompleted;
        [SerializeField] private DialogueData dialogueNoItem;
        [SerializeField] private DialogueData dialogueWrongItem;
        [SerializeField] private Animator animator;
        [SerializeField] private Animator animatorKid;
        [SerializeField] private PauseAnimation pauseAnimation;
        [SerializeField] private KidPauseAnimation pauseKidAnimation;
        [SerializeField] private Interaction[] roomInteractions;
        [SerializeField] private ItemContainer[] roomitems;
        [SerializeField] private AudioClip _beber;
        
        [Inject] private readonly Child _child;
        [Inject] private readonly AudioPlayer _audioPlayer;
        [Inject] private readonly Inventory _inventory;
        [Inject] private HandleInventory _handleInventory;
        [Inject] private readonly ShowDialogue _showDialogue;

        protected override void Awake()
        {
            base.Awake();
            foreach (var interaction in roomInteractions)
            {
                interaction.Disable();
            }

            foreach (var item in roomitems)
            {
                item.Disable();
            }
        }
        public override void Interact()
        {
            if (_child.FirstLevelHidrationCompleted) return;
            if (_inventory.HasitemOnHand(itemOnHand))
            {
                HidrateChild();
            }
            else
            {
                if (_inventory.HasSomethingOnHand)
                {
                    _handleInventory.DeselectItem();
                    _showDialogue.Start(_child.GetRandomWrongPhrase());
                }
                else
                {
                    _showDialogue.Start(_child.GetRandomWrongPhrase());
                }
            }
        }

        
        private void HidrateChild()
        {
            _child.Hidrate();
            animator.Play("DrinkWater");
            animatorKid.Play("KidDrinkingAnimation");
            _showDialogue.OnShowNewLine += pauseAnimation.Resume;
            _showDialogue.OnShowNewLine += pauseKidAnimation.Resume;
            _showDialogue.Start(_child.GetPhraseOfHidratation());
            _handleInventory.RemoveItemOnHand();
            _handleInventory.AddEmptyGlass();
            foreach (var interaction in roomInteractions)
            {
                interaction.Enable();
            }

            foreach (var item in roomitems)
            {
                item.Enable();
            }
            Disable();
            gameObject.SetActive(false);
        }
        public void SkipIntro()
        {
            _handleInventory.AddGlassOfWater();
            HidrateChild();
        }
        
    }
}