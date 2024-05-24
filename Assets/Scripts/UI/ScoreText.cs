using Architecture.Services;
using Architecture.ServicesInterfaces.Player;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;

        private IPlayerScoreService _playerScoreService;

        private void OnDisable()
        {
            _playerScoreService.ScoreChanged -= OnScoreChanged;
        }

        private void Start()
        {
            _playerScoreService = AllServices.Container.Single<IPlayerScoreService>();
            
            OnScoreChanged();
            _playerScoreService.ScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged() => _score.text = _playerScoreService.Score.ToString();
    }
}