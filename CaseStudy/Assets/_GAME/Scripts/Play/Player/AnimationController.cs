using _GAME.Scripts.Managers;
using _GAME.Scripts.Managers.LevelSystem;
using _GAME.Scripts.Play.Shoot;
using _GAME.Scripts.Pool;
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
        public PoolFactory poolFactory;
        public ShootController shootController;
        public Animator pistolAnimator;
        private Animator _animator;
        private float _animationLength;


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
                IdleAnimState();
            }
        }

        private void CheckAnimationState()
        {
            switch (poolFactory.settings.tier)
            {
                case 0:
                    KunaiAnimState();
                    break;
                case 1:
                    ShurikenAnimState();
                    break;
                case 2:
                    //PistolState();
                    break;
                default:
                    IdleAnimState();
                    break;
            }
            
        }

        private void IdleAnimState()
        {
            state = States.Idle;
            _animator.SetTrigger("isIdle");
        }
    
        private void ShurikenAnimState()
        {
            state = States.ShurikenShoot;
            _animator.SetTrigger("isShuriken");
            _animationLength = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }
        
        private void KunaiAnimState()
        {
            state = States.KunaiShoot;
            _animator.SetTrigger("isKunai");
            _animationLength = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }
        
        private void PistolAnimState()
        {
            state = States.PistolShoot;
            pistolAnimator.SetTrigger("isPistol");
        }

        public void Shoot(float animationLength)
        {
            _animationLength = animationLength;
            shootController.Shot(_animationLength);
        }
    }
}