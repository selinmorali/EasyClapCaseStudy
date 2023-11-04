using _GAME.Scripts.Managers;
using _GAME.Scripts.Managers.LevelSystem;
using UnityEngine;

namespace _GAME.Scripts.Play.Shoot
{
    public class Weapon : MonoBehaviour
    {
        // public GameObject hitParticleForGate;
        // public GameObject hitParticleForObstacle;
        public int index;
        public int power;
        public float speed;
        public float lifeTime;
        
        private float _timer;
        
        
        public void Init(Vector3 position, Quaternion rotation)
        {
            transform.position = position;
            transform.rotation = rotation;
            gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (!GameManager.Instance.isFirstClick)
            {
                return;
            }
            
            WeaponForwardMovement();
        }

        private void WeaponForwardMovement()
        {
            _timer += Time.deltaTime;

            if (_timer >= lifeTime)
            {
                _timer = 0;
                gameObject.SetActive(false);
            }
            transform.Translate(Vector3.forward * (10f * (speed * Time.deltaTime)));
        }

        public void UpdateWeaponHitPowerValue(int powerValue)
        {
            power = powerValue;
        }

        public void UpdateLifeTime(float input)
        {
            lifeTime = input;
        }
    }
}