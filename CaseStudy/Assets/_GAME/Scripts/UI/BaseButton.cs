using _GAME.Scripts.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME.Scripts.UI
{
    public abstract class BaseButton : MonoBehaviour
    {
        public TextMeshProUGUI _buttonPriceText;
        public Button button;
        public Vector3 _originalScale;
        public int buttonLevelIndex;
        public int cost;
        public int costMultiplier;
        
        private void Start()
        {
            IsButtonInteractable();
        }
        
        public void IsButtonInteractable()
        {
            button.interactable = IsPurchasable();
        }
        
        public bool IsPurchasable()
        {
            if (TotalCoinPanel.Instance == null)
            {
                Debug.LogError("TotalCoinPanel is null.");
                return false;
            }

            _buttonPriceText.text = "$" + cost;
            return TotalCoinPanel.Instance.totalCoinValue >= cost;
        }
        
        public void DecreaseMoney()
        {
            EventManager.OnTotalCoinUpdate.Invoke(-cost);
        }

        public void UpdateButtonScale()
        {
            transform.DOKill();
            transform.localScale = _originalScale; 
            transform.DOPunchScale(Vector3.one * 0.2f, 1, 1).SetEase(Ease.Linear);
        }
    }
}