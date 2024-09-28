using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float gravitationalForce = 10f; // Força de "atração" do buraco negro
    public float orbitSpeed = 50f; // Velocidade de órbita

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectiles"))
        {
            // Iniciar a órbita quando o projétil entra na área de efeito
            StartCoroutine(OrbitAround(other.gameObject));
        }
    }

    private IEnumerator OrbitAround(GameObject projectile)
    {
        yield return new WaitForSeconds(Random.Range(0.25f, 1.0f));

        float distance = Vector3.Distance(transform.position, projectile.transform.position);
        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>(); 
        rigidbody.useGravity = false;
        rigidbody.velocity = Vector3.zero;
        
        // Enquanto o projétil estiver ativo, ele orbita ao redor do buraco negro
        while (projectile != null && distance < 20)
        {
            // Direção do buraco negro para o projétil
            Vector3 direction = (transform.position - projectile.transform.position).normalized;

            // Atração em direção ao buraco negro
            //projectile.transform.position += direction * gravitationalForce * Time.deltaTime;

            // Rotacionar o projétil em torno do buraco negro
            projectile.transform.RotateAround(transform.position, Vector3.up, orbitSpeed * Time.deltaTime);

            yield return null; // Esperar o próximo frame
        }
    }
}
