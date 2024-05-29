using System;
using System.Collections;
using Architecture.Services;
using Architecture.ServicesInterfaces.Zombie;
using Audio;
using Constants.Characters;
using JetBrains.Annotations;
using UnityEngine;

namespace Characters.GameLevel.Citizens
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
        private Coroutine _coroutine;

        public event Action<int> HealthChanged;
        public event Action<Citizen> Died;

        [field: SerializeField] public float Health { get; private set; }

        private void OnDestroy()
        {
            StopCoroutine(_coroutine);
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _zombieHealthService = AllServices.Container.Single<IZombieHealthService>();

            _coroutine = StartCoroutine(ApplyDamage());
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
            while (_isDead == false && _zombieHealthService.Health > ZombieConstants.ZombieMinimumHealth)
            {
                _animator.SetTrigger(CitizenConstants.Attack);
                WaitForSeconds speed = new WaitForSeconds(_speed);

                yield return speed;
            }
        }

        [UsedImplicitly]
        private void OnAttack()
        {
            _shortEffectAudio.PlayOneShot();
            _zombieHealthService.TakeDamage(_damage);
        }

        private void CheckDeath()
        {
            if (Health > CitizenConstants.MinimumHealth)
                return;

            Died?.Invoke(this);
            StartCoroutine(Die());
        }

        private IEnumerator Die()
        {
            _animator.Play(CitizenConstants.Die);
            WaitForSeconds dieAnimationTime = new WaitForSeconds(CitizenConstants.DieAnimationTime);

            yield return dieAnimationTime;

            _isDead = true;
            Destroy(gameObject);
        }
    }
}