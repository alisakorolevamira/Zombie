using Scripts.Architecture.Factory;
using Scripts.Architecture.Services;
using Scripts.Characters.Sitizens;
using Scripts.UI.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.Spawner
{
    public class SitizenSpawner : MonoBehaviour
    {
        public readonly List<Sitizen> Sitizens = new();

        [SerializeField] private SpawnPoint[] _spawnPoints;
        [SerializeField] private AddSitizenCard _addSitizenCard;
        [SerializeField] private MergeCard _mergeCard;

        private IGameFactory _factory;
        private IPlayerScoreService _playerScoreService;

        public event Action NumberOfSitizensChanged;
        public event Action AllSitizensDied;

        private void OnEnable()
        {
            _playerScoreService = AllServices.Container.Single<IPlayerScoreService>();
            DontDestroyOnLoad(gameObject); //изменить потом
        }

        private void OnDisable()
        {
            ClearSubscriptions();
        }

        public void ClearSubscriptions()
        {
            if (Sitizens.Count != 0)
            {
                foreach (var sitizen in Sitizens)
                {
                    sitizen.Died -= RemoveDeadSitizen;
                }
            }

            if (_addSitizenCard != null && _mergeCard != null)
            {
                _addSitizenCard.OnClicked -= AddFirstLevelSitizen;
                _mergeCard.OnClicked -= MergeSitizens;
            }
        }

        public void AddComponentsOnLevel()
        {
            _factory = AllServices.Container.Single<IGameFactory>();

            _addSitizenCard.OnClicked += AddFirstLevelSitizen;
            _mergeCard.OnClicked += MergeSitizens;

            Sitizens.Clear();

            GetSpawnPoints();
            AddFirstLevelSitizen();
        }

        public bool CheckAmountOfSitizens()
        {
            return Sitizens.Count < Constants.MaximumNumberOfSitizens;
        }

        public void AllSitizensDie()
        {
            AllSitizensDied?.Invoke();
            _playerScoreService.RemoveScore();
        }

        private void AddFirstLevelSitizen()
        {
            SpawnPoint freeSpawnPoint = _spawnPoints.FirstOrDefault(p => p.IsAvailable == true);

            if (CheckAmountOfSitizens() && freeSpawnPoint != null)
                InstantiateSitizen(freeSpawnPoint, Constants.FirstLevelSitizenPath);
        }

        private void RemoveDeadSitizen(Sitizen deadSitizen)
        {
            deadSitizen.SpawnPoint.ChangeAvailability(true);
            Sitizens.Remove(deadSitizen);
            deadSitizen.Died -= RemoveDeadSitizen;

            NumberOfSitizensChanged?.Invoke();
        }

        private void MergeSitizens()
        {
            Sitizen firstSitizen = Sitizens.FirstOrDefault(p => p.TypeId == SitizenTypeId.FirstSitizen);
            RemoveDeadSitizen(firstSitizen);

            Sitizen secondSitizen = Sitizens.FirstOrDefault(p => p.TypeId == SitizenTypeId.FirstSitizen);
            RemoveDeadSitizen(secondSitizen);

            InstantiateSitizen(firstSitizen.SpawnPoint, Constants.SecondLevelSitizenPath);

            firstSitizen.Die();
            secondSitizen.Die();
        }

        private void InstantiateSitizen(SpawnPoint spawnPoint, string path)
        {
            Sitizen newSitizen = _factory.SpawnSitizen(spawnPoint, path).GetComponent<Sitizen>();

            newSitizen.SpawnPoint = spawnPoint;
            newSitizen.SpawnPoint.ChangeAvailability(false);

            Sitizens.Add(newSitizen);
            newSitizen.Died += RemoveDeadSitizen;

            NumberOfSitizensChanged?.Invoke();
        }

        private void GetSpawnPoints()
        {
            foreach (SpawnPoint spawnPoint in _spawnPoints)
                spawnPoint.ChangeAvailability(true);
        }
    }
}
