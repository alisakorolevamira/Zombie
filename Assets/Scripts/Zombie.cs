using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zombie : Creature
{
    [SerializeField] private int _reward;
    [SerializeField] private SitizensSpawner _spawner;
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private ParticleSystem _bloodEffect;

    private int _maximumHealth;
    private Sitizen [] _sitizens;

    private int _waitingTime = 4;
    private int _coefficientOfChangingReward = 2;
    private string _nameOfAnimation = "Hit";

    public event UnityAction Died;
    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> RewardGived;
    public event UnityAction AllSitizensDied;

    public int Damage { get { return _damage; } }
    public int Reward { get { return _reward; } }

    private protected override void Start()
    {
        base.Start();

        _maximumHealth = _health;
        StartCoroutine(ApplyDamage());
    }
    
    public void DoubleReward()
    {
        _reward *= _coefficientOfChangingReward;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        _bloodEffect.Play();
        HealthChanged?.Invoke(_health, _maximumHealth);
        RewardGived?.Invoke(_reward);
    }

    private protected override void CheckDeath()
    {
        if (_health <= 0)
        {
            Died?.Invoke();
            _deathEffect.Play();

            base.CheckDeath();
        }
    }

    private IEnumerator ApplyDamage()
    {
        while (!_isDead)
        {
            FindSitizens();

            if (_sitizens.Length != 0)
            {
                foreach (var sitizen in _sitizens)
                {
                    sitizen.TakeDamage(_damage);
                    _animator.SetTrigger(_nameOfAnimation);
                    
                    yield return new WaitForSeconds(_waitingTime);
                }
            }

            else
            {
                AllSitizensDied?.Invoke();
                break;
            }
        }
    }

    private void FindSitizens()
    {
        _sitizens = _spawner.GetSitizens();
    }
}
