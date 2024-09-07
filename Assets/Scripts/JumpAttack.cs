using System.Collections;
using System.Collections.Generic;
using Units.Player;
using UnityEngine;

public class JumpAttack : MonoBehaviour, IAttack
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        Vector3 targetPosition = Player.Instance.PlayerTransform.position;
        
    }
}
