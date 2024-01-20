using Scripts.Architecture.Services;
using Scripts.Spawner;
using System.Collections;
using UnityEngine;

namespace Scripts.Characters
{
    [RequireComponent(typeof(Animator))]
    public class Zombie : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private ParticleSystem _deathEffect;
        [SerializeField] private ParticleSystem _bloodEffect;

        private const string Hit = "Hit";
        private readonly int _waitingTime = 4;
        private SitizenSpawner _spawner;
        private IZombieHealthService _health;
        private Animator _animator;

        public int Damage
        { get { return _damage; } }

        public bool IsDead = false;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _spawner = AllServices.Container.Single<ISpawnerService>().CurrentSitizenSpawner;
            _health = AllServices.Container.Single<IZombieHealthService>();

            _health.DamageApplied += OnDamageApplied;
            _health.Died += OnDied;

            StartCoroutine(ApplyDamage());
        }

        private IEnumerator ApplyDamage()
        {
            yield return new WaitForSeconds(_waitingTime);

            while (!IsDead)
            {
                if (_spawner.Sitizens.Count != 0)
                {
                    foreach (var sitizen in _spawner.Sitizens.ToArray())
                    {
                        sitizen.TakeDamage(_damage);
                        _animator.SetTrigger(Hit);
                    }

                    yield return new WaitForSeconds(_waitingTime);
                }

                else
                {
                    _spawner.AllSitizensDie();
                    OnDied();
                    break;
                }
            }
        }

        private void OnDamageApplied()
        {
            if (_bloodEffect != null)
            {
                _bloodEffect.Play();
            }
        }

        private void OnDied()
        {
            IsDead = true;
            _deathEffect.transform.parent = null;
            _deathEffect.Play();

            if (_health != null)
            {
                _health.DamageApplied -= OnDamageApplied;
                _health.Died -= OnDied;
            }

            Destroy(gameObject);
        }
    }
}
