using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Scripts.Architecture;
using Scripts.Architecture.Services;
using Scripts.Characters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    [RequireComponent(typeof(LeaderBoard))]

    public class LeaderBoardPanel : Panel
    {
        private readonly List<LeaderBoardElement> _leaderBoardElements = new();

        [SerializeField] private LeaderBoardElement _leaderBoardElementPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Button _leaderBoardButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Panel _unsuccessfulAuthorizationPanel;
        [SerializeField] private LeaderBoard _leaderBoard;
        [SerializeField] private Panel _errorPanel;

        private ISaveLoadService _saveLoadService;

        public Panel ErrorPanel => _errorPanel;

        private void OnEnable()
        {
            _leaderBoardButton.onClick.AddListener(Open);
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _leaderBoardButton.onClick.RemoveListener(Open);
            _closeButton.onClick.RemoveListener(Close);
        }

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
        }

        public void ConstractLeaderBoard(List<LeaderBoardPlayer> leaderBoardPlayers)
        {
            Clear();
            
            foreach (var player in leaderBoardPlayers)
            {
                LeaderBoardElement leaderBoardElement = Instantiate(_leaderBoardElementPrefab, _container);
                leaderBoardElement.Initialize(player.Name, player.Rank, player.Score);

                _leaderBoardElements.Add(leaderBoardElement);
            }
        }

        public async override void Open()
        {
            base.Open();

            await CheckPersonalData();
        }

        private async UniTask CheckPersonalData()
        {
            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
                await _leaderBoard.SetPlayer(_saveLoadService.PlayerProgress.Score);
            }
            
            else
            {
                _unsuccessfulAuthorizationPanel.Open();
                Close();
            }
        }

        private void Clear()
        {
            foreach (var element in _leaderBoardElements)
                Destroy(element.gameObject);

            _leaderBoardElements.Clear();
        }
    }
}
