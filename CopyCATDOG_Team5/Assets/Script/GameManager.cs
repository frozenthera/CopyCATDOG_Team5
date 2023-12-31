using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public Character_Controller character_1;
    public Character_Controller character_2;

    public GameObject referenece_empty;
    public GameObject referenece_wall;
    public GameObject referenece_box;
    public GameObject referenece_block;
    public GameObject reference_border;

    public Grid gameGrid;
    public List<tilestate> map = new();
    public List<tilestate> map2 = new();
    public List<tilestate> mini_map = new();

    public int mapIdx = 0;

    public GameObject[,] Object_List;

    public int player1Select, player2Select;

    public bool game_is_pause = false;
    public bool game_is_over = false;
    public bool game_is_end = false;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;
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
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Main")
        {
            SetMainScene();
        }
        if(scene.name == "Main_chaehun")
        {
            SetMINIinScene();
        }
    }

    void  SetMainScene()
    {
        character_1 = GameObject.Find("Character1").GetComponent<Character_Controller>();
        character_2 = GameObject.Find("Character2").GetComponent<Character_Controller>();

        Screen.SetResolution(1920, 1080, true);

        
        Object_List = Generate_map(mapIdx == 0  ? map : map2);
        Generate_border(gameGrid);

        if(mapIdx ==  0)
        {
            character_1.StartPosition = gameGrid.grid_to_unity(new Coordinate(1, 1));
            character_2.StartPosition = gameGrid.grid_to_unity(new Coordinate(13, 12));
        }
        else
        {
            character_1.StartPosition = gameGrid.grid_to_unity(new Coordinate(8, 12));
            character_2.StartPosition = gameGrid.grid_to_unity(new Coordinate(14, 3));
        }

        for (int i = 0; i < 2; i++)
        {
            int selectPlayer;
            if (i == 0)
                selectPlayer = player1Select;
            else
                selectPlayer = player2Select;
            int maxInstall = 0, speed = 0, range = 0;
            switch (selectPlayer)
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
            if (i == 0)
            {
                character_1.maxInstall = maxInstall;
                character_1.speed_level = speed;
                character_1.range_level = range;
                character_1.FirstCharacter = true;
                character_1.characterPos = new Coordinate(1, 0);
                character_1.StartCharacter();
            }
            else
            {
                character_2.maxInstall = maxInstall;
                character_2.speed_level = speed;
                character_2.range_level = range;
                character_2.FirstCharacter = false;
                character_2.characterPos = new Coordinate(1, 1);
                character_2.StartCharacter();
            }
        }

        InvokeRepeating("item_randspawn", 10f, 10f);
    }
    void SetMINIinScene()
    {
        character_1 = GameObject.Find("Character1").GetComponent<Character_Controller>();
        character_2 = GameObject.Find("Character2").GetComponent<Character_Controller>();

        Screen.SetResolution(1920, 1080, true);

        Object_List = Generate_map(mini_map);
        Generate_border(gameGrid);

        character_1.StartPosition = gameGrid.grid_to_unity(new Coordinate(2, 2));
        character_2.StartPosition = gameGrid.grid_to_unity(new Coordinate(3, 2));

        character_1.needlecount = false;
        character_2.needlecount = false;

        for (int i = 0; i < 2; i++)
        {
            int selectPlayer;
            if (i == 0)
                selectPlayer = player1Select;
            else
                selectPlayer = player2Select;
            int maxInstall = 0, speed = 0, range = 0;
            switch (selectPlayer)
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
            if (i == 0)
            {
                character_1.maxInstall = maxInstall;
                character_1.speed_level = speed;
                character_1.range_level = range;
                character_1.FirstCharacter = true;
                character_1.characterPos = new Coordinate(1, 0);
                character_1.StartCharacter();
            }
            else
            {
                character_2.maxInstall = maxInstall;
                character_2.speed_level = speed;
                character_2.range_level = range;
                character_2.FirstCharacter = false;
                character_2.characterPos = new Coordinate(1, 1);
                character_2.StartCharacter();
            }
        }
    }

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

    private GameObject[,] Generate_map(List<tilestate> maplist)
    {
        gameGrid = new Grid(15, 13);
        for (int row = 0; row < 15; row++)
        {
            for (int col = 0; col < 13; col++)
            {
                gameGrid.tileset[row, col] = maplist[row + 15 * col];
            }
        }

        GameObject[,] object_list = new GameObject[gameGrid.rows, gameGrid.cols];

        for (int row = 0; row < 15; row++)
        {
            for (int col = 0; col < 13; col++)
            {
                float x = row;
                float y = col;

                //Debug.Log(maplist[row + 15 * col]);

                if (maplist[row + 15 * col] == tilestate.wall)
                {
                    GameObject wall_tile = Instantiate(referenece_wall, new Vector3(x, y, y), Quaternion.identity);
                }
                else
                {
                    GameObject empty_tile = Instantiate(referenece_empty, new Vector3(x, y, 255), Quaternion.identity);
                }
            }
        }

        for (int row = 0; row < 15; row++)
        {
            for (int col = 0; col < 13; col++)
            {
                float x = row;
                float y = col;

                if (maplist[row + 15 * col] == tilestate.block)
                {
                    GameObject block_tile = Instantiate(referenece_block, new Vector3(x, y, y), Quaternion.identity);
                    object_list[row, col] = block_tile;
                }
                else if (maplist[row + 15 * col] == tilestate.box)
                {
                    GameObject box_tile = Instantiate(referenece_box, new Vector3(x, y, y), Quaternion.identity);
                    object_list[row, col] = box_tile;
                }
            }
        }
        return object_list;
    }

    private void Generate_border(Grid gamegrid) 
    {
        for(int row = -1; row <= gamegrid.rows; row++)
        {
            GameObject transparent_wall = Instantiate(reference_border, new Vector3(row, -1, -1), Quaternion.identity);
            GameObject tp_wall = Instantiate(reference_border, new Vector3(row, gamegrid.cols, gamegrid.cols), Quaternion.identity);
        }

        for(int col = 0; col < gamegrid.cols; col++)
        {
            GameObject transparent_wall = Instantiate(reference_border, new Vector3(-1, col, col), Quaternion.identity);
            GameObject tp_wall = Instantiate(reference_border, new Vector3(gamegrid.rows, col, col), Quaternion.identity);
        }
    }

    public void destroy_tile(Coordinate coord) => destroy_tile(coord.X, coord.Y);
    public void destroy_tile(int x, int y)
    {
        bool is_item = gameGrid.tileset[x, y] == tilestate.item;
        gameGrid.tileset[x, y] = tilestate.empty;
        Destroy(Object_List[x, y]);

        if (!is_item)
        {
            Coordinate tile = new Coordinate(x, y);
            int rand = Random.Range(0, 10);
            if (rand < 3)
            {
                itemSpawn(tile);
            }
        }
    }


    public void move_box(int x_origin, int y_origin, int x_dest, int y_dest)
    {
        Coordinate destCoord = new Coordinate(x_dest, y_dest);
        if (!gameGrid.is_reachable(destCoord) || destCoord == character_1.characterPos || destCoord == character_2.characterPos) return;
        if (gameGrid.tileset[x_dest, y_dest] == tilestate.item)
        {
            destroy_tile(x_dest, y_dest);
        }
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
    public void itemSpawn(Coordinate itemdest)
    {
        int item_kind = Random.Range(0, 3);
        gameGrid.tileset[itemdest.X, itemdest.Y] = tilestate.item;
        Vector3 item_v3 = gameGrid.grid_to_unity(itemdest);

        switch (item_kind)
        {
            case 0: 
                GameObject itemtile_bubble = Instantiate(referenece_bubble, item_v3, Quaternion.identity);
                Object_List[itemdest.X, itemdest.Y] = itemtile_bubble;
                break;

            case 1:
                GameObject itemtile_potion = Instantiate(referenece_potion, item_v3, Quaternion.identity);
                Object_List[itemdest.X, itemdest.Y] = itemtile_potion;
                break;

            case 2:
                GameObject itemtile_skate = Instantiate(referenece_skate, item_v3, Quaternion.identity);
                Object_List[itemdest.X, itemdest.Y] = itemtile_skate;
                break;
        }
    }

    public void item_randspawn()
    {
        int item_kind = Random.Range(0, 8);
        List<Coordinate> available = new List<Coordinate>();
        for(int i=0; i< gameGrid.rows; i++)
        {
            for(int j=0; j<gameGrid.cols; j++)
            {
                if (gameGrid.is_empty(new Coordinate(i, j)))
                {
                    available.Add(new Coordinate(i, j));
                }
            }
        }
        if (available.Count != 0)
        {
            itemSpawn(available[Random.Range(0, available.Count)]);
        }
    }

    //정해진 타이밍에 스폰
    private IEnumerator itemTimer()
    {
        if (game_is_end) yield break;
        while (game_is_pause)
        {
            yield return null; // 1frame
        }
        float timerTime = 10f;
        float curTime = 0f;
        while (curTime < timerTime)
        {
            if (game_is_end) yield break;
            curTime += Time.deltaTime;
            yield return null;
        }
        item_randspawn();
        StartCoroutine(itemTimer());
    }
}
