using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Zombie : MonoBehaviour
{
    [SerializeField] private SitizensSpawner _spawner;
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private ParticleSystem _bloodEffect;

    private List<Sitizen> _sitizens;

    private readonly int _waitingTime = 4;
    private readonly string _nameOfAnimation = "Hit";

    public event UnityAction AllSitizensDied;

    public int Damage { get { return _damage; } }

    [SerializeField] private int _damage;
    public bool IsDead = false;
    private Animator _animator;

    private void OnEnable()
    {
        var interactor = Game.GetInteractor<ZombiesHealthInteractor>();
        if (interactor != null)
        {
            interactor.DamageApplied += OnDamageApplied;
            interactor.Died += OnDied;
        }
    }

    private void OnDisable()
    {
        var interactor = Game.GetInteractor<ZombiesHealthInteractor>();
        if (interactor != null)
        {
            interactor.DamageApplied -= OnDamageApplied;
            interactor.Died -= OnDied;
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(ApplyDamage());
    }

    private IEnumerator ApplyDamage()
    {
        while (!IsDead)
        {
            _sitizens = _spawner.Sitizens;

            if (_sitizens.Count != 0)
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

    private void OnDamageApplied()
    {
        _bloodEffect.Play();
    }

    private void OnDied()
    {
        IsDead = true;
        _deathEffect.Play();
        Destroy(gameObject);
    }
}
