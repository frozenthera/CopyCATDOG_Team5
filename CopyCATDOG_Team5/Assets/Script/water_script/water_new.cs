using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_new : MonoBehaviour
{
    [SerializeField]
    private GameObject boxprefab;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            create_water_left();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            create_water_right();
        }
    }

    void create_water_left()
    {
        GameObject newball = Instantiate(boxprefab, new Vector2(1, 0), Quaternion.identity);   // + �� �÷��̾� ��ǥ �Ҵ�
    }

    void create_water_right()
    {
        GameObject newball = Instantiate(boxprefab, new Vector2(2, 0), Quaternion.identity);   // + �� �÷��̾� ��ǥ �Ҵ�
    }
}
