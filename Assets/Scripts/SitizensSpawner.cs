using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class SitizensSpawner : MonoBehaviour
{
    [SerializeField] private Sitizen _firstLevelSitizen;
    [SerializeField] private Sitizen _secondLevelSitizen;
    [SerializeField] private AddSitizenCard _addSitizenCard;
    [SerializeField] private MergdeCard _mergeCard;
    [SerializeField] private Zombie _zombie;

    private List<Sitizen> _sitizens = new List<Sitizen>();
    private SpawnPoint [] _spanwPoints;
    private int _maximumNumberOfSitizens = 5;

    public event UnityAction NumberOfSitizensChanged;

    public int NumberOfSitizens { get { return _sitizens.Count; } }

    private void OnEnable()
    {
        _addSitizenCard.OnClicked += AddFirstLevelSitizen;
        _mergeCard.OnClicked += MergeSitizens;

        _spanwPoints = GetComponentsInChildren<SpawnPoint>();
        AddFirstLevelSitizen();
    }

    private void OnDisable()
    {
        foreach (var sitizen in _sitizens)
        {
            sitizen.Died -= RemoveDeadSitizen;
        }

        _addSitizenCard.OnClicked -= AddFirstLevelSitizen;
        _mergeCard.OnClicked -= MergeSitizens;
    }

    private void Start()
    {
       // _spanwPoints = GetComponentsInChildren<SpawnPoint>();
       // AddFirstLevelSitizen();
    }

    public bool CheckAmountOfSitizens()
    {
        return _sitizens.Count < _maximumNumberOfSitizens;
    }

    public Sitizen [] GetSitizens()
    {
        var sitizens = _sitizens.ToArray();
        return sitizens;
    }

    private void AddFirstLevelSitizen()
    {
        var freeSpawnPoint = _spanwPoints.FirstOrDefault(p => p.IsAvailable == true);

        if (CheckAmountOfSitizens() && freeSpawnPoint != null)
        {
            InstantiateNewSitizen(_firstLevelSitizen, freeSpawnPoint);
        }
    }

    private void RemoveDeadSitizen(Sitizen deadSitizen)
    {
        deadSitizen.SpawnPoint.ChangeAvailability(true);
        _sitizens.Remove(deadSitizen);

        NumberOfSitizensChanged?.Invoke();
    }

    private void MergeSitizens()
    {
        var firstSitizen = _sitizens[0];
        var secondSitizen = _sitizens[1];

        RemoveDeadSitizen(secondSitizen);
        RemoveDeadSitizen(firstSitizen);

        InstantiateNewSitizen(_secondLevelSitizen, firstSitizen.SpawnPoint);

        Destroy(firstSitizen.gameObject);
        Destroy(secondSitizen.gameObject);
    }

    private void InstantiateNewSitizen(Sitizen sitizen, SpawnPoint spawnPoint)
    {
        var newSitizen = Instantiate(sitizen, spawnPoint.transform.position, spawnPoint.transform.rotation);
        newSitizen.SpawnPoint = spawnPoint;
        newSitizen.GetZombie(_zombie);

        _sitizens.Add(newSitizen);

        newSitizen.SpawnPoint.ChangeAvailability(false);
        newSitizen.Died += RemoveDeadSitizen;

        NumberOfSitizensChanged?.Invoke();
    }
}