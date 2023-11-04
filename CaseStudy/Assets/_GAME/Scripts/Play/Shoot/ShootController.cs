using _GAME.Scripts.Managers;
using _GAME.Scripts.Pool;
using UnityEngine;
using Type = _GAME.Scripts.Play.Collect.Type;

namespace _GAME.Scripts.Play.Shoot
{
    public class ShootController : MonoSingleton<ShootController>
    {
        public Transform barrel;
        public float range;
        public float fireRate;
        public float power;
        public float fireCooldownCoef;
        
        private PoolFactory _factory;
        private float _timer;
        private float _cooldown;
        
        public override void Awake()
        {
            base.Awake();
            _factory = GetComponent<PoolFactory>();
            CheckValues();
        }

        private void OnEnable()
        {
            EventManager.OnGetShotValue.AddListener(SetShotValue);
        }

        private void OnDisable()
        {
            EventManager.OnGetShotValue.RemoveListener(SetShotValue);
        }

        private void SetShotValue(Type type, float value)
        {
            switch (type)
            {
                case Type.Range:
                    SetRange(value);
                    break;
                case Type.Power:
                    SetPower(value);
                    break;
                case Type.FireRate:
                    SetFireRate(value);
                    break;
                default:
                    Debug.LogError("Yolunda Gitmeyen şeyler var");
                    break;
            }
        }
        
        private void SetFireRate(float input)
        {
            fireRate += input / 5f;
            CheckValues();
        }

        private void SetRange(float input)
        {
            range += input;
            CheckValues();
            _factory.AllUpdateWeapon(power, range);
        }

        private void SetPower(float input)
        {
            power += input;
            CheckValues();
            _factory.AllUpdateWeapon(power, range);
        }
        
        private void Update()
        {
            // if (LevelManager.Instance.currentLevel.stage != Level.Stage.Runner
            //     || LevelManager.Instance.currentLevel.IsFinished)
            // {
            //     return;
            // }
            
            if (!GameManager.Instance.isFirstClick)
            {
                return;
            }

            Shot();
        }

        private void Shot()
        {
            _cooldown = fireCooldownCoef / fireRate;
            _timer += Time.deltaTime;
            if (_timer >= _cooldown)
            {
                _timer = 0;
                GetWeapon();
            }
        }

        private void GetWeapon()
        {
            var createdObject = _factory.CallPoolItem();
            createdObject.transform.position = barrel.position;
            createdObject.gameObject.SetActive(true);
        }
        
        private void CheckValues()
        {
            fireRate = Mathf.Clamp(fireRate, 1f, 100f);
            range = Mathf.Clamp(range, 1f, 100f);
            power = Mathf.Clamp(power, 1f, 1000f);
        }
    }
}