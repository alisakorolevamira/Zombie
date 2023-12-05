using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneConfig
{
    public abstract string SceneName { get; }
    public abstract Dictionary<Type, Repository> CreateAllRepositories();
    public abstract Dictionary<Type, Interactor> CreateAllInteractors();
    public abstract Dictionary<Type, Panel> GetAllPanels();

    public void CreateInteractor<T>(Dictionary<Type, Interactor> interactorsMap) where T : Interactor, new()
    {
        var interactor = new T();
        var type = typeof(T);

        interactorsMap[type] = interactor;
    }

    public void CreateRepository<T>(Dictionary<Type, Repository> repositoriesMap) where T : Repository, new()
    {
        var repository = new T();
        var type = typeof(T);

        repositoriesMap[type] = repository;
    }

    public void GetPanel<T>(Dictionary<Type, Panel> panelsMap) where T : Panel, new()
    {
        var type = typeof(T);
        var prefab = Resources.Load<T>($"Prefabs/UI/{type.Name}");
        var panel = new Panel() as T;
        panel.GetFromPrefab(prefab);

        panelsMap[type] = panel;
    }
}
