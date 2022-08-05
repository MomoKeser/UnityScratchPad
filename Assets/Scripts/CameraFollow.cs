using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform destination;
    private Vector3 offset;
    
    void Start()
    {
        offset = transform.position - destination.position;
    }
    
    void Update()
    {
        transform.position = destination.position + offset;
    }
}
