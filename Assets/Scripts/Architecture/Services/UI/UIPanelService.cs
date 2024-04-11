using Architecture.Factory;
using Architecture.ServicesInterfaces.UI;
using Architecture.States;
using Constants;
using Constants.UI;
using UI.Panels.Common;
using UI.Panels.GameLevel;
using UI.Panels.Menu;

namespace Architecture.Services.UI
{
    public class UIPanelService : IUIPanelService
    {
        private readonly IGameFactory _gameFactory;

        private LevelPanel _levelPanel;
        private MenuPanel _menuPanel;

        public UIPanelService(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            StateMachine = stateMachine;
            _gameFactory = gameFactory;
        }

        public GameStateMachine StateMachine { get; private set; }
        public LoadingPanel LoadingPanel { get; private set; }

        public T GetCard<T>() where T : Panel
        {
            T panel = _levelPanel.GetComponentInChildren<T>();

            return panel;
        }

        public void CreateCanvas(string sceneName)
        {
            if (sceneName == LevelConstants.Menu)
            {
                _levelPanel.Close();
                _menuPanel.Open();
            }

            else
            {
                _menuPanel.Close();
                _levelPanel.Open();
            }
        }

        public void Initialize()
        {
            LoadingPanel = _gameFactory.SpawnObject(UIConstants.LoadingPanelPath).GetComponent<LoadingPanel>();
            _levelPanel = _gameFactory.SpawnObject(UIConstants.LevelCanvasPath).GetComponent<LevelPanel>();
            _menuPanel = _gameFactory.SpawnObject(UIConstants.MenuCanvasPath).GetComponent<MenuPanel>();

            LoadingPanel.Open();
            _levelPanel.Close();
        }
    }
}