using _GAME.Scripts.Pool;
using _GAME.Scripts.UI;
using UnityEngine;

namespace _GAME.Scripts.Play.Chest
{
    public class Lid : MonoBehaviour
    {
        public ObjectPooler pool;
        public TotalCoinPanel totalCoinPanel;
        public ParticleSystem particleForChest;
        private Chest _chest;
        private Animator _animator;
        private GameObject _createdCoinObject;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _chest = GetComponentInParent<Chest>();
        }

        public void OpenLidAnim()
        {
            _animator.SetBool("isOpen", true);
        }

        private void PlayParticle()
        {
            particleForChest.Play();
        }

        public void CoinCollect() //animation event
        { 
            _createdCoinObject = pool.SpawnFromPool("coin", transform.position, Quaternion.identity);
            totalCoinPanel.MoveCoinToUI(_createdCoinObject, transform.position, _chest.income);
        }
    }
}