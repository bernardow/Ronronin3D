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

        private void Start()
        {
            PlayerAttack = GetComponent<PlayerAttack>();
            PlayerHealth = GetComponent<BaseUnit>();
            PlayerMovement = GetComponent<PlayerMovement>();
            PlayerTransform = transform;
            PlayerRigidbody = GetComponent<Rigidbody>();
        }
    }
}
