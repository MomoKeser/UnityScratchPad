using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOverTimeComponent : MonoBehaviour
{
    public float damage;
    public float interval;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= interval)
        { 
            timer = 0f;

            HealthComponent healthComponent = GetComponent<HealthComponent>();
            if(healthComponent != null)
            {
                healthComponent.health -= damage;
            }

        }
    
    }

}
