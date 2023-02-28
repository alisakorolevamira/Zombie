using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Creature : MonoBehaviour
{
    [SerializeField] private protected int _health;
    [SerializeField] private protected int _damage;

    private protected bool _isDead = false;
    private protected Animator _animator;

    public bool IsDead { get { return _isDead; } }

    private protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage)
    {
        _health -= damage;

        CheckDeath();
    }

    private protected virtual void CheckDeath()
    {
        if (_health <= 0)
        {
            _isDead = true;
            Destroy(gameObject);
        }
    }
}