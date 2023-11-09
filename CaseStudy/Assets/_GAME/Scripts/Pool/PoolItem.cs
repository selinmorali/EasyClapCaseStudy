using System;
using System.Collections.Generic;
using _GAME.Scripts.Play.Shoot;

namespace _GAME.Scripts.Pool
{
    [Serializable]
    public class PoolItem
    {
        public Weapon weaponPrefab;
        public List<Weapon> prefabs;
        public List<Weapon> pools = new();
        public int count;
        public int increaseCount;
        public int tier;
    }
}