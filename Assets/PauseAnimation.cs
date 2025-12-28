using System;
using UnityEngine;

public class PauseAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public event Action OnEnded;

    public static bool cacheResume;

    private void Awake()
    {
        cacheResume = false;
    }

    public void Pause()
    {
       /* if (cacheResume)
        {
            _animator.speed = 1;
            cacheResume = false;
            return;
        }*/
        _animator.speed = 0;
    }

    public void Resume()
    {
        if(cacheResume) cacheResume = false;
        _animator.speed = 1;
    }

    public void Ended()
    {
        OnEnded?.Invoke();
    }
}
