using Scripts.Architecture.States;
using Scripts.UI.Panels;

namespace Scripts.Architecture.Services
{
    public interface IUIPanelService : IService
    {
        LoadingPanel LoadingPanel { get; }
        GameStateMachine StateMachine { get; }

        void CreateCanvas(string sceneName);
        T GetPanel<T>() where T : Panel;
    }
}