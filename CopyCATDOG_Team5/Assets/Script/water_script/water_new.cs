using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_new : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject boxprefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            create_water();
        }
    }

    void create_water()
    {
        GameObject newball = Instantiate(boxprefab, new Vector2(0, 0), Quaternion.identity);   // + 플레이어 좌표 할당
    }
}
