using _GAME.Scripts.Controllers;
using _GAME.Scripts.Managers.LevelSystem;
using UnityEngine;

namespace _GAME.Scripts.Play.Player
{
    public enum States
    {
        Idle,
        ShurikenShoot,
        KunaiShoot,
        PistolShoot
    }
    
    public class AnimationController : MonoBehaviour
    {
        public States state;
        public WeaponController weaponController;
        public Animator pistolAnimator;
        private Animator _animator;

        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (LevelManager.Instance.currentLevel.state == Level.State.Started)
            {
                CheckAnimationState();
            }
            else
            {
                IdleState();
            }
        }

        private void CheckAnimationState()
        {
            switch (weaponController.currentWeaponIndex)
            {
                case 0:
                    KunaiState();
                    break;
                case 1:
                    ShurikenState();
                    break;
                case 2:
                    PistolState();
                    break;
                default:
                    IdleState();
                    break;
            }
        }

        private void IdleState()
        {
            state = States.Idle;
            _animator.SetTrigger("isIdle");
        }
    
        private void ShurikenState()
        {
            state = States.ShurikenShoot;
            _animator.SetTrigger("isShuriken");
        }
        
        private void KunaiState()
        {
            state = States.KunaiShoot;
            _animator.SetTrigger("isKunai");
        }
        
        private void PistolState()
        {
            state = States.PistolShoot;
            pistolAnimator.SetTrigger("isPistol");
        }
    }
}