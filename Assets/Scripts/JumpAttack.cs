using System.Collections;
using Units;
using Units.Player;
using UnityEngine;

public class JumpAttack : BaseUnit, IAttack
{
    private readonly int ATTACK_ANIMATION_ID = Animator.StringToHash("Attack");
    
    private Animator animator;
    private Collider collider;
    private Rigidbody rigidbody;
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        collider = GetComponent<Collider>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Attack()
    {
        animator.SetTrigger(ATTACK_ANIMATION_ID);
    }

    public void Jump()
    {
        StartCoroutine(JumpCoroutine());
    }

    public void ManageColliders(bool enable)
    {
        rigidbody.isKinematic = !enable;
        collider.enabled = enable;
    }
    
    private IEnumerator JumpCoroutine()
    {
        Vector3 targetPosition = Player.Instance.PlayerTransform.position;
        Vector3 initialPosition = transform.position;

        
        float current = 0;
        while (current < 1)
        {
            current = Mathf.MoveTowards(current, 1, 1.5f * Time.deltaTime);
            transform.position = Vector3.Lerp(initialPosition, targetPosition, current);
            yield return null;
        }
    }
}
