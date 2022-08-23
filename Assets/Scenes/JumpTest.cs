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
        if(Input.GetKey(KeyCode.Space))
        {
            if(col.gameObject.CompareTag("Ground"))
            {
                Debug.Log("Space");
                PlayerRigidbody.AddForce(transform.up * JumpForce);
            }
        }
    }
}