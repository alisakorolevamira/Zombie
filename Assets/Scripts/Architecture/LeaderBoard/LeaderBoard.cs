using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Characters.UI;
using Constants.UI;
using Cysharp.Threading.Tasks;
using UI.Panels.Menu;
using UnityEngine;

namespace Architecture.LeaderBoard
{
    public class LeaderBoard : MonoBehaviour
    {
        private readonly List<LeaderBoardPlayer> _leaderBoardPlayers = new();

        [SerializeField] private LeaderBoardPanel _leaderBoardPanel;

        public async UniTask SetPlayer(int score)
        {
            var task = new UniTaskCompletionSource();

            Leaderboard.GetPlayerEntry(LeaderBoardConstants.LeaderBoardName, (result) =>
            {
                if (result.score < score)
                {
                    Leaderboard.SetScore(LeaderBoardConstants.LeaderBoardName, score);
                }

                task.TrySetResult();
            }, 
            (error) =>
            {
                _leaderBoardPanel.ErrorPanel.Open();
                _leaderBoardPanel.Close();

                task.TrySetException(new Exception(error));
            });

            try
            {
                await task.Task;
                await Fill();
            }
            
            catch(OperationCanceledException exception)
            {
                Debug.LogWarning($"{nameof(exception)}");
            }
        }

        private async UniTask Fill()
        {
            _leaderBoardPlayers.Clear();

            if (PlayerAccount.IsAuthorized == false)
                return;

            var task = new UniTaskCompletionSource();

            Leaderboard.GetEntries(LeaderBoardConstants.LeaderBoardName, (result) =>
            {
                foreach (LeaderboardEntryResponse entry in result.entries)
                {
                    int rank = entry.rank;
                    int score = entry.score;
                    string name = entry.player.publicName;
            
                    if (string.IsNullOrEmpty(name))
                        name = LeaderBoardConstants.AnonymousName;
            
                    _leaderBoardPlayers.Add(new LeaderBoardPlayer(rank, name, score));
                }

                task.TrySetResult();
            
            },
            (error) =>
            {
                _leaderBoardPanel.ErrorPanel.Open();
                _leaderBoardPanel.Close();
            });

            await task.Task;

            _leaderBoardPanel.ConstractLeaderBoard(_leaderBoardPlayers);
        }
    }
}