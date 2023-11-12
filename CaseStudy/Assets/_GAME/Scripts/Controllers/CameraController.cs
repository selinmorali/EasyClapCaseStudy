using _GAME.Scripts.Managers;
using UnityEngine;

namespace _GAME.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public GameObject runningCamera;
        public GameObject finalCamera;

        private void OnEnable()
        {
            EventManager.OnFinalArea.AddListener(OpenFinalCam);
        }

        private void OnDisable()
        {
            EventManager.OnFinalArea.RemoveListener(OpenFinalCam);
        }

        private void OpenFinalCam()
        {
            runningCamera.SetActive(false);
            finalCamera.SetActive(true);
        }
    }
}