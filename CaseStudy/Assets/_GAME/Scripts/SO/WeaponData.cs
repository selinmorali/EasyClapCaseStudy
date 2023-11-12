using UnityEngine;

namespace _GAME.Scripts.SO
{
    [CreateAssetMenu]
    public class WeaponData : ScriptableObject
    {
        public int index;
        public float range;
        public float fireRate;
        public float power;
        public float lifeTime => CalculateLifeTime();
        public float lifeTimeCoef;
        
        
        public float CalculateLifeTime()
        {
            return range / lifeTimeCoef;
        }
    }
}