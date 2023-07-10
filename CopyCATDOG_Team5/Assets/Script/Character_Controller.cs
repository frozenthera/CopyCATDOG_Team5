using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    public int speed;
    private Rigidbody2D rigidbody;
    private Vector2 vector;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3 (0, 0, 0);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    int temp;
    int active;
    void Update()
    {
        // 동시입력관리
        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            temp = 0;
            active = 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            temp = 1;
            active = 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            temp = 2;
            active = 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            temp = 3;
            active = 1;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) && active == 1)
        {
            rigidbody.velocity = vector * speed;
            switch (temp)
            {
                case 0:
                    vector.y = 1;
                    vector.x = 0;
                    break;
                case 1:
                    vector.y = -1;
                    vector.x = 0;
                    break;
                case 2:
                    vector.x = -1;
                    vector.y = 0;
                    break;
                case 3:
                    vector.x = 1;
                    vector.y = 0;
                    break;
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && temp == 0)
        {
            active = 0;
            rigidbody.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && temp == 1)
        {
            active = 0;
            rigidbody.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && temp == 2)
        {
            active = 0;
            rigidbody.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && temp == 3)
        {
            active = 0;
            rigidbody.velocity = new Vector2(0, 0);
        }
    }
}
