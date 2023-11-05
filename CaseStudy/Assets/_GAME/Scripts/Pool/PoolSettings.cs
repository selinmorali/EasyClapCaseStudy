using System;
using System.Collections.Generic;
using _GAME.Scripts.Play.Shoot;
using UnityEngine;
using UnityEngine.Serialization;

namespace _GAME.Scripts.Pool
{
    [Serializable]
    public class PoolSettings
    {
        public Weapon weaponPrefab;
        public List<Weapon> prefabs;
        public List<Weapon> pools = new();
        public int count;
        public int increaseCount;
    }
}