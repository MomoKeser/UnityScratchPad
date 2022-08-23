using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private Transform[] cells;
    private bool[] isWall;
    private List<transform_and_index> walls = new List<transform_and_index>();
    [SerializeField] private int width;
    [SerializeField] private int height;

    struct transform_and_index
    {
        public Transform t;
        public Vector2Int index;
    }

    void Awake()
    {
        cells = new Transform[width * height];
        isWall = new bool[width * height];
    }


    //Where arrayWidth is the X length of the array, AKA column count
    Vector2Int GetIndex(int flatIndex, int arrayWidth)
    {
        Vector2Int index = Vector2Int.zero;
        index.x = flatIndex % arrayWidth;
        index.y = flatIndex / arrayWidth;
        return index;
    }



    public void GenerateMaze()
    {
        //  PRIM'S ALGORITHM 
        // 1. Start with a grid full of walls.
        for(int i = 0; i < isWall.Length; ++i)
        {
            isWall[i] = true;
        }

        // 2. Pick a cell, mark it as part of the maze. Add the walls of the cell to the wall list.
        int randomCellIndex = Random.Range(0, isWall.Length);
        isWall[randomCellIndex] = false;

        

        // 3. While there are walls in the list:
        //      3.1 Pick a random wall from the list. If only one of the cells that the wall divides is visited, then:
        //          3.1.1 Make the wall a passage and mark the unvisited cell as part of the maze.
        //          3.1.2 Add the neighboring walls of the cell to the wall list.
        //      3.2 Remove the wall from the list.  
    }

}
