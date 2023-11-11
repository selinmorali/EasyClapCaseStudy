using _GAME.Scripts.Managers;
using _GAME.Scripts.Play.Collect;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _GAME.Scripts.UI
{
    public class TotalMoneyPanel : MonoBehaviour
    {
        public Transform coinTargetUI;
        public float totalCoinValue;
        private Canvas _canvas;
        private TextMeshProUGUI _coinText;
        private Tween _scaleTween;
        private float _lastSpawnTime;
        private const float movementDuration = 0.5f;
        private const float shakeDuration = 0.2f;
        private const float shakeStregnth = 0.65f;
        private string _shakeTweenId;

        private void Awake()
        {
            _coinText = GetComponentInChildren<TextMeshProUGUI>();
            _coinText.text = totalCoinValue.ToString();
            _canvas = GetComponentInParent<Canvas>();
        }

        private void OnEnable()
        {
            EventManager.OnCoinCollected.AddListener(MoveCoinToUI);
        }

        private void OnDisable()
        {
            EventManager.OnCoinCollected.RemoveListener(MoveCoinToUI);
        }
     
        private void MoveCoinToUI(GameObject coin, Vector3 coinPos)
        {
            if (coin == null)
            {
                return;
            }
            
            Vector3 moneyUIPosition = WorldToUISpace(_canvas, coinPos);
            coin.transform.position = moneyUIPosition;
            coin.transform.DOMove(coinTargetUI.position, movementDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    totalCoinValue += Mathf.Round(coin.GetComponent<Coin>().coinValue);
                    ShakeCoinImage();
                })
                .OnKill(() => { coin.SetActive(false); });;
        }
        
        private static Vector3 WorldToUISpace(Canvas canvas, Vector3 worldPosition)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);
    
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos,
                canvas.worldCamera, out Vector2 localPoint);
    
            return canvas.transform.TransformPoint(localPoint);
        }

        private void ShakeCoinImage()
        {
            _coinText.text = ("$" + totalCoinValue);
            
            if (_scaleTween != null)
                _scaleTween.Kill(true);
            
            _scaleTween = coinTargetUI.DOPunchScale(Vector3.one * shakeStregnth, shakeDuration).SetEase(Ease.Linear);
        }
        
        private void UpdateMoneyValue(float value)
        {
            if (_scaleTween != null)
                _scaleTween.Kill(true);

            _scaleTween = coinTargetUI.transform.DOPunchScale(Vector3.one * 0.5f, 0.5f, 1).SetEase(Ease.Linear);

            totalCoinValue += value;
            totalCoinValue = Mathf.Round(totalCoinValue);

            UpdateUI();
        }

        private void UpdateUI()
        {
            _coinText.text = ("$" + totalCoinValue);
        }
    }
}