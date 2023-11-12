using _GAME.Scripts.Managers.LevelSystem;
using UnityEngine;

namespace _GAME.Scripts.Play.Player
{
    public class Swerve : MonoBehaviour
    {
        public float minClampX;
        public float maxClampX;
        private Vector3 _clampPos;

        private void Awake()
        {
            _clampPos = transform.position;
        }

        private void Update()
        {
            if (LevelManager.Instance.currentLevel.state != Level.State.Started)
            {
                return;
            }
            
            SwerveXAxis();
        }

        private void SwerveXAxis()
        {
            _clampPos.x = Mathf.Clamp(transform.position.x, minClampX, maxClampX);
            transform.position = new Vector3(_clampPos.x, 0, transform.position.z);
        }
    }
}