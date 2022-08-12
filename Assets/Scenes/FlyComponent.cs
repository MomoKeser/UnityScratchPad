using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyComponent : MonoBehaviour
{

    Rigidbody PlayerRigidbody;
    public float JumpForce;

    // Start is called before the first frame update
    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public void Fly()
    {
        PlayerRigidbody.AddForce(transform.up * JumpForce);    
    }
}
