using _GAME.Scripts.Managers;
using _GAME.Scripts.Play.Collect;
using UnityEngine;

namespace _GAME.Scripts.Play.Player
{
    public class PlayerInteractive : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EnterEndGameEntry(other);
            EnterGate(other);
        }

        private void EnterEndGameEntry(Collider other)
        {
            EndGameEntry endGameEntry = other.GetComponent<EndGameEntry>();

            if (endGameEntry == null)
            {
                return;
            }
            
            EventManager.OnFinalArea.Invoke();
        }

        private void EnterGate(Collider other)
        {
            Gate gate = other.GetComponent<Gate>();

            if (gate == null)
            {
                return;
            }
            
            EventManager.OnGetShotValue.Invoke(gate.gateType, gate.value);
            gate.gameObject.SetActive(false);
        }
    }
}