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
    
    public Vector2 grid_to_unity(Coordinate gridtile)
    {
        Vector2 unityVector = new Vector2(gridtile.X, gridtile.Y);
        return unityVector;
    }

    public Coordinate unity_to_grid(Vector2 targetVector)
    {
        Coordinate gridtile = new Coordinate();

        gridtile.X = (int)(targetVector.x + 0.5);
        gridtile.Y = (int)(targetVector.y + 0.5);

        // gridtile.X = targetVector.x;
        // gridtile.Y = targetVector.y;

        return gridtile;
    }

    public bool is_empty(Coordinate dest)
    {
        if (dest.X >= 0 && dest.X < rows && dest.Y >= 0 && dest.Y < cols && tileset[dest.X, dest.Y] == tilestate.empty)
        {
            return true;
        }
        else return false;
    }

    //is reachable과 is empty가 구분될 필요성??
    
}

//좌표값을 저장하기 위한 구조체
public class Coordinate
{
    public int X;
    public int Y;
    
    public Coordinate(int _x, int _y)
    {
        X = _x;
        Y = _y;
    }
    
    public Coordinate()
    {
        X = -1;
        Y = -1;
    }

    public override string ToString()
    {
        return X.ToString() + "," + Y.ToString();
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

