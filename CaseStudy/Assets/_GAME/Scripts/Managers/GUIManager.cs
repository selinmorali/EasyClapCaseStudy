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
        public GameObject buttons;
        public TextMeshProUGUI levelIndexText;

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

        private void CloseButtons()
        {
            buttons.SetActive(false);
        }
    }
}