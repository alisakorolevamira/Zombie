using Scripts.Architecture.Services;
using TMPro;
using UnityEngine;

namespace Scripts.UI
{
    public class ScoreText : MonoBehaviour
    {
        private TMP_Text _score;
        private IPlayerScoreService _playerScoreService;

        private void OnEnable()
        {
            _score = GetComponent<TMP_Text>();
            _playerScoreService = AllServices.Container.Single<IPlayerScoreService>();
            OnScoreChanged();

            _playerScoreService.ScoreChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _playerScoreService.ScoreChanged += OnScoreChanged;
        }

        private void OnScoreChanged()
        {
            _score.text = _playerScoreService.Score.ToString();
        }
    }
}
