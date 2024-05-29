using System.Collections.Generic;
using Agava.YandexGames;
using Architecture.LeaderBoard;
using Architecture.Services;
using Architecture.ServicesInterfaces.Data;
using Characters.UI;
using Cysharp.Threading.Tasks;
using UI.Panels.Common;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Panels.Menu
{
    [RequireComponent(typeof(LeaderBoard))]

    public class LeaderBoardPanel : Panel
    {
        private readonly List<LeaderBoardElement> _leaderBoardElements = new ();

        [SerializeField] private LeaderBoardElement _leaderBoardElementPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Button _leaderBoardButton;
        [SerializeField] private Button _closeButton;
        [SerializeField] private Panel _unsuccessfulAuthorizationPanel;
        [SerializeField] private LeaderBoard _leaderBoard;

        private IPlayerDataService _playerDataService;
        [field: SerializeField] public Panel ErrorPanel { get; private set; }

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
            _playerDataService = AllServices.Container.Single<IPlayerDataService>();
        }

        public void ConstractLeaderBoard(List<LeaderBoardPlayer> leaderBoardPlayers)
        {
            Clear();

            foreach (LeaderBoardPlayer player in leaderBoardPlayers)
            {
                LeaderBoardElement leaderBoardElement = Instantiate(_leaderBoardElementPrefab, _container);
                leaderBoardElement.Initialize(player.Name, player.Rank, player.Score);

                _leaderBoardElements.Add(leaderBoardElement);
            }
        }

        public override async void Open()
        {
            base.Open();

            await CheckPersonalData();
        }

        private async UniTask CheckPersonalData()
        {
            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();

                await _leaderBoard.SetPlayer(_playerDataService.PlayerProgress.Score);
            }

            else
            {
                _unsuccessfulAuthorizationPanel.Open();
                Close();
            }
        }

        private void Clear()
        {
            foreach (LeaderBoardElement element in _leaderBoardElements)
                Destroy(element.gameObject);

            _leaderBoardElements.Clear();
        }
    }
}