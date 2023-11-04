using _GAME.Scripts.Managers;
using UnityEngine;

namespace _GAME.Scripts.Controllers
{
    public class CameraController : MonoBehaviour
    {
        public GameObject RunningCamera;
        public GameObject FinalCamera;

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
            FinalCamera.SetActive(true);
            RunningCamera.SetActive(false);
        }
    }
}