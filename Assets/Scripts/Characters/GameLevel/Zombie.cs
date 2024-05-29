using System.Collections;
using Architecture.Services;
using Architecture.ServicesInterfaces.Zombie;
using Constants.Characters;
using UnityEngine;

namespace Characters.GameLevel
{
    [RequireComponent(typeof(Animator))]
    public class Zombie : MonoBehaviour
    {
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
            while (IsDead == false)
            {
                if (_combatService.CitizenCount != CitizenConstants.MinimumNumberOfCitizens)
                {
                    _animator.SetTrigger(ZombieConstants.Hit);
                    WaitForSeconds animationTime = new WaitForSeconds(ZombieConstants.AnimationTime);

                    yield return animationTime;

                    _combatService.ApplyDamage(_damage);
                    WaitForSeconds damageTime = new WaitForSeconds(ZombieConstants.DamageWaitingTime);

                    yield return damageTime;
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