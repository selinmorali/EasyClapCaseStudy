using _GAME.Scripts.Managers;
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
        public float value;
        public Type gateType;
        public TextMeshProUGUI valueText;
        public TextMeshProUGUI typeText; 
        public Material greenMaterial;
        public Material borderGreenMaterial;
        
        [SerializeField]private MeshRenderer _gateRenderer;
        [SerializeField]private MeshRenderer _borderRenderer; 

        private Color _color;
        private Vector3 _originalScale;

        
        private void Awake()
        {
            valueText.text = value.ToString("+#;-#;0");
            typeText.text = gateType.ToString();
            _color = _gateRenderer.material.color;
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
            UpdateScaleGate();
            UpdateFillMaterialColor();
            StartCoroutine(weapon.CloseWeaponAndPlayParticle(weapon));
        }

        private void UpdateCollectibleValue(float input)
        {
            value += input;
            valueText.text = value.ToString("+#;-#;0");
            
            if (value > 0 && _gateRenderer.name != "GreenGate")
            {
                ChangeGateMaterial();
            }
        }
        
        private void UpdateScaleGate()
        {
            transform.DOKill();
            transform.localScale = _originalScale; 
            transform.DOPunchScale(Vector3.one * 0.1f, 1, 1);
        }
        
        private void UpdateFillMaterialColor()
        {
            if (_gateRenderer == null)
            {
                return;
            }

            if (_color.a <= 1f)
            {
                _color.a += 0.1f;
                _color.a = Mathf.Clamp01(_color.a);
            }

            _gateRenderer.material.color = _color;
        }

        private void ChangeGateMaterial()
        {
            _borderRenderer.material = borderGreenMaterial; 
            _gateRenderer.material = greenMaterial;
        }
    }
}