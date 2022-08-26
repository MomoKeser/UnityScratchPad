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
  
  Item [,] inventory = new Item[8, 8]; //Multidimensional arrays
  asdasffs
  public bool FitsInInventory(Item item, Vector2Int position)
  {
    int inventoryWidth  = inventory.GetLength(0);
    int inventoryHeight = inventory.GetLength(1);
    
    int itemBottom = position.y + item.height - 1;
    int itemRight = position.x + item.width - 1;
    int itemTop = position.y;
    int itemLeft = position.x;
    
    bool inInventoryBorders = itemBottom < inventoryHeight &&
            itemTop >= 0 &&
            itemRight < inventoryWidth &&
            itemLeft >= 0;  
    
    if(!inInventoryBorders)
    {
      return false;
    }
            
    for(int row = position.y; row < item.height + position.y; row++)
    {
      for(int col = position.x; col < item.width + position.x; col++)
      {
        if(inventory[col, row] != null)
        {
          return false;
        }
      }
    }
    
    return true;
  }
  
  public bool AddItemToInventory(Item item)
  {
    bool fitsInInventory = FitsInInventory(item, new Vector2Int(item.positionX, item.positionY));
    if(!fitsInInventory)
    {
      Debug.Log($"ERROR: Item {item.name} is outside of inventory boundaries!");
      return false;
    }
    
    for(int row = 0 + item.positionY; row < item.height + item.positionY; row++)
    {
      for (int col = 0 + item.positionX; col < item.width + item.positionX; col++)
      {
        inventory[col, row] = item;
      }  
    }
    
    Debug.Log($"Item {item.name} is in the inventory");
    
    for(int y = 0; y < item.height; y++)
    {
      for(int x = 0; x < item.width; x++)
      {
        Vector2Int cellIndex = new Vector2Int(item.positionX + x, item.positionY + y);
        int cellFlatIndex = cellIndex.y * inventory.GetLength(0) + cellIndex.x;
        tiles[cellFlatIndex].GetComponent<UnityEngine.UI.Image>().color = Color.red;
      }
    }
    
    return true;
  }
  
  List<GameObject> tiles = new List<GameObject>();
  public GameObject cellPrefab;
  public Transform canvas;
  public Vector2 debugInventoryScreenOffset;

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
    item2.height = 1;
    item2.name = "Item 2";
    
    Item item3 = new Item();
    item3.positionX = 0;
    item3.positionY = 2;
    item3.width = 1;
    item3.height = 2;
    item3.name = "Item 3";
    
    for(int y = 0; y < inventory.GetLength(1); y++)
    {
      for(int x = 0; x < inventory.GetLength(0); x++)
      {
        GameObject tile = GameObject.Instantiate(cellPrefab);
        tile.GetComponent<RectTransform>().anchoredPosition = new Vector2(x * cellWidth, Screen.height - y * cellHeight) + debugInventoryScreenOffset;
        tile.transform.parent = canvas;
        tiles.Add(tile);
      }
    }
    
    // AddItemToInventory(item0);
    // AddItemToInventory(item1);
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

  public bool IsInsideBox(RectTransform rectTransform, Vector2 pointToCheck)
  {
    Vector2 boxCenter = new Vector2((0.5f - rectTransform.pivot.x) * rectTransform.rect.width, (0.5f - rectTransform.pivot.y) * rectTransform.rect.height) + rectTransform.anchoredPosition;
    return IsInsideBox(boxCenter, rectTransform.rect.size, pointToCheck);
  }
    
  public RectTransform box;
  public UnityEngine.UI.Image boxImage;

  bool isDraggingBox = false;
  Vector3 dragOffset;
  Item draggedItem = null;
  
  public float cellWidth;
  public float cellHeight;
  
  public Vector2Int WorldTo2dIndex(Vector2 point, Vector2 offset)
  {
    Vector2 transformedPoint = new Vector2(point.x, -point.y) + new Vector2(-debugInventoryScreenOffset.x, debugInventoryScreenOffset.y) + new Vector2(Screen.width/2f, Screen.height/2f);
    return new Vector2Int((int)(transformedPoint.x / cellWidth), (int)(transformedPoint.y / cellHeight));
  }

  private void Update()
  {
    Vector3 mousePosition = Input.mousePosition - new Vector3(Screen.width/2f, Screen.height/2f);//The mouse is in screen space, the rect transform is in centered-screen space

    bool isInsideBox = IsInsideBox(box, mousePosition);
        
    Vector2Int mouseIndex = WorldTo2dIndex(mousePosition, debugInventoryScreenOffset);
    int mouseFlatIndex = mouseIndex.y * inventory.GetLength(0) + mouseIndex.x;
    
    if(Input.GetMouseButtonDown(0))
    {
      if(inventory[mouseIndex.x, mouseIndex.y] != null)
      {
        draggedItem = inventory[mouseIndex.x, mouseIndex.y];
        Debug.Log("Dragging Item!");
      }
      // Item item0 = new Item();
      // item0.positionX = mouseIndex.x;
      // item0.positionY = mouseIndex.y;
      // item0.width = 2;
      // item0.height = 2;
      // item0.name = "Item 0";
      // if(AddItemToInventory(item0))
      // {
      //   for(int y = 0; y < item0.height; y++)
      //   {
      //     for(int x = 0; x < item0.width; x++)
      //     {
      //       Vector2Int cellIndex = new Vector2Int(item0.positionX + x, item0.positionY + y);
      //       int cellFlatIndex = cellIndex.y * inventory.GetLength(0) + cellIndex.x;
      //       tiles[cellFlatIndex].GetComponent<UnityEngine.UI.Image>().color = Color.red;
      //     }
      //   }
      // }
    }
    
    if(Input.GetMouseButtonUp(0))
    {
      if(draggedItem != null)
      {
        //TODO: Check if you can place this item where the mouse is
        if(FitsInInventory(draggedItem, mouseIndex))
        {
        
          //Remove the dragged item from the inventory
          for(int y = 0; y < draggedItem.height; y++)
          {
            for(int x = 0; x < draggedItem.width; x++)
            {
              Vector2Int cellIndex = new Vector2Int(draggedItem.positionX + x, draggedItem.positionY + y);
              int cellFlatIndex = cellIndex.y * inventory.GetLength(0) + cellIndex.x;
              tiles[cellFlatIndex].GetComponent<UnityEngine.UI.Image>().color = Color.white;
              inventory[cellIndex.x, cellIndex.y] = null;
            }
          }
          
          draggedItem.positionX = mouseIndex.x;
          draggedItem.positionY = mouseIndex.y;
          
          
          AddItemToInventory(draggedItem);
          
          
        }
        else
        {
          Debug.Log("Doesnt fit in inventory");
        }
      }
      
      draggedItem = null;
    }
    
  }

}
