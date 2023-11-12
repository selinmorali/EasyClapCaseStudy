using _GAME.Scripts.Managers.LevelSystem;
using _GAME.Scripts.SO;
using UnityEngine;

namespace _GAME.Scripts.Play.Shoot
{
    public class Weapon : MonoBehaviour
    {
        public WeaponData weaponData;
        private float _timer;

        private void Update()
        {
            if (LevelManager.Instance.currentLevel.state != Level.State.Started)
            {
                return;
            }
            
            WeaponForwardMovement();
        }

        private void WeaponForwardMovement()
        {
            _timer += Time.deltaTime;

            if (_timer >= weaponData.lifeTime)
            {
                _timer = 0;
                gameObject.SetActive(false);
            }
            transform.Translate(Vector3.forward * (10f * Time.deltaTime), Space.World);
        }
    }
}