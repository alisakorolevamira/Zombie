using System;
using System.Collections.Generic;
using UnityEngine;

public class FirstLevelSceneConfig : SceneConfig
{
    public const string Name = "FirstLevel";

    public override string SceneName => Name;

    public override Dictionary<Type, Interactor> CreateAllInteractors()
    {
        var interactorsMap = new Dictionary<Type, Interactor>();

        CreateInteractor<PlayersMoneyInteractor>(interactorsMap);
        CreateInteractor<ZombiesHealthInteractor>(interactorsMap);
        CreateInteractor<ZombiesRewardInteractor>(interactorsMap);

        return interactorsMap;
    }

    public override Dictionary<Type, Repository> CreateAllRepositories()
    {
        var repositoriesMap = new Dictionary<Type, Repository>();

        CreateRepository<PlayersMoneyRepository>(repositoriesMap);
        CreateRepository<ZombiesHealthRepository>(repositoriesMap);

        return repositoriesMap;
    }

    public override Dictionary<Type, Panel> GetAllPanels()
    {
        var panelMap = new Dictionary<Type, Panel>();

        GetPanel<WinPanel>(panelMap);
        GetPanel<LosePanel>(panelMap);
        GetPanel<SettingsPanel>(panelMap);
        GetPanel<MoneyBalancePanel>(panelMap);
        GetPanel<ZombieHealthBarPanel>(panelMap);

        return panelMap;
    }
}
