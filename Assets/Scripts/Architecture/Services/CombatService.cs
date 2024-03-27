using Scripts.Characters.Citizens;
using System;
using System.Collections.Generic;

namespace Scripts.Architecture.Services
{
    public class CombatService : ICombatService
    {
        private readonly List<Citizen> _citizens = new();
        private readonly ISpawnerService _spawnerService;

        public CombatService(ISpawnerService spawner)
        {
            _spawnerService = spawner;
        }

        public event Action AllCitizensDied;

        public int CitizenCount => _citizens.Count;

        public void Initialize()
        {
            _spawnerService.CitizenSpawner.CitizenAdded += AddCitizen;
            _spawnerService.CitizenSpawner.CitizenRemoved += RemoveCitizen;
        }

        public void Dispose()
        {
            _spawnerService.CitizenSpawner.CitizenAdded -= AddCitizen;
            _spawnerService.CitizenSpawner.CitizenRemoved -= RemoveCitizen;
        }

        public void AllCitizensDie() => AllCitizensDied?.Invoke();

        public void ApplyDamage(int damage)
        {
            foreach (var citizen in _citizens.ToArray())
            {
                if (citizen != null)
                    citizen.TakeDamage(damage);
            }
        }

        private void AddCitizen(Citizen citizen) => _citizens.Add(citizen);

        private void RemoveCitizen(Citizen citizen) => _citizens.Remove(citizen);
    }
}
