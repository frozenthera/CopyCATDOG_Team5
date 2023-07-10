using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Water_delete : MonoBehaviour
{
    float timecount = 0;
    bool ones = true;
    bool chgrid = true;
    Vector2 pos;



    void Start()
    {
        pos = this.transform.position;
        Destroy(gameObject, 2.3f); //≈Õ¡ˆ¥¬ ¿Ã∆—∆Æ 0.3√ 

    }

    void Update()
    {
        timecount += Time.deltaTime;

        if (timecount >= 2)
        {
            if (ones)
            {
                Debug.Log("π∞«≥º± ≈Õ¡¸");
                ones = false;

                Debug.Log(pos);
                Debug.Log(pos.x);
                Debug.Log(pos.y);



            }
            
        }
    }
}
