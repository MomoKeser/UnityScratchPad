using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionComponent : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
    }

    void OnCollisionExit(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.collider.gameObject.name);
    }


}
