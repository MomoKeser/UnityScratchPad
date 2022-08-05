using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : MonoBehaviour
{
    public Transform playerTransform;// * 2. We want to have the player transform
 /* TODO:
  * 1. We want to grab this gameobject's NavMeshAgent component
  * 2. We want to have the player transform
  * 3. Every frame, we want to call the navmeshagent component's SetDestination function using the player's position
  */   
  
  void Update()// Every frame
  {
    // we want to call the navmeshagent component's SetDestination function
    NavMeshAgent NMA = gameObject.GetComponent<NavMeshAgent>();//* 1. We want to grab this gameobject's NavMeshAgent component
        
    //...using the player's position
    NMA.SetDestination(playerTransform.position);
  }
}