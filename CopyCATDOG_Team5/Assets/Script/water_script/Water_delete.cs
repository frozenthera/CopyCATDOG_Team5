using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Water_delete : MonoBehaviour
{
    float timecount = 0;
    bool ones = true;

    void Start()
    {
        Destroy(gameObject, 2.3f); //������ ����Ʈ 0.3��
        
    }

    void Update()
    {
        timecount += Time.deltaTime;

        if (timecount >= 2)
        {
            if (ones)
            {
                Debug.Log("��ǳ�� ����");
                ones = false;

                
            }
            
        }
    }
}
