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
    needle,
}


