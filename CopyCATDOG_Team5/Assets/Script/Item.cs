using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public itemEnum itemname;

    public Item(int i_n, Coordinate i_p)
    {
        itemname = itemEnum.bubble + i_n;
    }

}

public enum itemEnum
{
    bubble,
    potion,
    roller,
    turtle,
    p_turtle,
    needle,
    shield,
    xray
}


public class Item_Spawn : MonoBehaviour
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
