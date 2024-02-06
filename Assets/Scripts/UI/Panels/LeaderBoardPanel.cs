using Agava.YandexGames;
using Scripts.Architecture;
using Scripts.Architecture.Services;
using Scripts.Characters;
using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] private UnsuccessfulAuthorizationPanel _unsuccessfulAuthorizationPanel;

        private LeaderBoard _leaderBoard;
        private ISaveLoadService _saveLoadService;

        private void OnEnable()
        {
            _leaderBoard = GetComponent<LeaderBoard>();
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();

            _leaderBoardButton.onClick.AddListener(Open);
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDisable()
        {
            _leaderBoardButton.onClick.RemoveListener(Open);
            _closeButton.onClick.RemoveListener(Close);
        }

        public void ConstractLeaderBoard(List<LeaderBoardPlayer> leaderBoardPlayers)
        {
            Clear();
            
            var sortedPlayers = from player in leaderBoardPlayers
                                orderby player.Score
                                select player;

            foreach (var player in sortedPlayers)
            {
                LeaderBoardElement leaderBoardElement = Instantiate(_leaderBoardElementPrefab, _container);
                leaderBoardElement.Initialize(player.Name, player.Rank, player.Score);

                _leaderBoardElements.Add(leaderBoardElement);
            }
        }

        public override void Open()
        {
            PlayerAccount.Authorize();

            base.Open();

            if (PlayerAccount.IsAuthorized)
            {
                PlayerAccount.RequestPersonalProfileDataPermission();
                _leaderBoard.SetPlayer(_saveLoadService.PlayerProgress.Score);
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
