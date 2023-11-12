using _GAME.Scripts.Managers;
using _GAME.Scripts.Managers.LevelSystem;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _GAME.Scripts.UI
{
    public class UpgradeButton : BaseButton
    {
        private void Awake()
        {
            _originalScale = transform.localScale;
            button = GetComponent<Button>();
            _buttonPriceText.text = "$" + cost;
            LevelManager.Instance.GetUpgradeButton();
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
            ButtonController.Instance.AreButtonsInteractable();
        }

        private void UpdateCost()
        {
            buttonLevelIndex += 1;
            cost += cost * (buttonLevelIndex * costMultiplier);
            _buttonPriceText.text = "$" + cost;
            LevelManager.Instance.SetUpgradeCost(cost, buttonLevelIndex);
        }
    }
}