using Scripts.Architecture.Factory;
using Scripts.Architecture.States;
using Scripts.UI.Panels;

namespace Scripts.Architecture.Services
{
    public class UIPanelService : IUIPanelService
    {
        private readonly IGameFactory _gameFactory;

        private LevelPanel _levelPanel;

        public UIPanelService(GameStateMachine stateMachine, IGameFactory gameFactory)
        {
            StateMachine = stateMachine;
            _gameFactory = gameFactory;
        }

        public GameStateMachine StateMachine { get; private set; }
        public LoadingPanel LoadingPanel { get; private set; }

        public T GetPanel<T>() where T : Panel
        {
            T panel = _levelPanel.GetComponentInChildren<T>();

            return panel;
        }

        public void CreateCanvas(string sceneName)
        {
            if (sceneName == Constants.Menu)
                _levelPanel.Close();

            else
                _levelPanel.Open();
        }

        public void Initialize()
        {
            LoadingPanel = _gameFactory.SpawnObject(Constants.LoadingPanelPath).GetComponent<LoadingPanel>();
            _levelPanel = _gameFactory.SpawnObject(Constants.LevelCanvasPath).GetComponent<LevelPanel>();

            _levelPanel.Close();
            LoadingPanel.Open();
        }
    }
}
