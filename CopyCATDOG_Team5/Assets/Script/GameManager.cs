using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public Character_Controller character_1;
    public Character_Controller character_2;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    public Grid gameGrid;

    public void OnEnable()
    {
        gameGrid = new Grid(4, 5);
        for (int i = 0; i < gameGrid.rows; i++)
        {
            for (int j = 0; j < gameGrid.cols; j++)
            {
                gameGrid.tileset[i, j] = tilestate.empty;
            }
        }
        gameGrid.tileset[1, 0] = tilestate.block;
        gameGrid.tileset[1, 2] = tilestate.box;
    }

    public GameObject[,] Object_List;

    public void Start()
    {
        Object_List = Generate_grid(gameGrid);
    }

    public GameObject referenece_empty;
    public GameObject referenece_wall;
    public GameObject referenece_box;
    public GameObject referenece_block;

    private GameObject[,] Generate_grid(Grid gamegrid)
    {
        GameObject[,] object_list = new GameObject[gamegrid.rows, gamegrid.cols];

        for (int row = 0; row < gamegrid.rows; row++)
        {
            for (int col = 0; col < gamegrid.cols; col++)
            {
                float x = row;
                float y = col;

                if (gamegrid.tileset[row, col] == tilestate.empty)
                {
                    GameObject empty_tile = Instantiate(referenece_empty, new Vector3(x, y, 0), Quaternion.identity);
                //  empty_tile.transform.position = new Vector3(x, y, 0);
                }
                else if (gamegrid.tileset[row, col] == tilestate.wall)
                {
                    GameObject wall_tile = Instantiate(referenece_wall, new Vector3(x, y, 0), Quaternion.identity);
                }
                else if (gamegrid.tileset[row, col] == tilestate.block)
                {
                    GameObject block_tile = Instantiate(referenece_block, new Vector3(x, y, 0), Quaternion.identity);
                    object_list[row, col] = block_tile;
                }
                else if (gamegrid.tileset[row, col] == tilestate.box)
                {
                    GameObject box_tile = Instantiate(referenece_box, new Vector3(x, y, 0), Quaternion.identity);
                    object_list[row, col] = box_tile;
                }
            }
        }
        return object_list;
    }


    public void destroy_tile(int x, int y)
    {
        gameGrid.tileset[x, y] = tilestate.empty;
        Destroy(Object_List[x, y]);
    }


    public void move_box(int x_origin, int y_origin, int x_dest, int y_dest)
    {
        if (gameGrid.tileset[x_dest, y_dest] != tilestate.empty) return;

        gameGrid.tileset[x_origin, y_origin] = tilestate.empty;
        gameGrid.tileset[x_dest, y_dest] = tilestate.box;
        Object_List[x_dest, y_dest] = Object_List[x_origin, y_origin];
        Object_List[x_origin, y_origin] = null;

        //box sprite 이동 애니메이션
        Object_List[x_dest, y_dest].transform.Translate(new Vector3(x_dest, y_dest, 0));

    }

}
