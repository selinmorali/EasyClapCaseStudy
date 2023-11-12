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
            else if(LevelManager.Instance.currentLevel.state == Level.State.Loading || LevelManager.Instance.currentLevel.state == Level.State.Succeed || LevelManager.Instance.currentLevel.state == Level.State.Failed)
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
            _animator.SetBool("isIdle", true);
            _animator.SetBool("isShuriken", false);
            _animator.SetBool("isKunai", false);
        }
    
        private void ShurikenState()
        {
            state = States.ShurikenShoot;
            _animator.SetBool("isShuriken", true);
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isKunai", false);
        }
        
        private void KunaiState()
        {
            state = States.KunaiShoot;
            _animator.SetBool("isKunai", true);
            _animator.SetBool("isIdle", false);
            _animator.SetBool("isShuriken", false);
        }
        
        private void PistolState()
        {
            state = States.PistolShoot;
            _animator.SetBool("isIdle", true);
            _animator.SetBool("isKunai", false);
            _animator.SetBool("isShuriken", false);
            pistolAnimator.SetTrigger("isPistol");
        }

        public void CalculateAnimTime()
        {
            //her animasyonun süresini fire rate değerine göre kısalt
            _animationLength = weaponController.weaponDataList[0].fireRate / _fireRateCoef;
            _animator.SetFloat("kunaiSpeed", _animationLength);
            
            _animationLength = weaponController.weaponDataList[1].fireRate / _fireRateCoef;
            _animator.SetFloat("shurikenSpeed", _animationLength);

            if (pistolAnimator.gameObject.activeInHierarchy)
            {
                _animationLength = weaponController.weaponDataList[2].fireRate / _fireRateCoef;
                pistolAnimator.SetFloat("pistolSpeed", _animationLength);
            }
        }

        public void Shoot() //Call animation event
        {
            if (LevelManager.Instance.currentLevel.state == Level.State.Succeed)
            {
                IdleState();
                return;
            }
            _player.Shot();
        }
    }
}