using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item
{
    public Coordinate itemPos;

    public itemEnum itemname;

}
public enum itemEnum
{
    bubble,
    potion,
    roller,
    turtle,
    piratet,
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
