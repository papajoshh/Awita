using DG.Tweening;
using UnityEngine;

namespace Runtime.Infrastructure
{
    public class AudioPlayer: MonoBehaviour
    {
        [SerializeField] private AudioSource musicAudioSource;
        [SerializeField] private AudioSource[] sfxAudioSources;
        
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

        public void PlaySfx(AudioClip clip, float volume = 0.2f)
        {
            if (clip == null)
                return;

            var hasBeenPlayed = false;
            foreach (var sfxAudioSource in sfxAudioSources)
            {
                if (sfxAudioSource.isPlaying) continue;
                sfxAudioSource.clip = clip;
                sfxAudioSource.volume = volume;
                sfxAudioSource.Play();
                hasBeenPlayed = true;
                return;
            }

            if (hasBeenPlayed) return;
            sfxAudioSources[0].clip = clip;
            sfxAudioSources[0].Play();
        }

    }
}