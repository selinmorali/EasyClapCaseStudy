using UnityEngine;
using UnityEngine.Events;
using Type = _GAME.Scripts.Play.Gates.Type;

namespace _GAME.Scripts.Managers
{
    public class FloatEvent : UnityEvent<float>{}
    public class BoolEvent : UnityEvent<bool>{}
    public class CoinEvent: UnityEvent<GameObject, Vector3, float>{}
    public class MoneyEvent : UnityEvent<Vector3, float> { }
    public class ShotEvent : UnityEvent<Type,float>{}

    public static class EventManager
    {
        public static UnityEvent OnFirstClick = new();
        public static UnityEvent OnLoadedFirstLevel = new();
        public static UnityEvent OnOpenButtons = new();
        public static UnityEvent OnFinalArea = new();
        public static UnityEvent OnNextButtonPressed = new();
        public static UnityEvent OnWeaponUpgradeButtonPressed = new();
        public static UnityEvent OnPlayerHitObstacle = new();
        public static UnityEvent OnCheckChestHealthValue= new();
        public static UnityEvent OnLevelSuccess = new();
        public static ShotEvent OnGetShotValue = new ();
    }
}