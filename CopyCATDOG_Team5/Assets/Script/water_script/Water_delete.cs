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
    [SerializeField]
    private GameObject waveprefab;

    [HideInInspector]
    public int water_owner=0;

    [HideInInspector]
    public int range = 2;

    private GameObject character_obj;

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
            GameManager.Instance.gameGrid.tileset[rx, ry] = tilestate.empty;

            if (water_owner == 1)
            {
                character_obj = GameObject.Find("Character1");
                //character_obj.GetComponent<Character_Controller>().maxInstall_First++;
            }
            if (water_owner == 2)
            {
                character_obj = GameObject.Find("Character2");
                //character_obj.GetComponent<Character_Controller>().maxInstall_second++;
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

                GameObject newwave = Instantiate(waveprefab, new Vector2(rx, ry + i), Quaternion.Euler(0,0,90));
                Destroy(newwave, 0.3f);

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

                GameObject newwave = Instantiate(waveprefab, new Vector2(rx, ry - i), Quaternion.Euler(0, 0, 90));
                Destroy(newwave, 0.3f);

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

                GameObject newwave = Instantiate(waveprefab, new Vector2(rx + i, ry), Quaternion.identity);
                Destroy(newwave, 0.3f);

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

                GameObject newwave = Instantiate(waveprefab, new Vector2(rx - i, ry), Quaternion.identity);
                Destroy(newwave, 0.3f);
            }
        }       
    }
}
