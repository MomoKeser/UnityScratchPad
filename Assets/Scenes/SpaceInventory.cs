/*
    - When checking item placement, we need an item's width, height and position
    -- Width and height are ints (or Vector2Int if you're feeling fancy), and position is an index/int
  
  - We need an array that holds the items in its cells
  
  - We need our 'neighbor' functions (functions that give the index of our neighbor cells)

    Special/Edge Case:
    - If the index you want to place at outside of the array, dont place it.


*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInventory : MonoBehaviour
{
    
  public class Item
  {
    public int positionX;
    public int positionY;
    public int width;
    public int height;
    public string name;
  }
  
  Item [,] inventory = new Item[10, 10]; //Multidimensional arrays
  
  public void AddItemToInventory(Item item)
  {
    int inventoryWidth  = inventory.GetLength(0);
    int inventoryHeight = inventory.GetLength(1);
    
    int itemBottom = item.positionY + item.height - 1;
    int itemRight = item.positionX + item.width - 1;
    int itemTop = item.positionY;
    int itemLeft = item.positionX;
    
    bool fitsInInventory =  itemBottom < inventoryHeight &&
                                                itemTop >= 0 &&
                                                itemRight < inventoryWidth &&
                                                itemLeft >= 0;
    
    if(!fitsInInventory)
    {
      Debug.Log($"ERROR: Item {item.name} is outside of inventory boundaries!");
      return;
    }
    
    for(int row = 0 + item.positionY; row < item.height + item.positionY; row++)
    {
      for (int col = 0 + item.positionX; col < item.width + item.positionX; col++)
      {
            if(inventory[col, row] != null)
        {
          Debug.Log($"ERROR: Item {item.name} is colliding with another item");
          return;
        }
      }
    }
    
    for(int row = 0 + item.positionY; row < item.height + item.positionY; row++)
    {
      for (int col = 0 + item.positionX; col < item.width + item.positionX; col++)
      {
        inventory[col, row] = item;
      }  
    }
    
    Debug.Log($"Item {item.name} is in the inventory");

  }
  
  private void Awake()
  {
    Item item0 = new Item();
    item0.positionX = 7;
    item0.positionY = 7;
    item0.width = 4;
    item0.height = 2;
    item0.name = "Item 0";
  
    Item item1 = new Item();
    item1.positionX = 1;
    item1.positionY = 1;
    item1.width = 4;
    item1.height = 2;
    item1.name = "Item 1";
    
    Item item2 = new Item();
    item2.positionX = 5;
    item2.positionY = 1;
    item2.width = 2;
    item2.height = 2;
    item2.name = "Item 2";
    
    Item item3 = new Item();
    item3.positionX = 0;
    item3.positionY = 2;
    item3.width = 2;
    item3.height = 2;
    item3.name = "Item 3";
    
    AddItemToInventory(item0);
    AddItemToInventory(item1);
    AddItemToInventory(item2);
    AddItemToInventory(item3);
  }
  
  public bool IsInsideBox(Vector2 boxCenter, Vector2 boxSize, Vector2 pointToCheck)
  {
    //1. Compare the position to the box's topmost, bottommost, leftmost and rightmost points.
    //In order to do that, I need to calculate all those points.
    float leftMostPoint = boxCenter.x - boxSize.x/2f;
    float rightMostPoint = boxCenter.x + boxSize.x/2f;
    float bottomMostPoint = boxCenter.y - boxSize.y/2f;
    float topMostPoint = boxCenter.y + boxSize.y/2f;

    //2. If the pointToCheck is in the box, return true. Else, return false.
    bool isInBox = leftMostPoint <= pointToCheck.x && pointToCheck.x <= rightMostPoint &&
                   bottomMostPoint <= pointToCheck.y && pointToCheck.y <= topMostPoint;
        
    return isInBox;
  }
    
  public RectTransform box;
  public UnityEngine.UI.Image boxImage;

  bool isDraggingBox = false;
  Vector2 dragOffset;

  private void Update()
  {
    Vector3 mousePosition = Input.mousePosition;

    bool isInsideBox = IsInsideBox(box.anchoredPosition + new Vector2(Screen.width/2f, Screen.height/2f), box.rect.size, mousePosition);
      
    Debug.Log(box.anchoredPosition + new Vector2(Screen.width/2f, Screen.height/2f));

    if(Input.GetMouseButton(0) && isInsideBox)
    {
      isDraggingBox = true;
      dragOffset = box.anchoredPosition - new Vector2(mousePosition.x, mousePosition.y) - new Vector2(Screen.width/2f, Screen.height/2f);
    }
    
    if(Input.GetMouseButtonUp(0))
    {
      isDraggingBox = false;
    }
    
    if (isDraggingBox)
    {
      box.anchoredPosition = box.anchoredPosition + dragOffset - new Vector2(Screen.width/2f, Screen.height/2f);
      boxImage.color = new Color(0f, 1f, 0f, 1f);
    }
    else
    {
      boxImage.color = new Color(1f, 0f, 0f, 1f);

    }

  }

}
