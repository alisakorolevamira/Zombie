using Architecture.Services;
using Architecture.ServicesInterfaces.GameLevel;
using Architecture.ServicesInterfaces.UI;
using Constants;
using Constants.UI;
using Progress;
using UnityEngine;

namespace Architecture.GameLevel
{
    public class Level
    {
        private readonly int _mediumScore;
        private readonly int _highScore;
        private readonly ILevelService _levelService;

        public Level(int mediumScore, int highScore, string name, int id, bool isAvailable, GameObject zombie,
            ILevelService levelService)
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

        public int Id { get; }
        public string Name { get; private set; }
        public int AmountOfStars { get; private set; }
        public GameObject Zombie { get; private set; }
        public bool IsAvailable { get; private set; }
        public bool IsCompleted { get; private set; }

        public void Initialize(LevelProgress levelProgress)
        {
            AmountOfStars = levelProgress.Stars;

            if (levelProgress.IsAvailable.ToLower() == LevelConstants.DefaultAvailability)
                IsAvailable = false;

            else
                IsAvailable = true;

            if (Id == LevelConstants.FirstLevelId)
                IsAvailable = true;

            if (AmountOfStars > StarsConstants.DefaultAmountOfStars)
                IsCompleted = true;
        }

        private void AddListener()
        {
            _levelService.LevelCompleted += OnSceneCompleted;
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

        private void OnSceneCompleted(Level level)
        {
            if (level == this)
            {
                IsCompleted = true;
                _levelService.LevelCompleted -= OnSceneCompleted;

                SetRate();
            }
        }
    }
}