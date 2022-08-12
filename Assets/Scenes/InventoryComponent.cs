using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryComponent : MonoBehaviour
{
    Item [] items = new Item[24];
    public Image [] images;
    public GameObject itemUi;

    public void AddItem(Item item)
    {
        bool isAdded = false;
        for(int i = 0; i < items.Length; i++)
        {
          if(isAdded == false)
          {
            if(items[i] == null)
            {
              items[i] = item;
              images[i].sprite = item.image;
              isAdded = true;
            }
          }
        }
    }
    
    KeyCode lastKeyPressed;
    public void Update()
    {
        if(Input.GetKey(KeyCode.Tab))
      {
        itemUi.SetActive(true);
        lastKeyPressed = KeyCode.Tab;
      }
      else if(lastKeyPressed == KeyCode.Tab)
      {
        itemUi.SetActive(false);
      }
      
      if(Input.GetKeyDown(KeyCode.I))
      {
        lastKeyPressed = KeyCode.I;
        bool newActive = !itemUi.activeSelf;
        itemUi.SetActive(newActive);
      }
    }

    public void RemoveItem(int index)
    {
        if(items[index] != null)
        {
          // items[i] = item;
          images[index].sprite = null;
          GameObject itemToDrop = items[index].gameObject;
          itemToDrop.SetActive(true);

          itemToDrop.transform.position = transform.position + transform.forward * 5f + Vector3.up;


          items[index].UseEffect();

          items[index] = null;
        }
        else
        {
            Debug.Log("This slot is already empty!");
        }
    }

}
