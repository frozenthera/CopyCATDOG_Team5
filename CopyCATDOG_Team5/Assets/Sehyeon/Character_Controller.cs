using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3 (0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey (KeyCode.UpArrow))
        {
            transform.Translate(0, 0.01f, 0);
        }
        else if(Input.GetKey (KeyCode.DownArrow))
        {
            transform.Translate(0, -0.01f, 0);
        }
        else if(Input.GetKey (KeyCode.LeftArrow)) 
        {
            transform.Translate(-0.01f, 0, 0);
        }
        else if(Input.GetKey (KeyCode.RightArrow))
        {
            transform.Translate(0.01f, 0, 0);
        }
    }
}
