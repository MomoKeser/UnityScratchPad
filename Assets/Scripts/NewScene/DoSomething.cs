using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSomething : MonoBehaviour
{
    public void MoveForward()
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z + 100f * Time.deltaTime
            );
    }
}
