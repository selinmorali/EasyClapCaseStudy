using UnityEngine;

namespace _GAME.Scripts.Play.Player
{
    public class PistolAnimation : MonoBehaviour
    {
        public void Shoot() //call animation event
        {
            AnimationController.Instance.Shoot();
        }
    }
}