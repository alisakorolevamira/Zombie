using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using Scripts.Spawner;

namespace Scripts.UI.Panels
{
    public class MenuPanel : Panel
    {
        private GameStateMachine _gameStateMachine;
        private ISaveLoadService _saveLoadService;

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public void OpenAnyLevel(int sceneIndex)
        {
            _saveLoadService.ResetProgress();
            _saveLoadService.SaveProgress();

            _gameStateMachine = GetComponentInParent<PanelSpawner>().StateMachine;
            _gameStateMachine.Enter<LoadProgressState, int>(sceneIndex);
        }

        public void OpenProgressLevel()
        {
            _gameStateMachine = GetComponentInParent<PanelSpawner>().StateMachine;
            _gameStateMachine.Enter<LoadProgressState, int>(_saveLoadService.PlayerProgress.Level);
        }
    }
}
