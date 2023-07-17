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
    public GameObject OtherPlayer;


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
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), OtherPlayer.GetComponent<BoxCollider2D>());
        getHit = false;
        speed = 5;
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
        
        // 동시입력관리
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

<<<<<<< HEAD
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            create_water_left();
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            create_water_right();
        }


        //Test key
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetHittedByWater();
        }
=======
        if (this.gameObject.name == "Character1")
            if (Input.GetKeyDown(KeyCode.LeftShift))
                create_water_left();
        if (this.gameObject.name == "Character2")
            if (Input.GetKeyDown(KeyCode.RightShift))
                create_water_right();

>>>>>>> 05921a57e173eabf25eefa9d07af235dd62e87c9
    }

    private int direction;
    float timer;

    //일정 시간 이상(대략 1초쯤) 밀어야 움직이게 하기
    private void OnCollisionStay2D(Collision2D collision)
    {

        if(collision.gameObject.tag != "Box" || getHit == true)
        {
            return;
        }

        //오브젝트와 플레이어의 방향 구하기
        Vector2 Target = transform.position - collision.gameObject.transform.position;
        float angle = Vector2.SignedAngle(Vector2.left, Target);
        direction = 4;
        if (angle >= -10f && angle <= 10f)
        {
            //오른쪽
            direction = 3;
        }
        else if (angle >= 80f && angle <= 100f)
        {
            //위쪽
            direction = 0;
        }
        else if (angle >= 170f || angle <= -170f)
        {
            //왼쪽
            direction = 2;
        }
        else if (angle <= -80f && angle >= -100f)
        {
            //아래쪽
            direction = 1;
        }

        //Box 방향으로 밀었는지 테스트
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


    public bool getHit;
    //플레이어가 물줄기에 맞았을 때의 동작 구현
    public void GetHittedByWater()
    {
        getHit = true;
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), OtherPlayer.GetComponent<BoxCollider2D>(), false);
        speed = 1;
        StartCoroutine(WaitForRescue());
    }

    public void GameOver()
    {
        print("GameOver");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(getHit == true && collision.gameObject.tag == "Player")
        {
            GameOver();
        }
    }

    IEnumerator WaitForRescue()
    {
        print('!');
        yield return new WaitForSeconds(3.0f);
        if(getHit == true)
        {
            GameOver();
        }
    }

    //캐릭터의 현재 위치에 물풍선 설치
    void create_water_left()
    {
        GameObject newball = Instantiate(boxprefab, new Vector2(1, 0), Quaternion.identity);   // + 좌 플레이어 좌표 할당
    }

    void create_water_right()
    {
        GameObject newball = Instantiate(boxprefab, new Vector2(2, 0), Quaternion.identity);   // + 우 플레이어 좌표 할당
    }
}
