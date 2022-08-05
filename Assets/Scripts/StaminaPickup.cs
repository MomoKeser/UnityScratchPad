using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaPickup : MonoBehaviour
{
    public float staminaIncrease;
    void OnTriggerEnter(Collider other)
    {
        //1.It's going to destroy itself
        Destroy(gameObject);

        Inventory inv = other.gameObject.GetComponent<Inventory>();
        if(inv != null)
        {
            inv.AddToStaminaIncreases(staminaIncrease);
        }
        
        //2.it's going to increase stamina by this amount
        //GetComponent<>//This gets a component from a gameobject
        // Movement movement = other.gameObject.GetComponent<Movement>();
        // if(movement != null)
        // {
        //     movement.stamina = movement.stamina + staminaIncrease;
        //     if(movement.stamina > movement.maxStamina)
        //     {
        //         movement.stamina = movement.maxStamina;
        //     }
        // }
    }

}
