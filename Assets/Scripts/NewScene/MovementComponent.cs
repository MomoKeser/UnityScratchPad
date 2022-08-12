using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float force;
    [SerializeField] private Transform t;
    private Rigidbody rigidbody;

    void Awake()
    {
        Debug.Log("Creating Movement Component");
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        rigidbody.AddForce(new Vector3(
            horizontalInput * force * Time.deltaTime, 
            0f, 
            verticalInput * force * Time.deltaTime));

        // t.position = new Vector3(
        //         t.position.x + horizontalInput * speed * Time.deltaTime,
        //         t.position.y,
        //         t.position.z + verticalInput * speed * Time.deltaTime);
    }
}
