using System.Collections;
using Runtime.Application;
using Runtime.Dialogues.Domain;
using UnityEngine;
using Zenject;

namespace Runtime.Infrastructure
{
    public class LoopGame: MonoBehaviour
    {
        [Inject] private readonly ShowDialogue _dialogue;
        
        [SerializeField] private DialogueData startingDialogue;
        private void Start()
        {
            StartCoroutine(StartingDialogue());
        }
        
        private IEnumerator StartingDialogue()
        {
            yield return new WaitForSeconds(1f);
            _dialogue.Start(startingDialogue);
        }
        
    }
}