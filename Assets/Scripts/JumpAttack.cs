using System.Collections;
using Units;
using Units.Player;
using UnityEngine;
using Utilities;

public class JumpAttack : MonoBehaviour, IAttack, IObserver
{
    private readonly int ATTACK_ANIMATION_ID = Animator.StringToHash("Attack");
    private readonly int BOSS_ATTACK_ANIMATION_ID = Animator.StringToHash("JumpAttack");
    
    
    private Animator animator;
    private Collider collider;
    private Rigidbody rigidbody;

    public bool FinishedAttack { get; private set; }
    
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        collider = GetComponentInChildren<Collider>();
        rigidbody = transform.GetComponentInChildren<Rigidbody>();
    }

    public void Attack()
    {
        FinishedAttack = false;
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

        FinishedAttack = true;
    }

    public void OnNotify()
    {
        StartCoroutine(Run());
    }

    public IEnumerator Run()
    {
        FinishedAttack = false;
        animator.SetTrigger(ATTACK_ANIMATION_ID);

        while (!FinishedAttack)
        {
            yield return null;
        }
    }

    public void Disable()
    {
        StopAllCoroutines();
        enabled = false;
    }

    public void Enable()
    {
        enabled = true;
    }
}
