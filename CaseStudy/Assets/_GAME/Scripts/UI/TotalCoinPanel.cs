using _GAME.Scripts.Managers;
using _GAME.Scripts.Managers.LevelSystem;
using _GAME.Scripts.Play.Collect;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _GAME.Scripts.UI
{
    public class TotalCoinPanel : MonoBehaviour
    {
        public Transform coinTargetUI;
        public int totalCoinValue;
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
            _canvas = GetComponentInParent<Canvas>();
            totalCoinValue = LevelManager.Instance.GetTotalCoinValue();
            _coinText.text = totalCoinValue.ToString();
        }

        private void OnEnable()
        {
            EventManager.OnTotalCoinUpdate.AddListener(UpdateTotalCoinValue);
        }

        private void OnDisable()
        {
            EventManager.OnTotalCoinUpdate.RemoveListener(UpdateTotalCoinValue);
        }

        public void MoveCoinToUI(GameObject coin, Vector3 coinPos, int income)
        {
            if (coin == null)
            {
                return;
            }
            
            Vector3 coinUIPosition = WorldToUISpace(_canvas, coinPos);
            coin.transform.position = coinUIPosition;
            coin.transform.DOMove(coinTargetUI.position, movementDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    totalCoinValue += coin.GetComponent<Coin>().coinValue * income;
                    LevelManager.Instance.AddToTotalCoin(totalCoinValue);
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
        
        private void UpdateTotalCoinValue(int value)
        {
            if (_scaleTween != null)
                _scaleTween.Kill(true);

            _scaleTween = coinTargetUI.transform.DOPunchScale(Vector3.one * 0.5f, 0.5f, 1).SetEase(Ease.Linear);

            totalCoinValue += value;
            LevelManager.Instance.AddToTotalCoin(totalCoinValue);
            UpdateUI();
        }

        private void UpdateUI()
        {
            _coinText.text = ("$" + totalCoinValue);
        }
    }
}