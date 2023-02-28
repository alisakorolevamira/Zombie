using UnityEngine;
using UnityEngine.SceneManagement;

public class WinPanel : Panel
{
    [SerializeField] private Zombie _zombie;
    [SerializeField] private string _nameOfNextLevel;

    private void OnEnable()
    {
        _zombie.Died += Open;
        _button.onClick.AddListener(OnNextLevelButtonClick);
    }

    private void OnDisable()
    {
        _zombie.Died -= Open;
        _button.onClick.RemoveListener(OnNextLevelButtonClick);
    }

    private void OnNextLevelButtonClick()
    {
        SceneManager.LoadScene(_nameOfNextLevel);
    }
}
