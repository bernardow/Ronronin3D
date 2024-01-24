using System;
using UnityEngine;
using Photon.Pun;
using Systems.Upgrades;

namespace Units.Player
{
    public class Player : MonoBehaviourPunCallbacks
    {
        public PlayerAttack PlayerAttack { get; private set; }
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

        private UpgradeManager _upgradeManager;

        [SerializeField] private bool _inLobby;
        [SerializeField] private Funghy.Funghy _funghy;

        private void Awake()
        {
            PlayerAttack = GetComponent<PlayerAttack>();
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

            if (_inLobby) return;

            if (!photonView.IsMine)
            {
                DisablePlayer();
                return;
            }

            _upgradeManager = GameObject.FindWithTag("Upgrade").GetComponent<UpgradeManager>();
            _upgradeManager.Initialize(this);

            PlayerHealth.Healthbar = GameObject.FindWithTag("Healthbar");

            _funghy = GameObject.FindWithTag("Boss").GetComponent<Funghy.Funghy>();
            _funghy.FungiUltimate.LaserAttack.OnUltimateHit += TakeDamage;
            PlayerCollisions.OnCollision += TakeDamage;
        }

        private void OnDestroy()
        {
            if (_inLobby) return;
            _funghy.FungiUltimate.LaserAttack.OnUltimateHit -= TakeDamage;
            PlayerCollisions.OnCollision -= TakeDamage;        
        }

        private void TakeDamage(object sender, OnCollisionArgs args)
        {
            if(!PlayerAttack.IsSlashing && !PlayerDash.IsDashing)
                PlayerHealth.RemoveLife(args.Collider.Damage);
        }
        
        private void TakeDamage(object sender, OnUltimateArgs args)
        {
            if(!PlayerAttack.IsSlashing && !PlayerDash.IsDashing)
                PlayerHealth.RemoveLife(args.Damage);
        }

        private void DisablePlayer()
        {
            PlayerAttack.enabled = false;
            PlayerHealth.enabled = false;
            PlayerMovement.enabled = false;
            PlayerInputs.enabled = false;
            PlayerCollisions.enabled = false;
            PlayerDash.enabled = false;
            PlayerBasicAttack.enabled = false;
            PlayerKunaiAttack.enabled = false;
        }
    }
}
