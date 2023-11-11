using UnityEngine;

namespace Units.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerAttack PlayerAttack { get; private set; }
        public BaseUnit PlayerHealth { get; private set; }
        public PlayerMovement PlayerMovement { get; private set; }
        
        public Transform PlayerTransform { get; private set; }
        
        public Rigidbody PlayerRigidbody { get; private set; }

        public PlayerInputs PlayerInputs { get; private set; }
        public CollisionsChecker PlayerCollisions { get; private set; }
        public PlayerDash PlayerDash { get; private set; }
        public Collider PlayerCollider { get; private set; }
        public PlayerBasicAttack PlayerBasicAttack { get; private set; }

        [SerializeField] private bool _inLobby;
        [SerializeField] private Funghy.Funghy _funghy;

        private void Awake()
        {
            PlayerAttack = GetComponent<PlayerAttack>();
            PlayerHealth = GetComponent<BaseUnit>();
            PlayerMovement = GetComponent<PlayerMovement>();
            PlayerTransform = transform;
            PlayerRigidbody = GetComponent<Rigidbody>();
            PlayerInputs = GetComponent<PlayerInputs>();
            PlayerCollisions = GetComponent<CollisionsChecker>();
            PlayerDash = GetComponent<PlayerDash>();
            PlayerCollider = GetComponent<Collider>();
            PlayerBasicAttack = GetComponent<PlayerBasicAttack>();


            if (_inLobby) return;
            
            _funghy.FungiUltimate.LaserAttack.OnUltimateHit += TakeDamage;
            _funghy.FungiUltimate.LaserAttack.OnUltimateHit += TakeDamage;
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
    }
}
