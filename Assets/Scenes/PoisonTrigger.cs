using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        DamageOverTimeComponent damageOverTimeComponent = other.gameObject.GetComponent<DamageOverTimeComponent>();
        if(damageOverTimeComponent != null)
        {
            damageOverTimeComponent.enabled = true;
        }
    }
}
