using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpTest : MonoBehaviour
{   
    
    Rigidbody PlayerRigidbody;
    public float JumpForce;
    

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void OnCollisionStay(Collision col)
    {
        Debug.Log("Space");

        if(Input.GetKey(KeyCode.Space))
        {
            if(col.gameObject.CompareTag("Ground"))
            {
                PlayerRigidbody.AddForce(transform.up * JumpForce);
            }
        }
    }
}