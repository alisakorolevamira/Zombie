using Agava.YandexGames;
using UI.Panels.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons.Menu
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