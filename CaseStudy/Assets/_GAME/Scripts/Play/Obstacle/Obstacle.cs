using _GAME.Scripts.Play.Shoot;
using UnityEngine;

namespace _GAME.Scripts.Play.Obstacle
{
    public class Obstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Weapon weapon = other.GetComponentInParent<Weapon>();

            if (weapon == null)
            {
                return;
            }
            
            weapon.gameObject.SetActive(false);
        }
    }
}