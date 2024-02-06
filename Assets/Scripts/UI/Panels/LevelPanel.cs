using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using Scripts.Spawner;

namespace Scripts.UI.Panels
{
    public class LevelPanel : Panel
    {
        private GameStateMachine _gameStateMachine;
        private ISaveLoadService _saveLoadService;
        private ISpawnerService _spawnerService;

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _spawnerService = AllServices.Container.Single<ISpawnerService>();
        }

        public void OpenNextScene(int sceneIndex)
        {
            _spawnerService.CurrentSitizenSpawner.ClearSubscriptions();
            _gameStateMachine = GetComponentInParent<PanelSpawner>().StateMachine;
            _gameStateMachine.Enter<LoadProgressState, int>(sceneIndex);
        }

        public void OpenSceneWithResetingProgress(int sceneIndex)
        {
            _saveLoadService.ResetProgress();
            _saveLoadService.SaveProgress();
            OpenNextScene(sceneIndex);
        }
    }
}
