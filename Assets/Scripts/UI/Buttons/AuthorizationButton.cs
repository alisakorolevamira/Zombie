using Agava.YandexGames;
using Scripts.UI.Panels;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Buttons
{
    public class AuthorizationButton : MonoBehaviour
    {
        [SerializeField] private Button _authorizationButton;
        [SerializeField] private MenuComponentPanel _alreadyAuthorizedPanel;

        private void OnEnable()
        {
            _authorizationButton.onClick.AddListener(Authorization);
        }

        private void OnDisable()
        {
            _authorizationButton.onClick.RemoveListener(Authorization);
        }

        private void Authorization()
        {
            if (PlayerAccount.IsAuthorized)
                _alreadyAuthorizedPanel.Open();

            else
                PlayerAccount.Authorize();
        }
    }
}