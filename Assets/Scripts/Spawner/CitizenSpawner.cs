using System;
using System.Collections.Generic;
using System.Linq;
using Architecture.Factory;
using Architecture.Services;
using Architecture.ServicesInterfaces.UI;
using Characters.GameLevel.Citizens;
using Constants.Characters;
using UI.Buttons.GameLevel.Cards;
using UnityEngine;

namespace Spawner
{
    public class CitizenSpawner : MonoBehaviour
    {
        public readonly List<Citizen> Citizens = new();

        [SerializeField] private SpawnPoint[] _spawnPoints;

        private AddCitizenCard _addCitizenCard;
        private MergeCard _mergeCard;
        private IGameFactory _factory;
        private IUIPanelService _panelService;

        public event Action NumberOfCitizensChanged;
        public event Action<Citizen> CitizenAdded;
        public event Action<Citizen> CitizenRemoved;

        private void OnEnable()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void OnDisable()
        {
            ClearSubscriptions();
        }

        private void Start()
        {
            _panelService = AllServices.Container.Single<IUIPanelService>();
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void AddComponentsOnLevel()
        {
            _addCitizenCard = _panelService.GetCard<AddCitizenCard>();
            _mergeCard = _panelService.GetCard<MergeCard>();

            ClearSubscriptions();

            _addCitizenCard.OnClicked += AddFirstLevelCitizen;
            _mergeCard.OnClicked += MergeCitizens;

            Citizens.Clear();
            GetSpawnPoints();
            AddFirstLevelCitizen();
        }

        public bool CheckAmountOfCitizens()
        {
            return Citizens.Count < CitizenConstants.MaximumNumberOfCitizens;
        }

        private void ClearSubscriptions()
        {
            if (Citizens.Count != CitizenConstants.MinimumNumberOfCitizens)
            {
                foreach (var citizen in Citizens)
                    citizen.Died -= RemoveDeadCitizen;
            }

            if (_addCitizenCard != null && _mergeCard != null)
            {
                _addCitizenCard.OnClicked -= AddFirstLevelCitizen;
                _mergeCard.OnClicked -= MergeCitizens;
            }
        }

        private void AddFirstLevelCitizen()
        {
            SpawnPoint freeSpawnPoint = _spawnPoints.FirstOrDefault(p => p.IsAvailable == true);

            if (CheckAmountOfCitizens() && freeSpawnPoint != null)
                InstantiateCitizen(freeSpawnPoint, CitizenConstants.FirstLevelCitizenPath);
        }

        private void RemoveDeadCitizen(Citizen deadCitizen)
        {
            deadCitizen.SpawnPoint.IsAvailable = true;
            Citizens.Remove(deadCitizen);

            deadCitizen.Died -= RemoveDeadCitizen;

            NumberOfCitizensChanged?.Invoke();
            CitizenRemoved?.Invoke(deadCitizen);
        }

        private void MergeCitizens()
        {
            Citizen firstCitizen = Citizens.FirstOrDefault(p => p.TypeId == CitizenTypeId.FirstCitizen);
            RemoveDeadCitizen(firstCitizen);

            Citizen secondCitizen = Citizens.FirstOrDefault(p => p.TypeId == CitizenTypeId.FirstCitizen);
            RemoveDeadCitizen(secondCitizen);

            InstantiateCitizen(firstCitizen.SpawnPoint, CitizenConstants.SecondLevelCitizenPath);

            Destroy(firstCitizen.gameObject);
            Destroy(secondCitizen.gameObject);
        }

        private void InstantiateCitizen(SpawnPoint spawnPoint, string path)
        {
            Citizen newCitizen = _factory.SpawnCitizen(spawnPoint, path).GetComponent<Citizen>();

            newCitizen.SpawnPoint = spawnPoint;
            newCitizen.SpawnPoint.IsAvailable = false;

            Citizens.Add(newCitizen);

            newCitizen.Died += RemoveDeadCitizen;

            NumberOfCitizensChanged?.Invoke();
            CitizenAdded?.Invoke(newCitizen);
        }

        private void GetSpawnPoints()
        {
            foreach (SpawnPoint spawnPoint in _spawnPoints)
                spawnPoint.IsAvailable = true;
        }
    }
}