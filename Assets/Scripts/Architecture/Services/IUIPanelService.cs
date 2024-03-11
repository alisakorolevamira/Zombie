using Scripts.Architecture.States;
using Scripts.UI.Panels;

namespace Scripts.Architecture.Services
{
    public interface IUIPanelService : IService
    {
        GameStateMachine StateMachine { get; }
        LoadingPanel LoadingPanel { get; }

        void CreateCanvas(string sceneName);
        void Initialize();
        T GetPanel<T>() where T : Panel;
    }
}