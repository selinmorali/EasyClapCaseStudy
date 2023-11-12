using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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

        private IEnumerator Loading()
        {
            LoadStarting();
            yield return new WaitForSeconds(0.05f);
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
        
        public int GetTotalCoinValue()
        {
            return PlayerPrefs.GetInt("TotalCoin");
        }

        public void AddToTotalCoin(int value)
        {
            PlayerPrefs.SetInt("TotalCoin", value);
            PlayerPrefs.Save();
        }
    }
}