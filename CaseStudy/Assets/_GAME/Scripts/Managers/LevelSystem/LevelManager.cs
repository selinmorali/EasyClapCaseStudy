using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GAME.Scripts.Managers.LevelSystem
{
    public class LevelManager : MonoSingleton<LevelManager>
    {
        public int levelIndex;
        public int textIndex;
        public Level currentLevel;

        private void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject); 
                return;
            }

            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += AssignCurrentLevel;
            StartCoroutine(LevelLoading());
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
                levelIndex = Random.Range(1, SceneManager.sceneCountInBuildSettings - 1);
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
            textIndex = 1;
            Set();
            SceneManager.LoadScene(levelIndex);
        }

        private void IncreaseIndex()
        {
            levelIndex += 1;
            textIndex += 1;
        }

        private IEnumerator LevelLoading()
        {
            LoadStarting();
            LevelIndexCheck();
            yield return null;
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

        private void AssignCurrentLevel(Scene scene, LoadSceneMode mode)
        {
            GameObject levelObject = GameObject.FindWithTag("Level"); 
            if (levelObject == null)
            {
                return;
            }
            currentLevel = levelObject.GetComponent<Level>();
        }
    }
}