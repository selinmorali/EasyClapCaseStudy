using _GAME.Scripts.Controllers;
using _GAME.Scripts.Managers;
using _GAME.Scripts.Pool;
using UnityEngine;

namespace _GAME.Scripts.Play.Player
{
    public class Player : MonoBehaviour
    {
        public ObjectPooler objectPooler;
        public Transform firePoint;
        public GameObject pistol;
        public Transform barrelOfPistol;

        private GameObject createdObject;
        private float _timer;
        private float _cooldown;

        private void OnEnable()
        {
            EventManager.OnWeaponUpgraded.AddListener(UpgradeWeapon);
        }

        private void OnDisable()
        {
            EventManager.OnWeaponUpgraded.RemoveListener(UpgradeWeapon);
        }

        public void Shot()
        {
            switch (WeaponController.Instance.currentWeaponIndex)
            {
                case 0:
                    createdObject = objectPooler.SpawnFromPool("kunai", firePoint.transform.position, Quaternion.Euler(90,0,0));
                    createdObject.gameObject.SetActive(true);
                    break;
                case 1:
                    createdObject = objectPooler.SpawnFromPool("shuriken", firePoint.transform.position, Quaternion.Euler(0,0,90));
                    createdObject.gameObject.SetActive(true);
                    break;
                case 2:
                    createdObject = objectPooler.SpawnFromPool("bullet", firePoint.transform.position, Quaternion.Euler(90,0,0));
                    createdObject.gameObject.SetActive(true);
                    break;
                default:
                    createdObject = objectPooler.SpawnFromPool("bullet", firePoint.transform.position, Quaternion.identity);
                    createdObject.gameObject.SetActive(true);
                    break;
            }
        }
        
        private void UpgradeWeapon()
        {
            WeaponController.Instance.currentWeaponIndex += 1;

            if (WeaponController.Instance.currentWeaponIndex >= 2)
            {
                EquipPistol();
            }
        }
        
        private void EquipPistol()
        {
            pistol.SetActive(true);
            firePoint = barrelOfPistol;
        }
    }
}