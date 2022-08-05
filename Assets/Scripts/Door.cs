using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
        public string nextLevelName;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
      {
        //SceneManager.LoadScene
        SceneManager.LoadScene(nextLevelName, LoadSceneMode.Additive);
      }
    }
}
