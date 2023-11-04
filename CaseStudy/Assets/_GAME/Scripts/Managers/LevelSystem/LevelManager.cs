using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GAME.Scripts.Managers.LevelSystem
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public int levelIndex;
        public int textIndex;
        public Level currentLevel => _currentLevel;
        private Level _currentLevel;
        
        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject); 
                return;
            }

            DontDestroyOnLoad(gameObject);
            StartCoroutine(Loading());
        }

        private void OnEnable()
        {
            EventManager.OnNextButtonPressed.AddListener(Load);
        }

        private void OnDisable()
        {
            EventManager.OnNextButtonPressed.RemoveListener(Load);
        }
        
        private void Get()
        {
            levelIndex = PlayerPrefs.GetInt("Index");
            textIndex = PlayerPrefs.GetInt("TextIndex");
        }
        
        private void Set()
        {
            PlayerPrefs.SetInt("Index", levelIndex);
            PlayerPrefs.SetInt("TextIndex", textIndex);
            PlayerPrefs.Save();
        }
        
        private void Load()
        {
            Get();
            IncreaseIndex();

            if (levelIndex >= SceneManager.sceneCountInBuildSettings - 1)
            {
                levelIndex = Random.Range(2, SceneManager.sceneCountInBuildSettings - 1);
                Set();
            }
            else
            {
                Set();
            }

            SceneManager.LoadScene(levelIndex);
        }

        private void Reload()
        {
            SceneManager.LoadScene(levelIndex);
        }
        
        private void LoadStarting()
        {
            Get();
            if (levelIndex == 0)
            {
                IncreaseIndex();
                Set();
            }
            SceneManager.LoadScene(levelIndex);
        }

        private void IncreaseIndex()
        {
            levelIndex += 1;
            textIndex += 1;
        }

        private void LevelIndexCheck()
        {
            if (levelIndex == 1)
            {
                EventManager.OnLoadedFirstLevel.Invoke();
            }
            else
            {
                EventManager.OnOpenButtons.Invoke();
            }
        }

        private IEnumerator Loading()
        {
            LoadStarting();
            yield return new WaitForSeconds(0.2f);
            LevelIndexCheck();
        }
    }
}