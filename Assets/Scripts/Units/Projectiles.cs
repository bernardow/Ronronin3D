using UnityEngine;

namespace Units
{
    public class Projectiles : MonoBehaviour
    {
        private BaseUnit _baseUnit;

        private void Start() => _baseUnit = GetComponent<BaseUnit>();

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Setup"))
                _baseUnit.RemoveLife(1);
        }
    }
}
