using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Grid
{
    public int rows, cols;
    public tilestate[,] tileset;

    public Grid(int self_rows, int self_cols)
    {
        rows = self_rows;
        cols = self_cols;
        tilecreate();
    }

    void tilecreate()
    {
        tileset = new tilestate[rows, cols];
    }

}

public enum tilestate
{
    empty,
    box,
    block,
    wall,
    item,
    ballon
}

public class GridCreate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}

