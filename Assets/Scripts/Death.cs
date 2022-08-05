using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name == "Enemy")
      {
        Destroy(gameObject);
        Chase Follow = col.gameObject.GetComponent<Chase>();
        Follow.enabled = false;
        
        //OpenGameOverScreen();
      }
    }
}
