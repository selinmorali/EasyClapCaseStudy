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
    
    public class AnimationController : MonoSingleton<AnimationController>
    {
        public States state;
        public WeaponController weaponController;
        public Animator pistolAnimator;
        public float _fireRateCoef;
        
        private Animator _animator;
        private Player _player;
        private float _animationLength;
        

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _player = GetComponentInParent<Player>();
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
            _animator.SetTrigger("isIdle");
            pistolAnimator.SetTrigger("isPistol");
        }

        public void CalculateAnimTime()
        {
            //her animasyonun süresini fire rate değerine göre kısalt
            _animationLength = weaponController.weaponDataList[0].fireRate / _fireRateCoef;
            _animator.SetFloat("kunaiSpeed", _animationLength);
            
            _animationLength = weaponController.weaponDataList[1].fireRate / _fireRateCoef;
            _animator.SetFloat("shurikenSpeed", _animationLength);
            
            _animationLength = weaponController.weaponDataList[2].fireRate / _fireRateCoef;
            pistolAnimator.SetFloat("pistolSpeed", _animationLength);
        }

        public void Shoot() //Call animation event
        {
            _player.Shot();
        }
    }
}