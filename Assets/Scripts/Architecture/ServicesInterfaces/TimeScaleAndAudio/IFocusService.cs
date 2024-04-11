using System;

namespace Architecture.ServicesInterfaces.TimeScaleAndAudio
{
    public interface IFocusService : IService, IDisposable
    {
        void Initialize();
    }
}