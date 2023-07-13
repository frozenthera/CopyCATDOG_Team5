using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
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

    public void Start()
    {
        
    }

    public void OnEnable()
    {
        gameGrid = new Grid(4, 5);
        for (int i = 0; i < gameGrid.rows; i++)
        {
            for (int j = 0; j < gameGrid.cols; j++)
            {
                gameGrid.tileset[i, j] = tilestate.block;
            }
        }
        gameGrid.tileset[1, 0] = tilestate.empty;
    }

}
