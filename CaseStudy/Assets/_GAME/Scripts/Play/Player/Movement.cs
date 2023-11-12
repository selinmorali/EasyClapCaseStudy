using _GAME.Scripts.Managers;
using _GAME.Scripts.Managers.LevelSystem;
using DG.Tweening;
using UnityEngine;

namespace _GAME.Scripts.Play.Player
{
    public class Movement : MonoBehaviour
    {
        public float movementSpeed;

        private void OnEnable()
        {
            EventManager.OnFinalArea.AddListener(DecreaseMovementSpeed);
            EventManager.OnPlayerHitObstacle.AddListener(MoveBackward);
        }

        private void OnDisable()
        {
            EventManager.OnFinalArea.RemoveListener(DecreaseMovementSpeed);
            EventManager.OnPlayerHitObstacle.RemoveListener(MoveBackward);
        }

        private void FixedUpdate()
        {
            if (LevelManager.Instance.currentLevel.state != Level.State.Started)
            {
                return;
            }
            
            ForwardMovement();
        }

        private void ForwardMovement()
        {
            transform.Translate(Vector3.forward * (movementSpeed * Time.fixedDeltaTime));
        }
        
        private void MoveBackward()
        {
            transform.DOMoveZ(-0.1f, 1);
        }

        private void DecreaseMovementSpeed()
        {
            movementSpeed = 3f;
        }
    }
}