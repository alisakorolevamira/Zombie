using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Sitizen : MonoBehaviour
{
    [SerializeField] public SpawnPoint SpawnPoint;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    private float _speed = 3;
    private readonly int _coefficientOfChangingSpeed = 2;
    private readonly string _nameOfAnimation = "Hit";
    private bool _isDead = false;
    private Animator _animator;

    public event UnityAction<int> HealthChanged;
    public event UnityAction<Sitizen> Died;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(ApplyDamage());
    }

    public void AddSpeed()
    {
        _speed /= _coefficientOfChangingSpeed;
        _animator.speed *= _coefficientOfChangingSpeed;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        CheckDeath();

        HealthChanged?.Invoke(-damage);
    }

    public Sitizen InstantiateNewSitizen(Sitizen sitizenprefab, SpawnPoint spawnPoint)
    {
        var newSitizen = Instantiate(sitizenprefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        newSitizen.SpawnPoint = spawnPoint;
        newSitizen.SpawnPoint.ChangeAvailability(false);

        return newSitizen;
    }

    private IEnumerator ApplyDamage()
    {
        var interactor = Game.GetInteractor<ZombiesHealthInteractor>();

        while (!_isDead && interactor.Health() >= 0)
        {
            _animator.SetTrigger(_nameOfAnimation);
            interactor.ChangeHealth(_damage);

            yield return new WaitForSeconds(_speed);
        }
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            Died?.Invoke(this);

            _isDead = true;
            Die();
        }
    }
}
