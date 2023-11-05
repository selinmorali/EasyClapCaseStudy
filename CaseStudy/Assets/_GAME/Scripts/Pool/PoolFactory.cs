using System.Linq;
using _GAME.Scripts.Managers;
using _GAME.Scripts.Play.Shoot;
using UnityEngine;

namespace _GAME.Scripts.Pool
{
    public class PoolFactory : MonoBehaviour
    {
        public PoolSettings settings = new();

        private void OnEnable()
        {
            EventManager.OnClickUpgradeWeaponButton.AddListener(CreateNewPool);
        }

        private void OnDisable()
        {
            EventManager.OnClickUpgradeWeaponButton.RemoveListener(CreateNewPool);
        }

        private void Start()
        {
            SetPool();
        }

        private void SetPool()
        {
            for (int i = 0; i < settings.count; i++)
            {
                var item = Instantiate(settings.weaponPrefab, transform);
                item.Init(transform.position, Quaternion.identity);
                settings.pools.Add(item);
            }
        }
        
        private void CreateNewPool()
        {
            settings.pools.Clear();
            settings.weaponPrefab = settings.prefabs[GetComponent<Weapon>().index];
            SetPool();
            AllUpdateWeapon(ShootController.Instance.power, ShootController.Instance.range);
        }

        public void AllUpdateWeapon(float power, float range)
        {
            foreach (var item in settings.pools)
            {
                item.UpdateWeaponHitPowerValue((int)power);
                item.UpdateLifeTime(range);
            }
        }
        
        public Weapon CallPoolItem()
        {
            var item = GetPoolItem();
            if (item != null)
            {
                return item;
            }

            SetPool(settings.increaseCount);
            return GetPoolItem();
        }

        private Weapon GetPoolItem()
        {
            Weapon firstItem = settings.pools.FirstOrDefault(item => item.gameObject.activeInHierarchy == false);
            settings.pools.Remove(firstItem);
            settings.pools.Add(firstItem);
            return firstItem;
        }

        private void SetPool(int value)
        {
            for (int i = 0; i < value; i++)
            {
                var item = Instantiate(settings.weaponPrefab, transform);
                item.Init(transform.position, Quaternion.identity);
                settings.pools.Add(item);
            }
        }
    }
}