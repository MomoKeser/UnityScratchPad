using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeverSleep : MonoBehaviour
{
    void Awake()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.sleepThreshold = 0.0f;
    }
}
