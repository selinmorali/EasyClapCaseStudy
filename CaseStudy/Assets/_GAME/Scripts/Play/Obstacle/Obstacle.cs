using _GAME.Scripts.Play.Shoot;
using _GAME.Scripts.Pool;
using _GAME.Scripts.UI;
using UnityEngine;

namespace _GAME.Scripts.Play.Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        public ObjectPooler pool;
        public TotalCoinPanel coinPanel;
        public float income;
        private GameObject _createdCoinObject;

        
        private void OnTriggerEnter(Collider other)
        {
            CollideWeapon(other);
        }

        private void CollideWeapon(Collider other)
        {
            Weapon weapon = other.GetComponent<Weapon>();
            if (weapon == null)
            {
                return;
            }
            
            weapon.gameObject.SetActive(false);
            _createdCoinObject = pool.SpawnFromPool("coin", transform.position, Quaternion.identity);
            coinPanel.MoveCoinToUI(_createdCoinObject, transform.position, income);
        }
    }
}