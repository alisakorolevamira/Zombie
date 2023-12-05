using Scripts.Architecture.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Characters.Sitizens
{
    [RequireComponent(typeof(Animator))]
    public class Sitizen : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _damage;

        public SpawnPoint SpawnPoint;
        public SitizenTypeId TypeId;

        private const string Hit = "Hit";
        private float _speed = 3;
        private readonly int _coefficientOfChangingSpeed = 2;
        private bool _isDead = false;
        private Animator _animator;
        private IZombieHealthService _zombieHealthService;

        public event UnityAction<int> HealthChanged;
        public event UnityAction<Sitizen> Died;

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
            while (!_isDead && _zombieHealthService.Health() >= 0)
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
