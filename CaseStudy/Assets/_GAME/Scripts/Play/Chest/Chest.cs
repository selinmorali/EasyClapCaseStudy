using _GAME.Scripts.Managers;
using _GAME.Scripts.Managers.LevelSystem;
using _GAME.Scripts.Play.Shoot;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _GAME.Scripts.Play.Chest
{
    public class Chest : MonoBehaviour
    {
        public TextMeshProUGUI healthValueText;
        public Lid lid;
        public float chestHealthValue;
        public float income;
       
        private Vector3 _originalScale;
        private Animator _animator;

        private void Awake()
        {
             _originalScale = transform.localScale;
             _animator = GetComponent<Animator>();
             healthValueText.text = chestHealthValue.ToString();
        }

        private void OnTriggerEnter(Collider other)
        {
            EnterWeapon(other);
        }

        private void EnterWeapon(Collider other)
        {
            Weapon weapon = other.GetComponent<Weapon>();

            if (weapon == null)
            {
                return;
            }
            
            StartCoroutine(weapon.CloseWeaponAndPlayParticle(weapon));
            UpdateHealthValue(weapon.weaponData.power);
        }

        private void UpdateHealthValue( float powerValue)
        {
            if (chestHealthValue <= 0)
            {
                return;
            }
            
            chestHealthValue -= powerValue;
            healthValueText.text = chestHealthValue.ToString();
            CheckHealthValue();
        }

        private void CheckHealthValue()
        {
            if (chestHealthValue <= 0)
            {
                chestHealthValue = 0;
                healthValueText.text = chestHealthValue.ToString();
                ChestBounceAnim();
            }
        }

        private void ChestBounceAnim()
        {
            _animator.SetBool("isBounce", true);
        }
        
        private void OpenChestLid() //animation event
        {
            lid.ChestOpeningStatus();
        }

        public void CheckChestHealth()
        {
            if (chestHealthValue > 0)
            {
                EventManager.OnLevelSuccess.Invoke();
            }
        }
    }
}