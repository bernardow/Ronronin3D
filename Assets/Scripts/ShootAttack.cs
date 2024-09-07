using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAttack : MonoBehaviour, IAttack
{
    public GameObject Bomb;
    public float Force = 10;

    public void Attack()
    {
        ShootBomb();
    }

    private void ShootBomb()
    {
        Rigidbody bombRigidbody = Instantiate(Bomb, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        bombRigidbody.AddForce(new Vector3(1, 1, 0) * Force, ForceMode.Impulse);
    }
}
