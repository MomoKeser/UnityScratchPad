using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject explosionParticles;
    private bool timerStarted = false;
    private float timer = 4f;
    public float explosionForce;
    public float explosionRadius;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            //TODO: Play explosion effect
            explosionParticles.SetActive(true);
            Destroy(other.gameObject);
            timerStarted = true;

            Collider [] collidersInExplosionRadius = Physics.OverlapSphere(
                gameObject.transform.position, 
                explosionRadius);

            for(int i = 0; i < collidersInExplosionRadius.Length; i++)
            {
                Rigidbody rb = collidersInExplosionRadius[i].gameObject.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.AddExplosionForce(explosionForce, gameObject.transform.position, explosionRadius);
                }
            }
        }
    }

    void Update()
    {
        if(timerStarted)
        {
            timer -= Time.deltaTime;
        }

        if(timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
