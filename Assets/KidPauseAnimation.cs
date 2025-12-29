using System;
using UnityEngine;

public class KidPauseAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public event Action OnEnded;

    public static bool cacheResume;

    private void Awake()
    {
        cacheResume = false;
    }

    private void Update()
    {
        if (!cacheResume) return;
        Resume();
    }


    public void Pause()
    {
        _animator.speed = 0;
    }

    public void Resume()
    {
        if (cacheResume) cacheResume = false;
        _animator.speed = 1;
    }

    public void Ended()
    {
        OnEnded?.Invoke();
    }
}