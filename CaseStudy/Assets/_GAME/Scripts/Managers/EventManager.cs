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
        public static UnityEvent OnGameStart = new();
    }
}