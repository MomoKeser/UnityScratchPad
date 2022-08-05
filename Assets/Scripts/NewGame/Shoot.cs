using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        { 
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.transform.position = gameObject.transform.position;
        }
    }
}
