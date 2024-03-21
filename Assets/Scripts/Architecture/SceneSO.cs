using Scripts.Architecture.Services;
using UnityEngine;

namespace Scripts.Architecture
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "SceneConfig/new Scene")]
    public class SceneSO : ScriptableObject
    {
        private const int ZeroStarAmount = 0;

        [SerializeField] private string _sceneName;
        [SerializeField] private GameObject _zombie;
        [SerializeField] private int _id;
        [SerializeField] private bool _isAvailable;
        [SerializeField] private int _meduimScore;
        [SerializeField] private int _highScore;

        private bool _isComplited;
        private ISceneService _sceneService;

        public int AmountOfStars { get; private set; }

        public bool IsAvailable { get { return _isAvailable; } }
        public bool IsComplited { get { return _isComplited; } }
        public string Name { get { return _sceneName; } }
        public int Id { get { return _id; } }
        public GameObject Zombie { get { return _zombie; } }

        public void AddListener(ISceneService sceneService)
        {
            _sceneService = sceneService;

            _sceneService.SceneComplited += OnSceneComplited;
            _sceneService.SceneAvailable += OnSceneAvailable;
        }

        public void ResetProgress()
        {
            _isComplited = false;
            AmountOfStars = ZeroStarAmount;

            AddListener(_sceneService);
        }

        private void RemoveListener()
        {
            _sceneService.SceneAvailable -= OnSceneAvailable;
        }

        private void SetRate()
        {
            IStarCountService starCountService = AllServices.Container.Single<IStarCountService>();
            AmountOfStars = starCountService.CountStars(_meduimScore, _highScore);
        }

        private void OnSceneAvailable(SceneSO scene)
        {
            if (scene == this)
            {
                _isAvailable = true;
                RemoveListener();
            }
        }

        private void OnSceneComplited(SceneSO scene)
        {
            if (scene == this)
            {
                _isComplited = true;
                _sceneService.SceneComplited -= OnSceneComplited;

                SetRate();
            }
        }
    }
}
