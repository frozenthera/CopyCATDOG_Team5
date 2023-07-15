using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UIElements;

public class Water_delete : MonoBehaviour
{
    float timecount = 0;
    Vector2 pos;
    public int range = 0;

    void Start()
    {
        pos = this.transform.position;
    }

    void Update()
    {
        timecount += Time.deltaTime;

        if (timecount >= 2)
        {
            

            int rx = (int)pos.x;
            int ry = (int)pos.y;

            Debug.Log("π∞«≥º± ≈Õ¡¸");
            Debug.Log(pos);

            Destroy(gameObject);

            for (int i = 1; i < GameManager.Instance.gameGrid.cols - ry; i++)
            {

                if(GameManager.Instance.gameGrid.tileset[rx, ry + i] == tilestate.wall)
                {
                    break;
                }

                if (GameManager.Instance.gameGrid.tileset[rx, ry + i] == tilestate.box || GameManager.Instance.gameGrid.tileset[rx, ry + i] == tilestate.block)
                {
                    GameManager.Instance.destroy_tile(rx, ry + i);
                    break;
                }

            }

            for (int i = 1; i < ry + 1; i++)
            {

                if (GameManager.Instance.gameGrid.tileset[rx, ry - i] == tilestate.wall)
                {
                    break;
                }

                if (GameManager.Instance.gameGrid.tileset[rx, ry - i] == tilestate.box || GameManager.Instance.gameGrid.tileset[rx, ry - i] == tilestate.block)
                {
                    GameManager.Instance.destroy_tile(rx, ry - i);
                    break;
                }

            }

            for (int i = 1; i < GameManager.Instance.gameGrid.rows - rx; i++)
            {

                if (GameManager.Instance.gameGrid.tileset[rx + i, ry] == tilestate.wall)
                {
                    break;
                }

                if (GameManager.Instance.gameGrid.tileset[rx + i, ry] == tilestate.box || GameManager.Instance.gameGrid.tileset[rx + i, ry] == tilestate.block)
                {
                    GameManager.Instance.destroy_tile(rx + i, ry);
                    break;
                }

            }

            for (int i = 1; i < rx + 1; i++)
            {

                if (GameManager.Instance.gameGrid.tileset[rx - i, ry] == tilestate.wall)
                {
                    break;
                }

                if (GameManager.Instance.gameGrid.tileset[rx - i, ry] == tilestate.box || GameManager.Instance.gameGrid.tileset[rx - i, ry] == tilestate.block)
                {
                    GameManager.Instance.destroy_tile(rx - i, ry);
                    break;
                }

            }

        }       
    }
}
