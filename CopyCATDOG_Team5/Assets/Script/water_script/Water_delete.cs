using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UIElements;
using System;

public class Water_delete : MonoBehaviour
{
    float timecount = 0;
    Vector2 pos;
    [SerializeField]
    private GameObject waveprefab_1;
    [SerializeField]
    private GameObject waveprefab_2;
    [SerializeField]
    private GameObject waveprefab_3;

    [HideInInspector]
    public bool First_owner;

    [HideInInspector]
    public int range;

    private GameObject character_obj;
    int rx, ry;

    void Start()
    {
        Coordinate gridtile = new Coordinate((int)this.transform.position.x, (int)(this.transform.position.y));
        pos = GameManager.Instance.gameGrid.grid_to_unity(gridtile);
        rx = (int)pos.x;
        ry = (int)pos.y;
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
            GameManager.Instance.gameGrid.tileset[rx, ry] = tilestate.empty;
            GameObject original_wave = Instantiate(waveprefab_3, new Vector3(rx, ry, ry), Quaternion.identity);
            Destroy(original_wave, 0.3f);

            if (First_owner == true)
            {
                GameManager.Instance.character_1.maxInstall++;
            }
            if (First_owner == false)
            {
                GameManager.Instance.character_2.maxInstall++;
            }

            for (int i = 1; i < GameManager.Instance.gameGrid.cols - ry; i++)
            {
                if (i > range)
                    break;

                if (GameManager.Instance.gameGrid.tileset[rx, ry + i] == tilestate.wall)
                {
                    break;
                }

                if (GameManager.Instance.gameGrid.tileset[rx, ry + i] == tilestate.box || GameManager.Instance.gameGrid.tileset[rx, ry + i] == tilestate.block)
                {
                    GameManager.Instance.destroy_tile(rx, ry + i);
                    break;
                }

                GameObject newwave = Instantiate(waveprefab_1, new Vector3(rx, ry + i, ry+ i), Quaternion.Euler(0,0,90));
                Destroy(newwave, 0.3f);

                if (rx == GameManager.Instance.character_1.characterPos.X && ry+i == GameManager.Instance.character_1.characterPos.Y)
                {
                    GameManager.Instance.character_1.GetHittedByWater();
                }

                if (rx == GameManager.Instance.character_2.characterPos.X && ry == GameManager.Instance.character_2.characterPos.Y)
                {
                    GameManager.Instance.character_2.GetHittedByWater();
                }

            }

            for (int i = 1; i < ry + 1; i++)
            {
                if (i > range)
                    break;

                if (GameManager.Instance.gameGrid.tileset[rx, ry - i] == tilestate.wall)
                {
                    break;
                }

                if (GameManager.Instance.gameGrid.tileset[rx, ry - i] == tilestate.box || GameManager.Instance.gameGrid.tileset[rx, ry - i] == tilestate.block)
                {
                    GameManager.Instance.destroy_tile(rx, ry - i);
                    break;
                }

                GameObject newwave = Instantiate(waveprefab_2, new Vector3(rx, ry - i, ry - i), Quaternion.Euler(0, 0, 90));
                Destroy(newwave, 0.3f);

                if (rx == GameManager.Instance.character_1.characterPos.X && ry-i == GameManager.Instance.character_1.characterPos.Y)
                {
                    GameManager.Instance.character_1.GetHittedByWater();
                }

                if (rx == GameManager.Instance.character_2.characterPos.X && ry-i == GameManager.Instance.character_2.characterPos.Y)
                {
                    GameManager.Instance.character_2.GetHittedByWater();
                }

            }

            for (int i = 1; i < GameManager.Instance.gameGrid.rows - rx; i++)
            {
                if (i > range)
                    break;

                if (GameManager.Instance.gameGrid.tileset[rx + i, ry] == tilestate.wall)
                {
                    break;
                }

                if (GameManager.Instance.gameGrid.tileset[rx + i, ry] == tilestate.box || GameManager.Instance.gameGrid.tileset[rx + i, ry] == tilestate.block)
                {
                    GameManager.Instance.destroy_tile(rx + i, ry);
                    break;
                }

                GameObject newwave = Instantiate(waveprefab_1, new Vector3(rx + i, ry, ry), Quaternion.identity);
                Destroy(newwave, 0.3f);

                if (rx+i == GameManager.Instance.character_1.characterPos.X && ry == GameManager.Instance.character_1.characterPos.Y)
                {
                    GameManager.Instance.character_1.GetHittedByWater();
                }

                if (rx+i == GameManager.Instance.character_2.characterPos.X && ry == GameManager.Instance.character_2.characterPos.Y)
                {
                    GameManager.Instance.character_2.GetHittedByWater();
                }

            }

            for (int i = 1; i < rx + 1; i++)
            {
                if (i > range)
                    break;

                if (GameManager.Instance.gameGrid.tileset[rx - i, ry] == tilestate.wall)
                {
                    break;
                }

                if (GameManager.Instance.gameGrid.tileset[rx - i, ry] == tilestate.box || GameManager.Instance.gameGrid.tileset[rx - i, ry] == tilestate.block)
                {
                    GameManager.Instance.destroy_tile(rx - i, ry);
                    break;
                }

                GameObject newwave = Instantiate(waveprefab_2, new Vector3(rx - i, ry, ry), Quaternion.identity);
                Destroy(newwave, 0.3f);

                if (rx-i == GameManager.Instance.character_1.characterPos.X && ry == GameManager.Instance.character_1.characterPos.Y)
                {
                    GameManager.Instance.character_1.GetHittedByWater();
                }

                if (rx-i == GameManager.Instance.character_2.characterPos.X && ry == GameManager.Instance.character_2.characterPos.Y)
                {
                    GameManager.Instance.character_2.GetHittedByWater();
                }
            }
        }       
    }
}
