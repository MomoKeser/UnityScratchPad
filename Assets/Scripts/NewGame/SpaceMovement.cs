using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceMovement : MonoBehaviour
{
    public float speed;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        gameObject.transform.position = new Vector3(
            gameObject.transform.position.x + horizontalInput * speed * Time.deltaTime, 
            gameObject.transform.position.y, 
            gameObject.transform.position.z);  
    }
}
