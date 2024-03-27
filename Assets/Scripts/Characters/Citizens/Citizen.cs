using Scripts.Architecture.Services;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Characters.Citizens
{
    [RequireComponent(typeof(Animator))]
    public class Citizen : MonoBehaviour
    {
        private readonly int _coefficientOfChangingSpeed = 2;
        private readonly int _minimumHealth = 0;

        [SerializeField] private int _damage;
        [SerializeField] private AudioSource _audioSource;

        public SpawnPoint SpawnPoint;
        public CitizenTypeId TypeId;

        private float _speed = 3;
        private bool _isDead = false;
        private Animator _animator;
        private IZombieHealthService _zombieHealthService;

        public event Action<int> HealthChanged;
        public event Action<Citizen> Died;
        [field: SerializeField] public float Health { get; private set; }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();

            StartCoroutine(ApplyDamage());
        }

        public void AddSpeed()
        {
            _speed /= _coefficientOfChangingSpeed;
            _animator.speed *= _coefficientOfChangingSpeed;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            CheckDeath();

            HealthChanged?.Invoke(-damage);
        }

        private IEnumerator ApplyDamage()
        {
            while (!_isDead && _zombieHealthService.Health > Constants.ZombieMinimumHealth)
            {
                _audioSource.PlayOneShot(_audioSource.clip);
                _animator.SetTrigger(Constants.Hit);
                _zombieHealthService.ChangeHealth(_damage);

                yield return new WaitForSeconds(_speed);
            }
        }

        private void CheckDeath()
        {
            if (Health <= _minimumHealth)
            {
                Died?.Invoke(this);

                StartCoroutine(Die());
            }
        }

        private IEnumerator Die()
        {
            _animator.Play(Constants.Die);

            yield return new WaitForSeconds(1.9f);

            _isDead = true;
            Destroy(gameObject);
        }
    }
}
