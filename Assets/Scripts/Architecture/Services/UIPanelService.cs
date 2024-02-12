using Scripts.Architecture.States;
using Scripts.UI.Panels;

namespace Scripts.Architecture.Services
{
    public class UIPanelService : IUIPanelService
    {
        private readonly LevelPanel _levelPanel;

        public UIPanelService(GameStateMachine stateMachine, LevelPanel levelPanel, LoadingPanel loadingPanel)
        {
            StateMachine = stateMachine;
            _levelPanel = levelPanel;
            LoadingPanel = loadingPanel;
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
    }
}
