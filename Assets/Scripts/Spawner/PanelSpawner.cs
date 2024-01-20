using Scripts.Architecture.States;
using Scripts.UI;
using UnityEngine;

namespace Scripts.Spawner
{
    public class PanelSpawner : MonoBehaviour
    {
        [SerializeField] private MenuPanel _menuPanel;
        [SerializeField] private LevelPanel _levelPanel;
        [SerializeField] private LoadingPanel _loadingPanel;

        private readonly int _menuIndex = 1;

        public GameStateMachine StateMachine;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public T GetPanel<T>() where T : Panel
        {
            T panel = _levelPanel.GetComponentInChildren<T>();

            return panel;
        }

        public void CreateCanvas(int sceneIndex)
        {
            if (sceneIndex == _menuIndex)
            {
                _menuPanel.gameObject.SetActive(true);
                _levelPanel.gameObject.SetActive(false);
            }

            else
            {
                _levelPanel.gameObject.SetActive(true);
                _menuPanel.gameObject.SetActive(false);
            }
        }

        public void DisableAllPanels()
        {
            _levelPanel.gameObject.SetActive(false);
            _menuPanel.gameObject.SetActive(false);
        }
    }
}
