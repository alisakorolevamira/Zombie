using Scripts.Architecture.Services;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Characters.Sitizens
{
    [RequireComponent(typeof(Animator))]
    public class Sitizen : MonoBehaviour
    {
        private const string Hit = "Hit";
        private readonly int _coefficientOfChangingSpeed = 2;

        [SerializeField] private int _health;
        [SerializeField] private int _damage;

        public SpawnPoint SpawnPoint;
        public SitizenTypeId TypeId;

        private float _speed = 3;
        private bool _isDead = false;
        private Animator _animator;
        private IZombieHealthService _zombieHealthService;

        public event Action<int> HealthChanged;
        public event Action<Sitizen> Died;

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

        private IEnumerator ApplyDamage()
        {
            while (!_isDead && _zombieHealthService.Health >= 0)
            {
                _animator.SetTrigger(Hit);
                _zombieHealthService.ChangeHealth(_damage);

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
}
