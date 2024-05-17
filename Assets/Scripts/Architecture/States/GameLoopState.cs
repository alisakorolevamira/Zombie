using System.Collections;
using Agava.YandexGames;
using Architecture.ServicesInterfaces.UI;

namespace Architecture.States
{
    public class GameLoopState : IState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IUIPanelService _panelService;
        
        private bool _isLoaded = false;

        public GameLoopState(ICoroutineRunner coroutineRunner, IUIPanelService panelService)
        {
            _panelService = panelService;
            _coroutineRunner = coroutineRunner;
        }
        
        public void Enter() 
        {
            if (_isLoaded)
                return;

            _isLoaded = true;
            _coroutineRunner.StartCoroutine(SendGameReady());
        }

        public void Exit() { }

        private IEnumerator SendGameReady()
        {
            while (!_panelService.LoadingPanel.IsClosed)
                yield return null;
            
            YandexGamesSdk.GameReady();
        }
    }
}