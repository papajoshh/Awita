using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.Infrastructure
{
    public class RandomAnimationsKid: MonoBehaviour
    {
        [SerializeField] private SpriteRenderer eyesRenderer;
        [SerializeField] private SpriteRenderer mouthRenderer;
        
        [SerializeField] private Sprite openEyesSprite;
        [SerializeField] private Sprite closedEyesSprite;
        [SerializeField] private Sprite openMouthSprite;
        [SerializeField] private Sprite closedMouthSprite;


        private void Start()
        {
            StartCoroutine(Blink());
            StartCoroutine(OpenCloseMouth());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        private IEnumerator Blink()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0.1f,10f));
                eyesRenderer.sprite = closedEyesSprite;
                yield return new WaitForSeconds(Random.Range(0.1f,0.5f));
                eyesRenderer.sprite = openEyesSprite;
            }
        }
        
        private IEnumerator OpenCloseMouth()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(0.1f,10f));
                mouthRenderer.sprite = closedMouthSprite;
                yield return new WaitForSeconds(Random.Range(0.1f,2f));
                mouthRenderer.sprite = openMouthSprite;
            }
        }
    }
}