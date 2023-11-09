using System.Collections;
using _GAME.Scripts.Play.Shoot;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _GAME.Scripts.Play.Collect
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
        public TextMeshProUGUI CollectibleValueText;
        public TextMeshProUGUI CollectibleTypeText;
        public float value;
        
        private Vector3 _originalScale;

        private void Awake()
        {
            CollectibleValueText.text = value.ToString("+#;-#;0");
            CollectibleTypeText.text = gateType.ToString();
            _originalScale = transform.localScale;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            EnterWeapon(other);
        }
        
        private void EnterWeapon(Collider other)
        {
            var weapon = other.GetComponentInParent<Weapon>();
            if (weapon == null)
            {
                return;
            }
            
            UpdateCollectibleValue(weapon.weaponData.power);
            UpdateScaleTheDoor();
            StartCoroutine(CloseWeapon(weapon));
        }

        private void UpdateCollectibleValue(float input)
        {
            value += input;
            CollectibleValueText.text = value.ToString("+#;-#;0");
        }
        
        private void UpdateScaleTheDoor()
        {
            transform.DOKill();
            transform.localScale = _originalScale; 
            transform.DOPunchScale(Vector3.one * 0.15f, 1, 1);
        }

        private IEnumerator CloseWeapon(Weapon weapon)
        {
            weapon.effect.Play();
            yield return new WaitForSeconds(0.1f);
            weapon.transform.GetChild(0).gameObject.SetActive(false);
            weapon.gameObject.SetActive(false);
        }
    }
}