using _GAME.Scripts.Managers;
using _GAME.Scripts.Managers.LevelSystem;
using UnityEngine.UI;

namespace _GAME.Scripts.UI
{
    public class RangeButton : BaseButton
    {
        private void Awake()
        {
            _originalScale = transform.localScale;
            button = GetComponent<Button>();
            _buttonPriceText.text = "$" + cost;
            LevelManager.Instance.GetRangeCost();
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
            EventManager.OnUpdateFireRate.Invoke(cost);
            ButtonController.Instance.AreButtonsInteractable();
        }

        private void UpdateCost()
        {
            buttonLevelIndex += 1;
            cost += cost * (buttonLevelIndex * costMultiplier);
            _buttonPriceText.text = "$" + cost;
            LevelManager.Instance.SetRangeCost(cost, buttonLevelIndex);
            IsButtonInteractable();
        }
    }
}