using _GAME.Scripts.Managers;
using UnityEngine;

namespace _GAME.Scripts.Play.Player
{
    public class Movement : MonoBehaviour
    {
        public float movementSpeed;

        private void OnEnable()
        {
            EventManager.OnFinalArea.AddListener(DecreaseMovementSpeed);
        }

        private void OnDisable()
        {
            EventManager.OnFinalArea.RemoveListener(DecreaseMovementSpeed);
        }

        private void FixedUpdate()
        {
            if (!GameManager.Instance.isFirstClick)
            {
                return;
            }
            
            ForwardMovement();
        }

        private void ForwardMovement()
        {
            transform.Translate(Vector3.forward * (movementSpeed * Time.fixedDeltaTime));
        }

        private void DecreaseMovementSpeed()
        {
            movementSpeed = 3f;
        }
    }
}