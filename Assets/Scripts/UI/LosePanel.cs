using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : Panel
{
    [SerializeField] private Zombie _zombie;

    private void OnEnable()
    {
        _zombie.AllSitizensDied += Open;

        _button.onClick.AddListener(OnRestartButtonClick);
    }

    private void OnDisable()
    {
        _zombie.AllSitizensDied -= Open;

        _button.onClick.RemoveListener(OnRestartButtonClick);
    }

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
