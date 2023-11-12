using System.Collections;
using _GAME.Scripts.Controllers;
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

        private int _upgradeButtonLevelIndex;
        private int _fireRateButtonLevelIndex;
        private int _rangeButtonLevelIndex;

        private int _upgradeButtonCost;
        private int _fireRateButtonCost;
        private int _rangeButtonCost;

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
            SetRanges();
            SetFireRates();
            SetPowers();
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

        public int GetWeaponCurrentIndex()
        {
            return PlayerPrefs.GetInt("CurrentIndex");
        }

        public void GetUpgradeButton()
        {
            _upgradeButtonCost = PlayerPrefs.GetInt("UpgradeCost");
            _upgradeButtonLevelIndex = PlayerPrefs.GetInt("UpgradeButtonLevel");
        }

        public void GetFireRateCost()
        {
            _fireRateButtonCost =  PlayerPrefs.GetInt("FireRateCost");
            _fireRateButtonLevelIndex =  PlayerPrefs.GetInt("FireRateButtonLevel");
        }

        public void GetRangeCost()
        {
            _rangeButtonCost = PlayerPrefs.GetInt("RangeCost");
            _rangeButtonLevelIndex = PlayerPrefs.GetInt("RangeButtonLevel");
        }

        public void SetTotalCoin(int value)
        {
            PlayerPrefs.SetInt("TotalCoin", value);
            PlayerPrefs.Save();
        }

        public void SetCurrentWeaponIndex(int value)
        {
            PlayerPrefs.SetInt("CurrentIndex", value);
            PlayerPrefs.Save();
        }
        
        public void GetFireRates()
        {
            if (levelIndex == 1)
            {
                WeaponController.Instance.weaponDataList[0].fireRate = 10;
                WeaponController.Instance.weaponDataList[1].fireRate = 11;
                WeaponController.Instance.weaponDataList[2].fireRate = 12;
            }
            else
            {
                WeaponController.Instance.weaponDataList[0].fireRate = PlayerPrefs.GetFloat("KunaiFireRate");
                WeaponController.Instance.weaponDataList[1].fireRate = PlayerPrefs.GetFloat("ShurikenFireRate");
                WeaponController.Instance.weaponDataList[2].fireRate = PlayerPrefs.GetFloat("BulletFireRate");
            }
        }
        
        public void GetRanges()
        {
            if (levelIndex == 1)
            {
                WeaponController.Instance.weaponDataList[0].range = 10;
                WeaponController.Instance.weaponDataList[1].range = 11;
                WeaponController.Instance.weaponDataList[2].range = 12;
            }
            else
            {
                WeaponController.Instance.weaponDataList[0].range = PlayerPrefs.GetFloat("KunaiRange");
                WeaponController.Instance.weaponDataList[1].range = PlayerPrefs.GetFloat("ShurikenRange");
                WeaponController.Instance.weaponDataList[2].range = PlayerPrefs.GetFloat("BulletRange");
            }
        }
        
        public void GetPowers()
        {
            if (levelIndex == 1)
            {
                WeaponController.Instance.weaponDataList[0].power = 1;
                WeaponController.Instance.weaponDataList[1].power = 1;
                WeaponController.Instance.weaponDataList[2].power = 1;
            }
            else
            {
                WeaponController.Instance.weaponDataList[0].power = PlayerPrefs.GetFloat("KunaiPower");
                WeaponController.Instance.weaponDataList[1].power = PlayerPrefs.GetFloat("ShurikenPower");
                WeaponController.Instance.weaponDataList[2].power = PlayerPrefs.GetFloat("BulletPower");
            }
        }
        
        public void SetFireRates()
        {
            PlayerPrefs.SetFloat("KunaiFireRate", WeaponController.Instance.weaponDataList[0].fireRate);
            PlayerPrefs.SetFloat("ShurikenFireRate", WeaponController.Instance.weaponDataList[1].fireRate);
            PlayerPrefs.SetFloat("BulletFireRate", WeaponController.Instance.weaponDataList[2].fireRate);
            PlayerPrefs.Save();
        }
        
        public void SetRanges()
        {
            PlayerPrefs.SetFloat("KunaiRange", WeaponController.Instance.weaponDataList[0].range);
            PlayerPrefs.SetFloat("ShurikenRange", WeaponController.Instance.weaponDataList[1].range);
            PlayerPrefs.SetFloat("BulletRange", WeaponController.Instance.weaponDataList[2].range);
            PlayerPrefs.Save();
        }
        
        public void SetPowers()
        {
            PlayerPrefs.SetFloat("KunaiPower", WeaponController.Instance.weaponDataList[0].power);
            PlayerPrefs.SetFloat("ShurikenPower", WeaponController.Instance.weaponDataList[1].power);
            PlayerPrefs.SetFloat("BulletPower", WeaponController.Instance.weaponDataList[2].power);
            PlayerPrefs.Save();
        }
        
        public void SetUpgradeCost(int value, int level)
        {
            PlayerPrefs.SetInt("UpgradeCost", value);
            PlayerPrefs.SetInt("UpgradeButtonLevel", level);
            PlayerPrefs.Save();
        }
        
        public void SetFireRateCost(float value, int level)
        {
            PlayerPrefs.SetFloat("FireRateCost", value);
            PlayerPrefs.SetInt("FireRateButtonLevel", level);
            PlayerPrefs.Save();
        }
        
        public void SetRangeCost(int value, int level)
        {
            PlayerPrefs.SetInt("FireRateCost", value);
            PlayerPrefs.SetInt("FireRateButtonLevel", level);
            PlayerPrefs.Save();
        }
    }
}