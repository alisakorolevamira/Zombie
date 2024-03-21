using Scripts.Architecture.Services;
using Scripts.Architecture.States;

namespace Scripts.UI.Panels
{
    public class MenuPanel : Panel
    {       
        private GameStateMachine _gameStateMachine;
        private ISaveLoadService _saveLoadService;
        private IUIPanelService _panelService;

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _panelService = AllServices.Container.Single<IUIPanelService>();
            _gameStateMachine = _panelService.StateMachine;
        }

        public void OpenAnyLevel(string sceneName)
        {
            _saveLoadService.ResetProgress();
            _saveLoadService.SaveProgress();

            _gameStateMachine.Enter<LoadProgressState, string>(sceneName);
        }

        public void OpenProgressLevel()
        {
            _gameStateMachine.Enter<LoadProgressState, string>(_saveLoadService.PlayerProgress.Level);
        }
    }
}
