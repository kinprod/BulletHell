using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField]
    private int _health = 5;

    [NonSerialized]
    public static Action TakeDamageEvent;
    [NonSerialized]
    public static Action DieEvent;

    public void TakeDamage(int damage)
    {
        if (_health - damage > 0)
        {
            _health -= damage;
            TakeDamageEvent?.Invoke();
        }
        else
        {
            TakeDamageEvent?.Invoke();
            DieEvent?.Invoke();
        }
    }
}
