using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreator : MonoBehaviour
{

    public GameObject wallPrefab;
    bool [][] isWall;//If the element is true, it is a wall. If false, it is empty.
    bool [][] outputBuffer;
    public int generationCount;
    Transform [][] walls;

    public int height;
    public int width;

    void GenerateWalls()
    {
        for(int i = 0; i < isWall.Length; i++)
        {
            for(int j = 0; j < isWall[i].Length; j++)
            {
                isWall[i][j] = UnityEngine.Random.Range(0f, 1f) <= 0.5f ? false : true;
            }
        }

        bool [][] frontBuffer = isWall;
        bool [][] backBuffer  = outputBuffer;

        for(int k = 0; k < generationCount; k++)
        {
            for(int i = 0; i < isWall.Length; i++)
            {
                for(int j = 0; j < isWall[i].Length; j++)
                {
                    int aliveNeighborCount = 0;
                    for(int neighborRow = i - 1; neighborRow <= i + 1; neighborRow++)
                    {
                        if(neighborRow < 0 || neighborRow >= isWall.Length) continue;
                        for(int neighborColumn = j - 1; neighborColumn <= j + 1; neighborColumn++)
                        {
                            if(neighborColumn < 0 || neighborColumn >= isWall[i].Length) continue;
                            if(neighborRow == i && neighborColumn == j) continue;
                            
                            if(frontBuffer[neighborRow][neighborColumn])
                            {
                                aliveNeighborCount++;
                            }
                        }    
                    }
                    if(aliveNeighborCount > 3 && aliveNeighborCount < 6)
                    {
                        backBuffer[i][j] = true;
                    }
                    else
                    {
                        backBuffer[i][j] = false;   
                    }
                }   
            }
            bool [][] temp = frontBuffer;
            frontBuffer = backBuffer;
            backBuffer = temp;            
        }    

        //Fill out the edges
        for(int i = 0; i < backBuffer.Length; i++)
        {
            backBuffer[i][0] = true;
            backBuffer[i][backBuffer[i].Length - 1] = true;
        }
        for(int i = 0; i < backBuffer[0].Length; i++)
        {
            backBuffer[0][i] = true;
        }
        for(int i = 0; i < backBuffer[backBuffer.Length-1].Length; i++)
        {
            backBuffer[backBuffer.Length-1][i] = true;
        }

        //Remove no-neighbor walls
        for(int i = 0; i < isWall.Length; i++)
        {
            for(int j = 0; j < isWall[i].Length; j++)
            {
                int aliveNeighborCount = 0;
                for(int neighborRow = i - 1; neighborRow <= i + 1; neighborRow++)
                {
                    if(neighborRow < 0 || neighborRow >= isWall.Length) continue;
                    for(int neighborColumn = j - 1; neighborColumn <= j + 1; neighborColumn++)
                    {
                        if(neighborColumn < 0 || neighborColumn >= isWall[i].Length) continue;
                        if(neighborRow == i && neighborColumn == j) continue;
                        
                        if(backBuffer[neighborRow][neighborColumn])
                        {
                            aliveNeighborCount++;
                        }
                    }    
                }
                if(aliveNeighborCount == 0)
                {
                    backBuffer[i][j] = false;
                }
            }   
        }
        
        //Reset the walls
        for(int i = 0; i < walls.Length; i++)
        {
            for(int j = 0; j < walls[i].Length; j++)
            {
                walls[i][j].gameObject.SetActive(false);
            }
        }

        //Position the wall gameobjects
        for(int i = 0; i < backBuffer.Length; i++)
        {
            for(int j = 0; j < backBuffer[i].Length; j++)
            {
                if(backBuffer[i][j])
                {
                    Collider wallCollider = walls[i][j].GetComponent<Collider>();
                    walls[i][j].gameObject.SetActive(true);
                    walls[i][j].position = new Vector3(
                        (float)j * wallCollider.bounds.size.x,
                        wallCollider.bounds.extents.y,
                        (float)i * wallCollider.bounds.size.z);
                }
            }
        }
    }

    void Awake()
    {
        isWall = new bool[height][];
        for(int i = 0; i < isWall.Length; i++)
        {
            isWall[i] = new bool[width];
        }
        outputBuffer = new bool[height][];
        for(int i = 0; i < outputBuffer.Length; i++)
        {
            outputBuffer[i] = new bool[width];
        }
        walls = new Transform[height][];
        for(int i = 0; i < walls.Length; i++)
        {
            walls[i] = new Transform[width];
            for(int j = 0; j < walls[i].Length; j++)
            {
                walls[i][j] = GameObject.Instantiate(wallPrefab).transform;
                walls[i][j].gameObject.SetActive(false);
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateWalls();
        }
    }

}
