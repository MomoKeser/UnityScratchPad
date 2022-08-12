using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Sprite image;
    public string name;
    public GameObject interactionImage;
    public InventoryComponent inventory;

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Vector3 vectorFromPlayerToItem = transform.position - other.gameObject.transform.position;
            Vector3 playerForward = other.gameObject.transform.forward;

            if(Vector3.Dot(vectorFromPlayerToItem.normalized, playerForward) > 0.8f)
            {
                interactionImage.SetActive(true);
                if(Input.GetKeyDown(KeyCode.E))
                {
                    inventory.AddItem(this);
                    gameObject.SetActive(false);
                    interactionImage.SetActive(false);
                }
            }
            else
            {
                interactionImage.SetActive(false);
            }
        }
    }

    public virtual void UseEffect()
    {
       Debug.Log("Base Item effect!");
    }
}
