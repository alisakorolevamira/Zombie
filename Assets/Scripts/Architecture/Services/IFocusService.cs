using System;

namespace Scripts.Architecture.Services
{
    public interface IFocusService : IService, IDisposable
    {
        bool IsGameStopped { get; set; }

        void Initialize();
        void PauseGame(bool value);
    }
}