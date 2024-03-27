using Scripts.Architecture.Services;
using Scripts.Progress;
using UnityEngine;

namespace Scripts.Architecture
{
    public class Level
    {
        private const int ZeroStarAmount = 0;
        private const string False = "false";

        private readonly int _mediumScore;
        private readonly int _highScore;
        private readonly ILevelService _levelService;

        public Level(int mediumScore, int highScore, string name, int id, bool isAvailable, GameObject zombie, ILevelService levelService)
        {
            _mediumScore = mediumScore;
            _highScore = highScore;
            Name = name;
            Id = id;
            IsAvailable = isAvailable;
            Zombie = zombie;
            _levelService = levelService;

            AddListener();
        }

        public string Name { get; private set; }
        public int Id { get; private set; }
        public int AmountOfStars { get; private set; }
        public GameObject Zombie { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsComplited { get; private set; }

        public void Initialize(LevelProgress levelProgress)
        {
            AmountOfStars = levelProgress.Stars;
            
            if(levelProgress.IsAvailable.ToLower() == False)
                IsAvailable = false;

            else
                IsAvailable = true;

            if (Id == 1)
                IsAvailable = true;

            if (AmountOfStars > ZeroStarAmount)
                IsComplited = true;
        }

        public void ResetProgress()
        {
            IsComplited = false;
            AmountOfStars = ZeroStarAmount;

            AddListener();
        }

        private void AddListener()
        {
            _levelService.LevelComplited += OnSceneComplited;
            _levelService.LevelAvailable += OnSceneAvailable;
        }

        private void RemoveListener()
        {
            _levelService.LevelAvailable -= OnSceneAvailable;
        }

        private void SetRate()
        {
            IStarCountService starCountService = AllServices.Container.Single<IStarCountService>();
            AmountOfStars = starCountService.CountStars(_mediumScore, _highScore);
        }

        private void OnSceneAvailable(Level level)
        {
            if (level == this)
            {
                IsAvailable = true;
                RemoveListener();
            }
        }

        private void OnSceneComplited(Level level)
        {
            if (level == this)
            {
                IsComplited = true;
                _levelService.LevelComplited -= OnSceneComplited;

                SetRate();
            }
        }
    }
}
