using _GAME.Scripts.Play.Shoot;
using _GAME.Scripts.Pool;
using _GAME.Scripts.UI;
using DG.Tweening;
using UnityEngine;

namespace _GAME.Scripts.Play.Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        public ObjectPooler pool;
        public TotalCoinPanel coinPanel;
        public float income;
        public float scaleCoef;
        
        private GameObject _createdCoinObject;
        private Vector3 _originalScale;


        private void Awake()
        {
            _originalScale = transform.localScale;
        }

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
            
            StartCoroutine(weapon.CloseWeaponAndPlayParticle(weapon));
            _createdCoinObject = pool.SpawnFromPool("coin", transform.position, Quaternion.identity);
            coinPanel.MoveCoinToUI(_createdCoinObject, transform.position, income);
            UpdateScaleObstacle();
        }

        private void UpdateScaleObstacle()
        {
            transform.DOKill();
            transform.localScale = _originalScale; 
            transform.DOPunchScale(Vector3.one * scaleCoef, 1, 1);
        }
    }
}