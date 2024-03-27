using Scripts.Architecture.Services;
using Scripts.Architecture.States;

namespace Scripts.UI.Panels
{
    public class MenuPanel : Panel
    {       
        private GameStateMachine _gameStateMachine;
        private IPlayerDataService _playerDataService;
        private IUIPanelService _panelService;
        private ISaveLoadService _saveLoadService;

        private void Start()
        {
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
            _panelService = AllServices.Container.Single<IUIPanelService>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _gameStateMachine = _panelService.StateMachine;
        }

        public void OpenAnyLevel(string sceneName)
        {
            _saveLoadService.ResetProgress();

            _gameStateMachine.Enter<LoadProgressState, string>(sceneName);
        }

        public void OpenProgressLevel()
        {
            _gameStateMachine.Enter<LoadProgressState, string>(_playerDataService.PlayerProgress.Level);
        }
    }
}
