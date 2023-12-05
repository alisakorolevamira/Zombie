using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class SceneManagerBase
{
    public event Action<Scene> OnSceneLoadedEvent;
    public Scene CurrentScene { get; private set; }

    public bool IsLoanding { get; private set; }

    protected Dictionary<string, SceneConfig> _sceneConfigMap;

    public SceneManagerBase()
    {
        _sceneConfigMap = new Dictionary<string, SceneConfig>();

        InitScenesMap();
    }

    public abstract void InitScenesMap();

    public Coroutine LoadNewSceneAsync(string sceneName)
    {
        if (IsLoanding)
        {
            throw new Exception("Scene is loading now");
        }

        var config = _sceneConfigMap[sceneName];

        return Coroutines.StartRoutine(LoadNewSceneRoutine(config));
    }

    public Coroutine LoadCurrentSceneAsync()
    {
        if (IsLoanding)
        {
            throw new Exception("Scene is loading now");
        }

        var sceneName = SceneManager.GetActiveScene().name;
        var config = _sceneConfigMap[sceneName];

        return Coroutines.StartRoutine(LoadCurrentSceneRoutine(config));
    }

    private IEnumerator LoadNewSceneRoutine(SceneConfig sceneConfig)
    {
        IsLoanding = true;

        yield return Coroutines.StartRoutine(LoadSceneRoutine(sceneConfig));
        yield return Coroutines.StartRoutine(InitializeSceneRoutine(sceneConfig));

        IsLoanding = false;
        OnSceneLoadedEvent?.Invoke(CurrentScene);
    }

    private IEnumerator LoadCurrentSceneRoutine(SceneConfig sceneConfig)
    {
        IsLoanding = true;

        yield return Coroutines.StartRoutine(InitializeSceneRoutine(sceneConfig));

        IsLoanding = false;
        OnSceneLoadedEvent?.Invoke(CurrentScene);
    }

    private IEnumerator LoadSceneRoutine(SceneConfig sceneConfig)
    {
        var async = SceneManager.LoadSceneAsync(sceneConfig.SceneName);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            yield return null;
        }

        async.allowSceneActivation = true;
    }

    private IEnumerator InitializeSceneRoutine(SceneConfig sceneConfig)
    {
        CurrentScene = new Scene(sceneConfig);

        yield return CurrentScene.InitializeAsync();
    }

    public T GetRepository<T>() where T : Repository
    {
        return CurrentScene.GetRepository<T>();
    }

    public T GetInteractor<T>() where T : Interactor
    {
        return CurrentScene.GetInteractor<T>();
    }

    public T GetPanel<T>() where T : Panel
    {
        return CurrentScene.GetPanel<T>();
    }
}
