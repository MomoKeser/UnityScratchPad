using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Observer : MonoBehaviour
{
    [SerializeField] private UnityEvent _event;
    [SerializeField] private GameObject playerPrefab;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _event?.Invoke();
            GameObject clone = GameObject.Instantiate(playerPrefab);
        }
    }


}
