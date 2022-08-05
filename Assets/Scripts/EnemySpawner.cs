using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    
    public void SpawnEnemy(float minX, float minZ, float maxX, float maxZ)
    {
      GameObject instance = GameObject.Instantiate(enemyPrefab);
      float RandomPosX = Random.Range(minX, maxX);
      float RandomPosZ = Random.Range(minZ, maxZ);
      instance.transform.position = new Vector3(RandomPosX, 0.5f, RandomPosZ);
    }
    
    //NOTE: Every component/monobehaviour has a GameObject variable called gameObject. This is the
    //GameObject that the component is attached to
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Other's name: " + other.gameObject.name);
      BoxCollider EnemyArea = GetComponent<BoxCollider>();
      float leftMostPosition = EnemyArea.bounds.min.x;
      float rightMostPosition = EnemyArea.bounds.max.x;
      float forwardMostPosition = EnemyArea.bounds.max.z;
      float backwardMostPosition = EnemyArea.bounds.min.z;
      SpawnEnemy(leftMostPosition, backwardMostPosition, rightMostPosition, forwardMostPosition);
    }

    void OnCollisionEnter(Collision collision)//This is for physics simulated colliders
    {
        Debug.Log("Other's name: " + collision.collider.gameObject.name);
        BoxCollider EnemyArea = GetComponent<BoxCollider>();
        float leftMostPosition = EnemyArea.bounds.min.x;
        float rightMostPosition = EnemyArea.bounds.max.x;
        float forwardMostPosition = EnemyArea.bounds.max.z;
        float backwardMostPosition = EnemyArea.bounds.min.z;
        SpawnEnemy(leftMostPosition, backwardMostPosition, rightMostPosition, forwardMostPosition);
    }

}
