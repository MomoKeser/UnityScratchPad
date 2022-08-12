using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntidoteTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        /*
            && = AND
            || = OR
            !  = INVERT/NOT
        */  
        DamageOverTimeComponent damageOverTimeComponent = other.gameObject.GetComponent<DamageOverTimeComponent>();
        if(damageOverTimeComponent != null && damageOverTimeComponent.enabled)
        {
            damageOverTimeComponent.enabled = false;
            Destroy(gameObject);
        }
    }
}
