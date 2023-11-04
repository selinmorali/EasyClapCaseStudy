using System;
using UnityEngine;
using UnityEngine.Events;

namespace _GAME.Scripts.Managers
{
    public class FloatEvent : UnityEvent<float>{}
    public class BoolEvent : UnityEvent<bool>{}
    public class MoneyEvent : UnityEvent<Vector3, Action, double> { }
    
    public static class EventManager
    {
        public static UnityEvent OnFirstClick = new();
        public static UnityEvent OnLoadedFirstLevel = new();
        public static UnityEvent OnOpenButtons = new();
        public static UnityEvent OnFinalArea = new();
        public static UnityEvent OnNextButtonPressed = new();
    }
}