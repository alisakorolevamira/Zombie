using Scripts.Architecture.Services;
using Scripts.Architecture.States;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Panels
{
    public class MenuPanel : Panel
    {
        private const int One = 1;
        private const int Two = 2;
        private const int Three = 3;
        private const int Four = 4;
        private const int Five = 5;
        private const int Six = 6;
        private const int Seven = 7;
        private const int Eight = 8;
        private const int Nine = 9;
        private const int Ten = 10;
        private const int Eleven = 11;

        [SerializeField] private List<Button> _chooseLevelButtons;
        
        private GameStateMachine _gameStateMachine;
        private ISaveLoadService _saveLoadService;
        private IUIPanelService _panelService;

        private void Start()
        {
            _saveLoadService = AllServices.Container.Single<ISaveLoadService>();
            _panelService = AllServices.Container.Single<IUIPanelService>();
            _gameStateMachine = _panelService.StateMachine;

            EnableChooseLevelButtons();
        }

        public void OpenAnyLevel(string sceneName)
        {
            _saveLoadService.ResetProgress();
            _saveLoadService.SaveProgress();

            _gameStateMachine.Enter<LoadProgressState, string>(sceneName);
        }

        public void OpenProgressLevel()
        {
            _gameStateMachine.Enter<LoadProgressState, string>(_saveLoadService.PlayerProgress.Level);
        }

        private void EnableChooseLevelButtons()
        {
            switch (_saveLoadService.PlayerProgress.Level)
            {
                case Constants.Menu:
                    break;

                case Constants.FirstLevel:
                    break;

                case Constants.SecondLevel:
                    EnableLevel(One);
                    break;

                case Constants.ThirdLevel:
                    EnableLevel(Two);
                    break;

                case Constants.FourthLevel:
                    EnableLevel(Three);
                    break;

                case Constants.FifthLevel:
                    EnableLevel(Four);
                    break;

                case Constants.SixthLevel:
                    EnableLevel(Five);
                    break;

                case Constants.SeventhLevel:
                    EnableLevel(Six);
                    break;

                case Constants.EighthLevel:
                    EnableLevel(Seven);
                    break;

                case Constants.NinthLevel:
                    EnableLevel(Eight);
                    break;

                case Constants.TenthLevel:
                    EnableLevel(Nine);
                    break;

                case Constants.EleventhLevel:
                    EnableLevel(Ten);
                    break;

                case Constants.TwelthLevel:
                    EnableLevel(Eleven);
                    break;

            }
        }

        private void EnableLevel(int times)
        {
            while (times != 0)
            {
                Button button = _chooseLevelButtons.Find(x => x.interactable == false);
                button.interactable = true;
                times--;
            }
        }
    }
}
