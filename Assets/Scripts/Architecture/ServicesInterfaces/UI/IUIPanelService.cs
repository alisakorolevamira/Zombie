using Architecture.States;
using UI.Panels.Common;

namespace Architecture.ServicesInterfaces.UI
{
    public interface IUIPanelService : IService
    {
        GameStateMachine StateMachine { get; }
        LoadingPanel LoadingPanel { get; }

        void Initialize();
        void CreateCanvas(string sceneName);
        T GetCard<T>()
            where T : Panel;
    }
}