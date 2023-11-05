using System.Collections.Generic;
using _GAME.Scripts.Play.Shoot;
using UnityEngine;

namespace _GAME.Scripts.Controllers
{
    public class WeaponController : MonoBehaviour
    {
        public List<Weapon> weaponsList = new();
        public int currentWeaponIndex;
        
    }
}