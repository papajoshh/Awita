using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Infrastructure
{
    public class AudioPlayer: MonoBehaviour
    {
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource[] sfxAudioSources;

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        public void PlayMusic(AudioClip clip, float volume = 0.2f)
        {
            if (clip == null)
                return;

            if (musicAudioSource)
            {
                musicAudioSource.DOFade(0, 1f).OnComplete(() =>
                {
                    musicAudioSource.clip = clip;
                    musicAudioSource.volume = volume;
                    musicAudioSource.Play();
                    musicAudioSource.DOFade(1, 1f);
                });
            }
            musicAudioSource.clip = clip;
            musicAudioSource.Play();
        }

        public void PlaySFX(AudioClip clip, float volume = 0.2f, bool loop = false)
        {
            if (clip == null)
                return;

            var hasBeenPlayed = false;
            foreach (var sfxAudioSource in sfxAudioSources)
            {
                if (sfxAudioSource.isPlaying) continue;
                sfxAudioSource.clip = clip;
                sfxAudioSource.volume = volume;
                sfxAudioSource.loop = loop;
                sfxAudioSource.Play();
                hasBeenPlayed = true;
                return;
            }

            if (hasBeenPlayed) return;
            sfxAudioSources[0].clip = clip;
            sfxAudioSources[0].Play();
        }
        
        public void StopSFX(AudioClip clip)
        {
            foreach (var sfxAudioSource in sfxAudioSources)
            {
                if (sfxAudioSource.clip != clip) continue;
                sfxAudioSource.Stop();
            }
        }
        
        public void PlaySfxWithDelay(AudioClip clip, float volume = 0.2f, bool loop = false, float delay = 0f)
        {
            StartCoroutine(WaitToPlaySfx(clip, volume, loop, delay));
        }
        
        private IEnumerator WaitToPlaySfx(AudioClip clip, float volume, bool loop, float delay)
        {
            yield return new WaitForSeconds(delay);
            PlaySFX(clip, volume, loop);
        }
    }
}