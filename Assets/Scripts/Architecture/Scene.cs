using System.Collections;
using UnityEngine;

public class Scene
{
    public SceneConfig SceneConfig;
    private InteractorsBase _interactorsBase;
    private RepositoriesBase _repositoriesBase;
    private PanelsManager _panelsManager;

    public Scene(SceneConfig sceneConfig)
    {
        SceneConfig = sceneConfig;
        _interactorsBase = new InteractorsBase(sceneConfig);
        _repositoriesBase = new RepositoriesBase(sceneConfig);
        _panelsManager = new PanelsManager(sceneConfig);
    }

    public Coroutine InitializeAsync()
    {
        return Coroutines.StartRoutine(InitializeRoutine());
    }

    private IEnumerator InitializeRoutine() //заменить корутины async await
    {
        _repositoriesBase.CreateAllRepositories();
        _interactorsBase.CreateAllInteractors();
        _panelsManager.GetAllPanels();

        yield return null;

        _repositoriesBase.SendOnCreateToAllRepositories();
        _interactorsBase.SendOnCreateToAllInteractors();

        yield return null;

        _repositoriesBase.InitializeAllRepositories();
        _interactorsBase.InitializeAllInteractors();
        _panelsManager.InitializeAllPanels();

        yield return null;

        _repositoriesBase.SendOnStartToAllRepositories();
        _interactorsBase.SendOnStartToAllInteractors();
        _panelsManager.SendOnStartToAllPanels();
    }

    public T GetRepository<T>() where T : Repository
    {
        return _repositoriesBase.GetRepository<T>();
    }

    public T GetInteractor<T>() where T : Interactor
    {
        return _interactorsBase.GetInteractor<T>();
    }

    public T GetPanel<T>() where T : Panel
    {
        return _panelsManager.GetPanel<T>();
    }
}
