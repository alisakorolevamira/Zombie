using System;
using System.Collections.Generic;
using UnityEngine;

public class PanelsManager
{
    private Dictionary<Type, Panel> _panelsMap;
    private readonly SceneConfig _sceneConfig;

    public PanelsManager(SceneConfig sceneConfig)
    {
        _sceneConfig = sceneConfig;
    }

    public void InitializeAllPanels()
    {
        var allPanels = _panelsMap.Values;

        foreach (var panel in allPanels)
        {
            panel.Initialize();
        }
    }

    public void SendOnStartToAllPanels()
    {
        var allPanels = _panelsMap.Values;

        foreach (var panel in allPanels)
        {
            panel.OnStart();
        }
    }

    public void GetAllPanels()
    {
        _panelsMap = _sceneConfig.GetAllPanels();
    }

    public T GetPanel<T>() where T : Panel
    {
        var type = typeof(T);
        return (T)_panelsMap[type];
    }
}