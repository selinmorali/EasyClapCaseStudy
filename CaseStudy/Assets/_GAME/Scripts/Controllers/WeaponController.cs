using System.Collections.Generic;
using _GAME.Scripts.Managers;
using _GAME.Scripts.Play.Player;
using _GAME.Scripts.Play.Shoot;
using _GAME.Scripts.SO;
using UnityEngine;
using Type = _GAME.Scripts.Play.Gates.Type;

namespace _GAME.Scripts.Controllers
{
    public class WeaponController : MonoSingleton<WeaponController>
    {
        public List<Weapon> weaponPrefabs = new();
        public List<WeaponData> weaponDataList = new();
        public int currentWeaponIndex;
        public AnimationController animationController;

        private void OnEnable()
        {
            EventManager.OnGetShotValue.AddListener(SetShotValue);
        }

        private void OnDisable()
        {
            EventManager.OnGetShotValue.RemoveListener(SetShotValue);
        }

        private void SetShotValue(Type type, float value)
        {
            switch (type)
            {
                case Type.Range:
                    SetRange(value);
                    break;
                case Type.Power:
                    SetPower(value);
                    break;
                case Type.FireRate:
                    SetFireRate(value);
                    break;
                default:
                    Debug.LogError("Error");
                    break;
            }
        }

        private void SetFireRate(float input)
        {
            for (int i = 0; i < weaponDataList.Count; i++)
            {
                weaponDataList[i].fireRate += input;
                CheckValues(i, 1);
            }
            animationController.CalculateAnimTime();
        }

        private void SetRange(float input)
        {
            for (int i = 0; i < weaponDataList.Count; i++)
            {
                weaponDataList[i].range += input;
                CheckValues(i, 2);
            }
        }

        private void SetPower(float input)
        {
            for (int i = 0; i < weaponDataList.Count; i++)
            {
                weaponDataList[i].power += input;
                CheckValues(i, 3);
            }
        }

        public Weapon GetCurrentWeapon()
        {
            return weaponPrefabs[currentWeaponIndex];
        }

        private void CheckValues(int input, int typeIndex)
        {
            switch (typeIndex)
            {
                case 1: 
                    weaponDataList[input].fireRate = Mathf.Clamp(weaponDataList[input].fireRate, 1f, 100f);
                    break;
                case 2:
                    weaponDataList[input].range = Mathf.Clamp(weaponDataList[input].range, 1f, 100f);
                    break;
                case 3:
                    weaponDataList[input].power = Mathf.Clamp(weaponDataList[input].power, 1f, 1000f);
                    break;
                default:
                    break;
            }
        }
    }
}