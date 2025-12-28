using System;
using UnityEngine;

public class PauseAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public event Action OnEnded;

    private bool cacheResume;
    public void Pause()
    {
        if (cacheResume)
        {
            _animator.speed = 1;
            cacheResume = false;
            return;
        }
        _animator.speed = 0;
    }

    public void Resume()
    {
        if (_animator.speed >= 1)
        {
            cacheResume = true;
        }
        else
        {
            _animator.speed = 1;
        }
        _animator.speed = 1;
    }

    public void Ended()
    {
        OnEnded?.Invoke();
    }
}
