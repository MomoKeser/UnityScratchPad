using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerationComponent : MonoBehaviour
{
    public int width;
    public int height;
    bool [] isWall;
    GameObject [] walls;
    GameObject [] enemyWalls;
    GameObject [] borderWalls;
    
    public GameObject wallPrefab;
    public GameObject enemyWallPrefab;

    private void Awake()
    {
        isWall = new bool[width * height];
        walls = new GameObject[width * height];
        enemyWalls = new GameObject[width * height];
        
        for(int i = 0; i < walls.Length; i++)
        {
          walls[i] = GameObject.Instantiate(wallPrefab);
          enemyWalls[i] = GameObject.Instantiate(enemyWallPrefab);
          walls[i].SetActive(false);
          enemyWalls[i].SetActive(false);
          int column = i % width;
          int row = i / width;
          walls[i].transform.position = new Vector3(
            column,
            0f,
            row);
          enemyWalls[i].transform.position = new Vector3(
            column,
            0f,
            row);
        }

        int outerWidth = width + 2;
        int outerHeight = height + 2;
        borderWalls = new GameObject[outerWidth * outerHeight];
        for(int i = 0; i < borderWalls.Length; i++)
        {
          borderWalls[i] = GameObject.Instantiate(wallPrefab);
          borderWalls[i].SetActive(false);
          int column = i % outerWidth - 1;
          int row = i / outerHeight - 1;
          borderWalls[i].transform.position = new Vector3(
            column,
            0f,
            row);
        }


        for(int i = 0; i < outerWidth * outerHeight; i++)
        {
            if(
            !LeftNeighborExists(i, outerWidth)  ||
            !RightNeighborExists(i, outerWidth) ||
            !AboveNeighborExists(i, outerWidth) ||
            !BelowNeighborExists(i, outerWidth, outerHeight))
            {
                borderWalls[i].SetActive(true);
            }
            
        }

    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
      {
        CreateMaze();
        for(int i = 0; i < isWall.Length; i++)
        {
          if(Random.Range(0, 100) < 50)
          {
            enemyWalls[i].SetActive(isWall[i]);
            walls[i].SetActive(false);
          }
          else
          {
            walls[i].SetActive(isWall[i]);
            enemyWalls[i].SetActive(false);
          }
        }
      }
    }

    public bool AboveNeighborExists(int index, int width)
    {
        return index >= width;
    }
    public bool BelowNeighborExists(int index, int width, int height)
    {
        return index < (width * height) - width;
    }
    public bool LeftNeighborExists(int index, int width)
    {
        return index % width > 0;
    }
    public bool RightNeighborExists(int index, int width)
    {
        int column = index % width;
      int lastColumn = width - 1;
      return column < lastColumn;
    }

    public int GetAboveNeighborIndex(int index)
    {
        return index - width;
    }
    
    public int GetBelowNeighborIndex(int index)
    {
        return index + width;
    }
    
    public int GetLeftNeighborIndex(int index)
    {
        return index - 1;
    }
    
    public int GetRightNeighborIndex(int index)
    {
        return index + 1;
    }

    public void CreateMaze()
    {
      //  PRIM'S ALGORITHM
      // 1. Start with a grid full of walls.
      for(int i = 0; i < isWall.Length; i++)
      {
        isWall[i] = true;
      }
      
      // 2. Pick a random cell, mark it as part of the maze (empty).
      List<int> wallsToCheckIndices = new List<int>();
      int randomCellIndex = Random.Range(0, isWall.Length);
      isWall[randomCellIndex] = false;
        
      //Add the walls (neighbors) of the cell to the wall list.
      if(AboveNeighborExists(randomCellIndex, width))
      {
        int neighborIndex = GetAboveNeighborIndex(randomCellIndex);
        wallsToCheckIndices.Add(neighborIndex);
      }
      if(BelowNeighborExists(randomCellIndex, width, height))
      {
        int neighborIndex = GetBelowNeighborIndex(randomCellIndex);
        wallsToCheckIndices.Add(neighborIndex);
      }
      if(LeftNeighborExists(randomCellIndex, width))
      {
        int neighborIndex = GetLeftNeighborIndex(randomCellIndex);
        wallsToCheckIndices.Add(neighborIndex);
      }
      if(RightNeighborExists(randomCellIndex, width))
      {
        int neighborIndex = GetRightNeighborIndex(randomCellIndex);
        wallsToCheckIndices.Add(neighborIndex);
      }
      
      // 3. While there are walls in the list:
      while(wallsToCheckIndices.Count > 0)
      {
        //3.1 Pick a random wall from the list...
        int randomWallIndex = wallsToCheckIndices[Random.Range(0, wallsToCheckIndices.Count)];
        
        //...If only one of the cells that the wall divides is visited, then: (Basically if the empty neighbor count is one)
        int emptyNeighborCount = 0;
        if(AboveNeighborExists(randomWallIndex, width))
        {
          int neighborIndex = GetAboveNeighborIndex(randomWallIndex);
          if(!isWall[neighborIndex])
          {
            emptyNeighborCount++;
          }
        }
        if(BelowNeighborExists(randomWallIndex, width, height))
        {
          int neighborIndex = GetBelowNeighborIndex(randomWallIndex);
          if(!isWall[neighborIndex])
          {
            emptyNeighborCount++;
          }
        }
        if(LeftNeighborExists(randomWallIndex, width))
        {
          int neighborIndex = GetLeftNeighborIndex(randomWallIndex);
          if(!isWall[neighborIndex])
          {
            emptyNeighborCount++;
          }
        }
        if(RightNeighborExists(randomWallIndex, width))
        {
          int neighborIndex = GetRightNeighborIndex(randomWallIndex);
          if(!isWall[neighborIndex])
          {
            emptyNeighborCount++;
          }
        }

        if(emptyNeighborCount == 1)
        {
          //    3.1.1 Make the wall a passage (empty) and mark the unvisited cell as part of the maze.
          isWall[randomWallIndex] = false;
          //  3.1.2 Add the neighboring walls of the cell to the wall list.
                    if(AboveNeighborExists(randomWallIndex, width))
          {
            int neighborIndex = GetAboveNeighborIndex(randomWallIndex);
            if(isWall[neighborIndex] && !wallsToCheckIndices.Contains(neighborIndex))
            {
                            wallsToCheckIndices.Add(neighborIndex);
            }
          }
          if(BelowNeighborExists(randomWallIndex, width, height))
          {
            int neighborIndex = GetBelowNeighborIndex(randomWallIndex);
                        if(isWall[neighborIndex] && !wallsToCheckIndices.Contains(neighborIndex))
            {
                            wallsToCheckIndices.Add(neighborIndex);
            }          
          }
          if(LeftNeighborExists(randomWallIndex, width))
          {
            int neighborIndex = GetLeftNeighborIndex(randomWallIndex);
            if(isWall[neighborIndex] && !wallsToCheckIndices.Contains(neighborIndex))
            {
                            wallsToCheckIndices.Add(neighborIndex);
            }
          }
          if(RightNeighborExists(randomWallIndex, width))
          {
            int neighborIndex = GetRightNeighborIndex(randomWallIndex);
                        if(isWall[neighborIndex] && !wallsToCheckIndices.Contains(neighborIndex))
            {
                            wallsToCheckIndices.Add(neighborIndex);
            }          
          }
        }
        
        //3.2 Remove the wall from the list.
        wallsToCheckIndices.Remove(randomWallIndex);
        
      }
      
      //Close the borders
      // for(int i = 0; i < isWall.Length; i++)
      // {
      //   if(
      //       !LeftNeighborExists(i)  ||
      //       !RightNeighborExists(i) ||
      //       !AboveNeighborExists(i) ||
      //       !BelowNeighborExists(i))
      //   {
      //               isWall[i] = true;
      //   }
      // }
      
      
    }

}
