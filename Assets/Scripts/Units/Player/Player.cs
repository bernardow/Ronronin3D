using System;
using UnityEngine;
using Systems.Upgrades;

namespace Units.Player
{
    public class Player : MonoBehaviour
    {
        public SpecialUnit PlayerHealth { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }
        
        public Transform PlayerTransform { get; private set; }
        
        public Rigidbody PlayerRigidbody { get; private set; }

        public PlayerInputs PlayerInputs { get; private set; }
        public CollisionsChecker PlayerCollisions { get; private set; }
        public PlayerDash PlayerDash { get; private set; }
        public Collider PlayerCollider { get; private set; }
        public PlayerBasicAttack PlayerBasicAttack { get; private set; }
        public KunaiAttack PlayerKunaiAttack { get; private set; }
        public PlayerStamina PlayerStamina { get; private set; }
        public BlackHoleAttack BlackHoleAttack { get; private set; }
        public SunAttack SunAttack { get; private set; }
        
        private UpgradeManager _upgradeManager;

        public static Player Instance;

        [SerializeField] private bool _inLobby;
        [SerializeField] private Funghy.Funghy _funghy;
        private Camera mainCamera;

        public Vector3 PlayerLastFramePosition;
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            mainCamera = Camera.main;
            PlayerHealth = GetComponent<SpecialUnit>();
            PlayerMovement = GetComponent<PlayerMovement>();
            PlayerTransform = transform;
            PlayerRigidbody = GetComponent<Rigidbody>();
            PlayerInputs = GetComponent<PlayerInputs>();
            PlayerCollisions = GetComponent<CollisionsChecker>();
            PlayerDash = GetComponent<PlayerDash>();
            PlayerCollider = GetComponent<Collider>();
            PlayerBasicAttack = GetComponent<PlayerBasicAttack>();
            PlayerKunaiAttack = GetComponent<KunaiAttack>();
            SunAttack = GetComponent<SunAttack>();
            BlackHoleAttack = GetComponent<BlackHoleAttack>();
            PlayerStamina = GetComponent<PlayerStamina>();

            if (_inLobby) return;

            _upgradeManager = GameObject.FindWithTag("Upgrade").GetComponent<UpgradeManager>();
            _upgradeManager.Initialize(this);
            
            GameObject boss = FindObjectOfType<Funghy.Funghy>(true).gameObject;//GameObject.FindWithTag("Boss");
            if (boss != null)
            {
                _funghy = boss.GetComponent<Funghy.Funghy>();
                _funghy.FungiUltimate.LaserAttack.OnUltimateHit += TakeDamage;
            }
                
            PlayerCollisions.OnCollision += TakeDamage;
        }

        private void LateUpdate()
        {
            PlayerLastFramePosition = transform.position;
        }

        private void OnDestroy()
        {
            if (_inLobby) return;
            if(_funghy != null)
                _funghy.FungiUltimate.LaserAttack.OnUltimateHit -= TakeDamage;
            PlayerCollisions.OnCollision -= TakeDamage;        
        }

        private void TakeDamage(object sender, OnCollisionArgs args)
        {
            if(!PlayerDash.IsDashing)
                PlayerHealth.RemoveLife(args.Collider.Damage);
        }
        
        private void TakeDamage(object sender, OnUltimateArgs args)
        {
            if(!PlayerDash.IsDashing)
                PlayerHealth.RemoveLife(args.Damage);
        }

        private void DisablePlayer()
        {
            PlayerHealth.enabled = false;
            PlayerMovement.enabled = false;
            PlayerInputs.enabled = false;
            PlayerCollisions.enabled = false;
            PlayerDash.enabled = false;
            PlayerBasicAttack.enabled = false;
            PlayerKunaiAttack.enabled = false;
        }
        
        public Vector3 ShootRaycast()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                return new Vector3(hitInfo.point.x, 0, hitInfo.point.z);
            }

            throw new NullReferenceException("Shooting out of the screen");
        }
    }
}
