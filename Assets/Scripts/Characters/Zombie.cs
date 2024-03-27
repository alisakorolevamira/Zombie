using Scripts.Architecture.Services;
using System.Collections;
using UnityEngine;

namespace Scripts.Characters
{
    [RequireComponent(typeof(Animator))]
    public class Zombie : MonoBehaviour
    {
        private readonly int _waitingTime = 4;

        [SerializeField] private int _damage;
        [SerializeField] private ParticleSystem _deathEffect;
        [SerializeField] private ParticleSystem _bloodEffect;

        public bool IsDead = false;

        private IZombieHealthService _healthService;
        private ICombatService _combatService;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _healthService = AllServices.Container.Single<IZombieHealthService>();
            _combatService = AllServices.Container.Single<ICombatService>();

            _healthService.DamageApplied += OnDamageApplied;
            _healthService.Died += OnDied;

            StartCoroutine(ApplyDamage());
        }

        private void OnDestroy()
        {
            if (_healthService != null)
            {
                _healthService.DamageApplied -= OnDamageApplied;
                _healthService.Died -= OnDied;
            }
        }

        private IEnumerator ApplyDamage()
        {
            yield return new WaitForSeconds(_waitingTime);

            while (!IsDead)
            {
                if (_combatService.CitizenCount != Constants.MinimumNumberOfCitizens)
                {
                    _animator.SetTrigger(Constants.Hit);
                    _combatService.ApplyDamage(_damage);

                    yield return new WaitForSeconds(_waitingTime);
                }

                else
                {
                    _combatService.AllCitizensDie();
                    OnDied();
                    break;
                }
            }
        }

        private void OnDamageApplied()
        {
            if (_bloodEffect != null)
                _bloodEffect.Play();
        }

        private void OnDied()
        {
            IsDead = true;

            _deathEffect.Play();

            if (_healthService != null)
            {
                _healthService.DamageApplied -= OnDamageApplied;
                _healthService.Died -= OnDied;
            }
            
            Destroy(gameObject);
        }
    }
}
