using Scripts.Architecture.States;
using Scripts.UI.Panels;
using UnityEngine.UI;

namespace Scripts.Architecture.Services
{
    public interface IUIPanelService : IService
    {
        GameStateMachine StateMachine { get; }
        LoadingPanel LoadingPanel { get; }

        void CreateCanvas(string sceneName);
        void Initialize();
        T GetCard<T>() where T : Panel;
    }
}