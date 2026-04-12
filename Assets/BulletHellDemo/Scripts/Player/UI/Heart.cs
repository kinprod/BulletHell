using System;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    public Action DestroyHeartAction;

    public void EnableHeart()
    {
        Health.TakeDamageEvent += DestroyHeartAnimation;
    }

    private void OnDisable()
    {
        Health.TakeDamageEvent -= DestroyHeartAnimation;
    }

    public void DestroyAfterAnimation()
    {
        DestroyHeartAction?.Invoke();
    }

    private void DestroyHeartAnimation()
    {
        _animator.SetBool("Destroy", true);
    }
}
