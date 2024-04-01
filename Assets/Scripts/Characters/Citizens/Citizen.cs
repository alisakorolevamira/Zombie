using Scripts.Architecture.Services;
using Scripts.Audio;
using Scripts.Constants;
using System;
using System.Collections;
using UnityEngine;

namespace Scripts.Characters.Citizens
{
    [RequireComponent(typeof(Animator))]
    public class Citizen : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private ShortEffectAudio _shortEffectAudio;

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
            _speed /= CitizenConstants.CoefficientOfChangingSpeed;
            _animator.speed *= CitizenConstants.CoefficientOfChangingSpeed;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            CheckDeath();

            HealthChanged?.Invoke(-damage);
        }

        private IEnumerator ApplyDamage()
        {
            while (!_isDead && _zombieHealthService.Health > ZombieConstants.ZombieMinimumHealth)
            {
                _shortEffectAudio.PlayOneShot();
                _animator.SetTrigger(CitizenConstants.Hit);
                _zombieHealthService.ChangeHealth(_damage);

                yield return new WaitForSeconds(_speed);
            }
        }

        private void CheckDeath()
        {
            if (Health <= CitizenConstants.MinimumHealth)
            {
                Died?.Invoke(this);

                StartCoroutine(Die());
            }
        }

        private IEnumerator Die()
        {
            _animator.Play(CitizenConstants.Die);

            yield return new WaitForSeconds(CitizenConstants.DieAnimationTime);

            _isDead = true;
            Destroy(gameObject);
        }
    }
}