using Agava.YandexGames;
using Scripts.Characters;
using Scripts.UI.Panels;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Architecture
{
    public class LeaderBoard : MonoBehaviour
    {
        private const string AnonymousName = "Anonymous";
        private const string LeaderBoardName = "LeaderBoard";
        private readonly List<LeaderBoardPlayer> _leaderBoardPlayers = new();

        private LeaderBoardPanel _leaderBoardPanel;

        private void Start()
        {
            _leaderBoardPanel = GetComponent<LeaderBoardPanel>();
        }

        public void SetPlayer(int score)
        {
            if (PlayerAccount.IsAuthorized == false)
                return;
          
            Leaderboard.GetPlayerEntry(LeaderBoardName, (result) =>
                {
                    if(result.score < score)
                        Leaderboard.SetScore(LeaderBoardName, score);
                });



            Fill();
        }

        private void Fill()
        {
            _leaderBoardPlayers.Clear();

            if (PlayerAccount.IsAuthorized == false)
                return;

            Leaderboard.GetEntries(LeaderBoardName, (result) =>
            {
                foreach (var entry in result.entries)
                {
                    var rank = entry.rank;
                    var score = entry.score;
                    var name = entry.player.publicName;
            
                    if (string.IsNullOrEmpty(name))
                        name = AnonymousName;
            
                    _leaderBoardPlayers.Add(new LeaderBoardPlayer(rank, name, score));
                }
            
            });

            _leaderBoardPanel.ConstractLeaderBoard(_leaderBoardPlayers);
        }
    }
}