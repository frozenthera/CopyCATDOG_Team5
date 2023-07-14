using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MovingBox : MonoBehaviour
{
    public int x, y;
    GameManager Instance = GameObject.Find("GameObject").GetComponent<GameManager>();
    
    // Start is called before the first frame update
    void Start()
    {
        Instance.gameGrid.tileset[x, y] = tilestate.box;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
