using _GAME.Scripts.Play.Shoot;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _GAME.Scripts.Play.Gates
{
    public enum Type
    {
        Range,
        FireRate,
        Power,
    }
    
    public class Gate : MonoBehaviour
    {
        public Type gateType;
        public TextMeshProUGUI collectibleValueText;
        public TextMeshProUGUI collectibleTypeText;
        public float value;
        
        private Vector3 _originalScale;

        private void Awake()
        {
            collectibleValueText.text = value.ToString("+#;-#;0");
            collectibleTypeText.text = gateType.ToString();
            _originalScale = transform.localScale;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            EnterWeapon(other);
        }
        
        private void EnterWeapon(Collider other)
        {
            var weapon = other.GetComponent<Weapon>();
            if (weapon == null)
            {
                return;
            }
            
            UpdateCollectibleValue(weapon.weaponData.power);
            UpdateScaleTheDoor();
            weapon.gameObject.SetActive(false);
        }

        private void UpdateCollectibleValue(float input)
        {
            value += input;
            collectibleValueText.text = value.ToString("+#;-#;0");
        }
        
        private void UpdateScaleTheDoor()
        {
            transform.DOKill();
            transform.localScale = _originalScale; 
            transform.DOPunchScale(Vector3.one * 0.1f, 1, 1);
        }
    }
}