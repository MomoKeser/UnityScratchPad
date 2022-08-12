using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventScript : MonoBehaviour
{
    public UnityEvent myEvent;
  
  public void Update()
  {
    if(Input.GetKeyDown(KeyCode.Space))
    {
        myEvent.Invoke();
    }
  }

}
