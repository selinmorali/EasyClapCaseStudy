using UnityEngine.Events;
using Type = _GAME.Scripts.Play.Gates.Type;

namespace _GAME.Scripts.Managers
{
    public class IntEvent : UnityEvent<int>{}
    public class FloatEvent : UnityEvent<float>{}
    public class ShotEvent : UnityEvent<Type,float>{}

    public static class EventManager
    {
        public static UnityEvent OnFirstClick = new();
        public static UnityEvent OnOpenButtons = new();
        public static UnityEvent OnFinalArea = new();
        public static UnityEvent OnNextButtonPressed = new();
        public static UnityEvent OnPlayerHitObstacle = new();
        public static UnityEvent OnLevelSuccess = new();
        public static ShotEvent OnGetShotValue = new ();
        public static IntEvent OnTotalCoinUpdate = new();
        public static UnityEvent OnWeaponUpgraded = new();
        public static FloatEvent OnUpdateFireRate = new();
        public static FloatEvent OnUpdateRange = new();
    }
}