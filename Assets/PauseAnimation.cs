using UnityEngine;

public class PauseAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    public void Pause()
    {
        _animator.speed = 0;
    }

    public void Resume()
    {
        _animator.speed = 1;
    }
}
