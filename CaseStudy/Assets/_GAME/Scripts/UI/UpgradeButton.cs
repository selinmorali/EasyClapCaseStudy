using _GAME.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME.Scripts.UI
{
    public class UpgradeButton : MonoBehaviour
    {
        public int cost;
        public TextMeshProUGUI _buttonPriceText;
        public int costMultiplier;
        
        private int _buttonLevelIndex;
        private TotalCoinPanel _totalCoinPanel;
        private Button _button;
        private Vector3 _originalScale;

        private void Awake()
        {
            _totalCoinPanel = GetComponentInParent<TotalCoinPanel>();
            _originalScale = transform.localScale;
        }

        public void ClickButton()
        {
            IsButtonInteractable();
             
            if (IsPurchasable() == false)
            {
                return;
            }
        
            UpdateButtonScale();
            DecreaseMoney();
            UpdateCost();
            
            EventManager.OnWeaponUpgraded.Invoke();
            IsButtonInteractable();
        }

        private void UpdateCost()
        {
            _buttonLevelIndex += 1;
            cost = _buttonLevelIndex * costMultiplier;
            _buttonPriceText.text = "$" + cost;
        }
        
        private void IsButtonInteractable()
        {
            _button.interactable = IsPurchasable();
        }
        
        private bool IsPurchasable()
        {
            _buttonPriceText.text = "$" + cost;
            return _totalCoinPanel.totalCoinValue >= cost;
        }
        
        private void DecreaseMoney()
        {
            EventManager.OnTotalCoinUpdate.Invoke(-cost);
        }

        private void UpdateButtonScale()
        {
            transform.DOKill();
            transform.localScale = _originalScale; 
            transform.DOPunchScale(Vector3.one * 0.2f, 1, 1).SetEase(Ease.Linear);
        }
    }
}