using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Item
{
    public Coordinate itemPos;
    public itemEnum itemname;
    private Rigidbody2D itembody;
    public float duration;

    
    public Item(int i_n, Coordinate i_p)
    {
        itemPos = i_p;
        itemname = itemEnum.bubble + i_n;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Destroy(this.gameObject);
            switch (itemname)
            {
                case itemEnum.bubble:
                    if (collision.gameObject == GameManager.Instance.character_1)
                    {
                        GameManager.Instance.character_1.maxInstall += 1;
                    }
                    else
                    {
                        GameManager.Instance.character_2.maxInstall += 1;
                    }
                    break;

                /*case itemEnum.potion:
                    //character controller�� range�� public ������ �Ǿ�� �Ұ� ����
                    if (collision.gameObject == GameManager.Instance.character_1)
                    {
                        GameManager.Instance.character_1.range += 1;
                    }
                    else{
                        GameManager.Instance.character_2.range  += 1;
                    }
                    break;
                */

                case itemEnum.roller:
                    if (collision.gameObject == GameManager.Instance.character_1)
                    {
                        GameManager.Instance.character_1.speed += 1;
                    }
                    else
                    {
                        GameManager.Instance.character_2.speed += 1;
                    }
                    break;

                case itemEnum.needle:
                    //character controller���� needle count�� ����ϰ� ��ǳ���� ���� ���¿��� ��� �� �ֵ��� ����� �־���ҵ�
                    
                    break;
            }
        }
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
