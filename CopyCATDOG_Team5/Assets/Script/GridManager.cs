using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Generate_grid(GameManager.Instance.gameGrid);
    }

    public GameObject referenece_empty;
    public GameObject referenece_wall;
    public GameObject referenece_box;
    public GameObject referenece_block;

    private void Generate_grid(Grid gamegrid)
    {
        for (int row = 0; row < gamegrid.rows; row++)
        {
            for (int col = 0; col < gamegrid.cols; col++)
            {
                float x = row;
                float y = col;

                if (gamegrid.tileset[row, col] == tilestate.empty)
                {
                    GameObject empty_tile = Instantiate(referenece_empty, transform);
                    empty_tile.transform.position = new Vector2(x, y);
                }
                else if (gamegrid.tileset[row, col] == tilestate.wall)
                {
                    GameObject wall_tile = Instantiate(referenece_wall, transform);
                    wall_tile.transform.position = new Vector2(x, y);
                }
                else if (gamegrid.tileset[row, col] == tilestate.block)
                {
                    GameObject block_tile = Instantiate(referenece_block, transform);
                    block_tile.transform.position = new Vector2(x, y);
                }
                else if (gamegrid.tileset[row, col] == tilestate.box)
                {
                    GameObject box_tile = Instantiate(referenece_box, transform);
                    box_tile.transform.position = new Vector2(x, y);
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
