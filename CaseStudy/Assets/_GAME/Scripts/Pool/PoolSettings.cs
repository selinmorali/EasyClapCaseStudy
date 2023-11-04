using System;
using System.Collections.Generic;
using _GAME.Scripts.Play.Shoot;
using UnityEngine;

namespace _GAME.Scripts.Pool
{
    [Serializable]
    public class PoolSettings
    {
        [SerializeField] public Weapon currencyPrefab;
        public List<Weapon> prefabs;
        public List<Weapon> pools = new();
        public int count;
        public int increaseCount;
    }
}