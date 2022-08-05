using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    List<float> staminaIncreases = new List<float>();

    public void AddToStaminaIncreases(float staminaIncrease)
    {
        Debug.Log("Added stamina to inventory");
        staminaIncreases.Add(staminaIncrease);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            //2.it's going to increase stamina by this amount
            if(staminaIncreases.Count > 0)
            {
                float increase = staminaIncreases[0];
                
                //Increase the player's stamina by increase
                //GetComponent<type>(); 
                //This gameobject is referred to as gameObject
                Movement staminaGet = gameObject.GetComponent<Movement>();
                
                staminaGet.stamina += increase;
                
                staminaIncreases.Remove(increase);
            }
        }
    }
}
