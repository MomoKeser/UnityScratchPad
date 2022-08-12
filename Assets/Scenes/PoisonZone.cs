using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        DamageOverTimeComponent damageOverTimeComponent = other.gameObject.GetComponent<DamageOverTimeComponent>();
        if(damageOverTimeComponent != null)
        {
            damageOverTimeComponent.enabled = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        DamageOverTimeComponent damageOverTimeComponent = other.gameObject.GetComponent<DamageOverTimeComponent>();
        if(damageOverTimeComponent != null)
        {
            damageOverTimeComponent.enabled = false;
        }
    }
}
