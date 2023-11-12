using _GAME.Scripts.Managers;
using _GAME.Scripts.Play.Gates;
using UnityEngine;

namespace _GAME.Scripts.Play.Player
{
    public class PlayerInteractive : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            EnterEndGameEntry(other);
            EnterGate(other);
            EnterObstacle(other);
            EnterChest(other);
            EnterFinish(other);
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

        private void EnterObstacle(Collider other)
        {
            Obstacle.Obstacle obstacle = other.GetComponent<Obstacle.Obstacle>();
            if (obstacle == null)
            {
                return;
            }
            
            EventManager.OnPlayerHitObstacle.Invoke();
        }

        private void EnterChest(Collider other)
        {
            Chest.Chest chest = other.GetComponent<Chest.Chest>();

            if (chest == null)
            {
                return;
            }
            
            chest.CheckChestHealth();
        }

        private void EnterFinish(Collider other)
        {
            Finish finish = other.GetComponent<Finish>();
            if (finish == null)
            {
                return;
            }
            
            finish.gameObject.SetActive(false);
            EventManager.OnLevelSuccess.Invoke();
        }
    }
}