using _GAME.Scripts.Managers;
using UnityEngine;

namespace _GAME.Scripts.Play.Player
{
    public class PlayerInteractive : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EnterEndGameEntry(other);
        }

        private void EnterEndGameEntry(Collider collider)
        {
            EndGameEntry endGameEntry = collider.GetComponent<EndGameEntry>();

            if (endGameEntry == null)
            {
                return;
            }
            
            EventManager.OnFinalArea.Invoke();
        }
    }
}