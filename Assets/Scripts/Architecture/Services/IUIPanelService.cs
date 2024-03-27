using Scripts.Architecture.States;
using Scripts.UI.Panels;
using UnityEngine.UI;

namespace Scripts.Architecture.Services
{
    public interface IUIPanelService : IService
    {
        GameStateMachine StateMachine { get; }
        LoadingPanel LoadingPanel { get; }

        void Initialize();
        void CreateCanvas(string sceneName);
        T GetCard<T>() where T : Panel;
    }
}