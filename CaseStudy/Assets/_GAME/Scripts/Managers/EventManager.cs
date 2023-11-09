using System;
using UnityEngine;
using UnityEngine.Events;
using Type = _GAME.Scripts.Play.Collect.Type;

namespace _GAME.Scripts.Managers
{
    public class FloatEvent : UnityEvent<float>{}
    public class BoolEvent : UnityEvent<bool>{}
    public class MoneyEvent : UnityEvent<Vector3, Action, double> { }
    public class ShotEvent : UnityEvent<Type,float>{}
    
    public static class EventManager
    {
        public static UnityEvent OnFirstClick = new();
        public static UnityEvent OnLoadedFirstLevel = new();
        public static UnityEvent OnOpenButtons = new();
        public static UnityEvent OnFinalArea = new();
        public static UnityEvent OnNextButtonPressed = new();
        public static UnityEvent OnWeaponUpgradeButtonPressed = new();
        //TODO: fiRERATE EVENT firerate arttıkça her bir animasyon için kontrol
        public static UnityEvent OnClickUpgradeWeaponButton = new();
        public static ShotEvent OnGetShotValue = new ();
        public static FloatEvent OnTotalMoneyUpdate = new();
    }
}