using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Character_Controller : MonoBehaviour
{
    public GameObject boxprefab;

    public bool FirstCharacter;
    public Vector3 StartPosition;

    public int speed;
    private Rigidbody2D rigidbody;
    private Vector2 vector;

    public Coordinate characterPos;

    public int range = 2;
    public int maxInstall = 2;

    private KeyCode myKey1, myKey2, myKey3, myKey4;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = StartPosition;
        rigidbody = GetComponent<Rigidbody2D>();
        if(FirstCharacter == true)
        {
            myKey1 = KeyCode.UpArrow;
            myKey2 = KeyCode.DownArrow;
            myKey3 = KeyCode.LeftArrow;
            myKey4 = KeyCode.RightArrow;
        }
        else
        {
            myKey1 = KeyCode.W;
            myKey2 = KeyCode.S;
            myKey3 = KeyCode.A;
            myKey4 = KeyCode.D;
        }

        timer = 0f;
    }
    // Update is called once per frame
    int temp;
    int active;
    void Update()
    {
        // �����Է°���
        if(Input.GetKeyDown(myKey1))
        {
            temp = 0;
            active = 1;
        }
        if (Input.GetKeyDown(myKey2))
        {
            temp = 1;
            active = 1;
        }
        if (Input.GetKeyDown(myKey3))
        {
            temp = 2;
            active = 1;
        }
        if (Input.GetKeyDown(myKey4))
        {
            temp = 3;
            active = 1;
        }
        if (Input.GetKey(myKey1) || Input.GetKey(myKey2) || Input.GetKey(myKey3) || Input.GetKey(myKey4) && active == 1)
        {
            rigidbody.velocity = vector * speed;
            switch (temp)
            {
                case 0:
                    vector = Vector2.up;
                    break;
                case 1:
                    vector = Vector2.down;
                    break;
                case 2:
                    vector = Vector2.left;
                    break;
                case 3:
                    vector = Vector2.right;
                    break;
            }
        }
        if (Input.GetKeyUp(myKey1) && temp == 0)
        {
            active = 0;
            rigidbody.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(myKey2) && temp == 1)
        {
            active = 0;
            rigidbody.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(myKey3) && temp == 2)
        {
            active = 0;
            rigidbody.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(myKey4) && temp == 3)
        {
            active = 0;
            rigidbody.velocity = new Vector2(0, 0);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            create_water_left();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            create_water_right();
        }
    }

    private int direction;
    float timer;

    //���� �ð� �̻�(�뷫 1����) �о�� �����̰� �ϱ�
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Box")
        {
            return;
        }

        //������Ʈ�� �÷��̾��� ���� ���ϱ�
        Vector2 Target = transform.position - collision.gameObject.transform.position;
        float angle = Vector2.SignedAngle(Vector2.left, Target);
        direction = 4;
        if (angle >= -10f && angle <= 10f)
        {
            //������
            direction = 3;
        }
        else if (angle >= 80f && angle <= 100f)
        {
            //����
            direction = 0;
        }
        else if (angle >= 170f || angle <= -170f)
        {
            //����
            direction = 2;
        }
        else if (angle <= -80f && angle >= -100f)
        {
            //�Ʒ���
            direction = 1;
        }

        //Box �������� �о����� �׽�Ʈ
        if (direction == temp && active == 1)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0f;
        }
        if(timer > 0.3f)
        {
            Vector2 target = collision.gameObject.transform.position;
            switch (temp)
            {
                case 0:
                    vector = Vector2.up;
                    break;
                case 1:
                    vector = Vector2.down;
                    break;
                case 2:
                    vector = Vector2.left;
                    break;
                case 3:
                    vector = Vector2.right;
                    break;
            }
            int x_dest = (int)target.x + (int)vector.x;
            int y_dest = (int)target.y + (int)vector.y;
            if (GameManager.Instance.gameGrid.tileset[x_dest, y_dest] == tilestate.empty)
            {
                GameManager.Instance.move_box((int)target.x, (int)target.y, x_dest, y_dest);
                print(target);
                print(vector);
                // collision.gameObject.transform.Translate(vector);
                timer = 0f;
            }
        }
    }

    //�÷��̾ ���ٱ⿡ �¾��� ���� ���� ����
    public void GetHittedByWater()
    { 

    }

    //ĳ������ ���� ��ġ�� ��ǳ�� ��ġ
    void create_water_left()
    {
        GameObject newball = Instantiate(boxprefab, new Vector2(1, 0), Quaternion.identity);   // + �� �÷��̾� ��ǥ �Ҵ�
    }

    void create_water_right()
    {
        GameObject newball = Instantiate(boxprefab, new Vector2(2, 0), Quaternion.identity);   // + �� �÷��̾� ��ǥ �Ҵ�
    }
}
