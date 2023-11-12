using _GAME.Scripts.Managers.LevelSystem;
using TMPro;
using UnityEngine;

namespace _GAME.Scripts.Managers
{
    public class GUIManager : MonoBehaviour
    {
        public GameObject tutorialPanel;
        public GameObject gamePanel;
        public GameObject settingsPanel;
        public GameObject successPanel;
        public GameObject buttons;
        public TextMeshProUGUI levelIndexText;
        public ParticleSystem particleForSuccess;

        private void OnEnable()
        {
            EventManager.OnLoadedFirstLevel.AddListener(OpenTutorialPanel);
            EventManager.OnFirstClick.AddListener(() =>
            {
                if (LevelManager.Instance.levelIndex == 1)
                {
                    CloseTutorialPanel();
                }
                else
                {
                    CloseButtons();
                }
            });
            EventManager.OnLevelSuccess.AddListener(OpenSuccessPanel);
            EventManager.OnLevelSuccess.AddListener(PlaySuccessParticle);
            EventManager.OnOpenButtons.AddListener(OpenButtons);
        }

        private void OnDisable()
        {
            EventManager.OnLoadedFirstLevel.RemoveListener(OpenTutorialPanel);
            EventManager.OnFirstClick.RemoveListener(() =>
            {
                if (LevelManager.Instance.levelIndex == 1)
                {
                    CloseTutorialPanel();
                }
                else
                {
                    CloseButtons();
                }
            });
            EventManager.OnLevelSuccess.RemoveListener(OpenSuccessPanel);
            EventManager.OnLevelSuccess.RemoveListener(PlaySuccessParticle);
            EventManager.OnOpenButtons.RemoveListener(OpenButtons);
        }

        private void Start()
        {
            levelIndexText.text = "LEVEL" + LevelManager.Instance.textIndex;
        }

        private void OpenTutorialPanel()
        {
            tutorialPanel.SetActive(true);
        }

        private void CloseTutorialPanel()
        {
            tutorialPanel.SetActive(false);
        }

        private void OpenButtons()
        {
            buttons.SetActive(true);
        }

        private void OpenSuccessPanel()
        {
            successPanel.SetActive(true);
        }

        private void PlaySuccessParticle()
        {
            particleForSuccess.Play();
        }

        private void CloseButtons()
        {
            buttons.SetActive(false);
        }

        public void ClickUpgradeButton()
        {
            EventManager.OnWeaponUpgradeButtonPressed.Invoke();
        }
    }
}