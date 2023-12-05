using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class SitizensSpawner : MonoBehaviour
{
    [SerializeField] private AddSitizenCard _addSitizenCard;
    [SerializeField] private MergdeCard _mergeCard;

    public readonly List<Sitizen> Sitizens = new();
    private SpawnPoint [] _spawnPoints;
    private readonly int _maximumNumberOfSitizens = 5;

    public event UnityAction NumberOfSitizensChanged;

    private void OnEnable()
    {
        _addSitizenCard.OnClicked += AddFirstLevelSitizen;
        _mergeCard.OnClicked += MergeSitizens;
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        
        AddFirstLevelSitizen();
    }

    private void OnDisable()
    {
        foreach (var sitizen in Sitizens)
        {
            sitizen.Died -= RemoveDeadSitizen;
        }

        _addSitizenCard.OnClicked -= AddFirstLevelSitizen;
        _mergeCard.OnClicked -= MergeSitizens;
    }

    private void AddFirstLevelSitizen()
    {
        var freeSpawnPoint = _spawnPoints.FirstOrDefault(p => p.IsAvailable == true);

        if (CheckAmountOfSitizens() && freeSpawnPoint != null)
        {
            InstantiateFirstLevelSitizen(freeSpawnPoint);
        }
    }

    public bool CheckAmountOfSitizens()
    {
        return Sitizens.Count < _maximumNumberOfSitizens;
    }

    private void RemoveDeadSitizen(Sitizen deadSitizen)
    {
        deadSitizen.SpawnPoint.ChangeAvailability(true);
        Sitizens.Remove(deadSitizen);

        NumberOfSitizensChanged?.Invoke();
    }

    private void MergeSitizens()
    {
        var firstSitizen = Sitizens.First<Sitizen>();
        RemoveDeadSitizen(firstSitizen);

        var secondSitizen = Sitizens.First<Sitizen>();
        RemoveDeadSitizen(secondSitizen);

        InstantiateSecondLevelSitizen(firstSitizen.SpawnPoint);

        firstSitizen.Die();
        secondSitizen.Die();
    }

    private void InstantiateFirstLevelSitizen(SpawnPoint spawnPoint)
    {
        var sitizenPrefab = Resources.Load<Sitizen>($"Prefabs/FirstLevelSitizen");
        var newSitizen = new Sitizen();

        newSitizen.InstantiateNewSitizen(sitizenPrefab, spawnPoint);
        Sitizens.Add(newSitizen);
        newSitizen.Died += RemoveDeadSitizen;

        NumberOfSitizensChanged?.Invoke();
    }

    private void InstantiateSecondLevelSitizen(SpawnPoint spawnPoint)
    {
        var sitizenPrefab = Resources.Load<Sitizen>($"Prefabs/SecondLevelSitizen");
        var newSitizen = new Sitizen();

        newSitizen.InstantiateNewSitizen(sitizenPrefab, spawnPoint);
        Sitizens.Add(newSitizen);
        newSitizen.Died += RemoveDeadSitizen;

        NumberOfSitizensChanged?.Invoke();
    }

}