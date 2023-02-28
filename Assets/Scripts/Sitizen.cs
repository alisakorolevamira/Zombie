using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Sitizen : Creature
{
    public SpawnPoint SpawnPoint;

    [SerializeField] private float _speed = 6f;
    private int _coefficientOfChangingSpeed = 2;
    private string _nameOfAnimation = "Hit";
    private Zombie _zombie;

    public event UnityAction<int> HealthChanged;
    public event UnityAction<Sitizen> Died;

    private protected override void Start()
    {
        base.Start();

        StartCoroutine(ApplyDamage(_zombie));
    }

    public void GetZombie(Zombie zombie)
    {
        _zombie = zombie;
    }

    public void AddSpeed()
    {
        _speed /= _coefficientOfChangingSpeed;
        _animator.speed *= _coefficientOfChangingSpeed;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        HealthChanged?.Invoke(-damage);
    }

    private IEnumerator ApplyDamage(Zombie creature)
    {
        while (!_isDead && creature != null)
        {
            _animator.SetTrigger(_nameOfAnimation);
            creature.TakeDamage(_damage);

            yield return new WaitForSeconds(_speed);
        }
    }

    private protected override void CheckDeath()
    {
        if (_health <= 0)
        {
            Died?.Invoke(this);

            base.CheckDeath();
        }
    }
}
