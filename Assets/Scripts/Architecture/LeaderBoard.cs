using Agava.YandexGames;
using Cysharp.Threading.Tasks;
using Scripts.Characters;
using Scripts.UI.Panels;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Architecture
{
    public class LeaderBoard : MonoBehaviour
    {
        private readonly List<LeaderBoardPlayer> _leaderBoardPlayers = new();

        [SerializeField] private LeaderBoardPanel _leaderBoardPanel;

        public async UniTask SetPlayer(int score)
        {
            if (PlayerAccount.IsAuthorized == false)
                return;

            var task = new UniTaskCompletionSource();

            Leaderboard.GetPlayerEntry(Constants.LeaderBoardName, (result) =>
            {
                if (result.score < score)
                {
                    Leaderboard.SetScore(Constants.LeaderBoardName, score);
                }

                task.TrySetResult();
            }, 
            (error) =>
            {
                _leaderBoardPanel.ErrorPanel.Open();
                _leaderBoardPanel.Close();
                return;
            });

            await task.Task;

            await Fill();
        }

        private async UniTask Fill()
        {
            _leaderBoardPlayers.Clear();

            if (PlayerAccount.IsAuthorized == false)
                return;

            var task = new UniTaskCompletionSource();

            Leaderboard.GetEntries(Constants.LeaderBoardName, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    var rank = entry.rank;
                    var score = entry.score;
                    var name = entry.player.publicName;
            
                    if (string.IsNullOrEmpty(name))
                        name = Constants.AnonymousName;
            
                    _leaderBoardPlayers.Add(new LeaderBoardPlayer(rank, name, score));
                }

                task.TrySetResult();
            
            },
            (error) =>
            {
                _leaderBoardPanel.ErrorPanel.Open();
                _leaderBoardPanel.Close();

                return;
            });

            await task.Task;

            _leaderBoardPanel.ConstractLeaderBoard(_leaderBoardPlayers);
        }
    }
}