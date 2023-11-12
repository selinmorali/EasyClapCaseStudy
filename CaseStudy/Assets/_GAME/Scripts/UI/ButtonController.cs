using System.Collections.Generic;
using UnityEngine.UI;

namespace _GAME.Scripts.UI
{
    public class ButtonController : MonoSingleton<ButtonController>
    {
        public List<BaseButton> buttonsList;

        private void Start()
        {
            AreButtonsInteractable();
        }

        public void AreButtonsInteractable()
        {
            for (int i = 0; i < buttonsList.Count; i++)
            {
                if (buttonsList[i].cost > TotalCoinPanel.Instance.totalCoinValue)
                {
                    buttonsList[i].GetComponent<Button>().interactable = false;
                }
                else
                {
                    buttonsList[i].GetComponent<Button>().interactable = true;
                }
            }
        }
    }
}