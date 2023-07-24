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
        gameGrid = new Grid(5, 5);
        for (int i = 0; i < gameGrid.rows; i++)
        {
            for (int j = 0; j < gameGrid.cols; j++)
            {
                gameGrid.tileset[i, j] = tilestate.empty;
            }
        }
        gameGrid.tileset[0, 2] = tilestate.box;
        gameGrid.tileset[1, 2] = tilestate.block;
        gameGrid.tileset[2, 2] = tilestate.wall;
        gameGrid.tileset[3, 2] = tilestate.block;
        gameGrid.tileset[4, 2] = tilestate.box;
    }

    public GameObject[,] Object_List;

    public void Start()
    {
        Object_List = Generate_grid(gameGrid);
        character_1.StartPosition = new Vector2(2, 0);
        character_1.transform.position = new Vector2(2, 0);
        character_2.StartPosition = new Vector2(2, 4);
        character_2.transform.position = new Vector2(2, 4);
        for (int i = 0; i < 2; i++)
        {
            int maxInstall = 0, speed = 0, range = 0;
            switch(Random.Range(0, 3))
            {
                case 0:
                    print("0");
                    maxInstall = 1;
                    speed = 2;
                    range = 2;
                    break;
                case 1:
                    print("1");
                    maxInstall = 1;
                    speed = 1;
                    range = 3;
                    break;
                case 2:
                    print("2");
                    maxInstall = 2;
                    speed = 1;
                    range = 1;
                    break;
            }
            if(i == 0)
            {
                character_1.maxInstall = maxInstall;
                character_1.speed_level = speed;
                character_1.range_level = range;
                character_1.FirstCharacter = true;
                character_1.characterPos = new Coordinate(2, 0);
                character_1.StartCharacter();
            }
            else
            {
                character_2.maxInstall = maxInstall;
                character_2.speed_level = speed;
                character_2.range_level = range;
                character_2.FirstCharacter = false;
                character_2.characterPos = new Coordinate(2, 4);
                character_2.StartCharacter();
            }
        }
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

                if (gamegrid.tileset[row, col] == tilestate.wall)
                {
                    GameObject wall_tile = Instantiate(referenece_wall, new Vector3(x, y, y), Quaternion.identity);
                }
                else 
                {
                    GameObject empty_tile = Instantiate(referenece_empty, new Vector3(x, y, 255), Quaternion.identity);
                //  empty_tile.transform.position = new Vector3(x, y, 0);
                }
            }
        }

        for (int row = 0; row < gamegrid.rows; row++)
        {
            for (int col = 0; col < gamegrid.cols; col++)
            {
                float x = row;
                float y = col;

                if (gamegrid.tileset[row, col] == tilestate.block)
                {
                    GameObject block_tile = Instantiate(referenece_block, new Vector3(x, y, y), Quaternion.identity);
                    object_list[row, col] = block_tile;
                }
                else if (gamegrid.tileset[row, col] == tilestate.box)
                {
                    GameObject box_tile = Instantiate(referenece_box, new Vector3(x, y, y), Quaternion.identity);
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

        Coordinate tile = new Coordinate(x, y);
        int rand = Random.Range(0, 10);
        if (rand < 2)
        {
            itemSpawn(tile);
        }
    }


    public void move_box(int x_origin, int y_origin, int x_dest, int y_dest)
    {
        if (gameGrid.tileset[x_dest, y_dest] != tilestate.empty) return;
       
        gameGrid.tileset[x_origin, y_origin] = tilestate.empty;
        gameGrid.tileset[x_dest, y_dest] = tilestate.box;
        Object_List[x_dest, y_dest] = Object_List[x_origin, y_origin];
        Object_List[x_origin, y_origin] = null;

        //box sprite 이동 애니메이션
        Object_List[x_dest, y_dest].transform.position = new Vector3(x_dest, y_dest, 0);
    }

    public GameObject referenece_bubble;
    public GameObject referenece_potion;
    public GameObject referenece_skate;
    public GameObject referenece_needle;
    public void itemSpawn(Coordinate itemdest)
    {
        int item_kind = Random.Range(0, 8);
        gameGrid.tileset[itemdest.X, itemdest.Y] = tilestate.item;
        Item new_item = new Item(item_kind, itemdest);

        switch (item_kind)
        {
            case 0: 
                GameObject itemtile_bubble = Instantiate(referenece_bubble, new Vector3(itemdest.X, itemdest.Y, itemdest.Y), Quaternion.identity);
                break;

            case 1:
                GameObject itemtile_potion = Instantiate(referenece_potion, new Vector3(itemdest.X, itemdest.Y, itemdest.Y), Quaternion.identity);
                break;

            case 4:
                GameObject itemtile_skate = Instantiate(referenece_skate, new Vector3(itemdest.X, itemdest.Y, itemdest.Y), Quaternion.identity);
                break;

            case 5:
                GameObject itemtile_needle = Instantiate(referenece_needle, new Vector3(itemdest.X, itemdest.Y, itemdest.Y), Quaternion.identity);
                break;

        }
    }

    public void item_randspawn()
    {
        int item_kind = Random.Range(0, 8);
        Coordinate randdest = new Coordinate(Random.Range(0, gameGrid.rows), Random.Range(0, gameGrid.cols));
        if (!gameGrid.is_empty(randdest))
        {
            item_randspawn();
        }
        else
        {
            itemSpawn(randdest);
        }
    }
    //정해진 시간에 스폰되게 하는법을 모르겠네요
}
