using _GAME.Scripts.Managers;
using UnityEngine;

namespace _GAME.Scripts.Play.Player
{
    public class Movement : MonoBehaviour
    {
        public float movementSpeed;
        
        private void FixedUpdate()
        {
            if (!GameManager.Instance.isGameStarted)
            {
                return;
            }
            
            ForwardMovement();
        }

        private void ForwardMovement()
        {
            transform.Translate(Vector3.forward * (movementSpeed * Time.fixedDeltaTime));
        }
    }
}