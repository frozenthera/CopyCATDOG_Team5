using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using static UnityEditor.PlayerSettings;
using UnityEngine.SceneManagement;

public class Character_Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject waterBalloonprefab, GameOver_panel;
    public bool FirstCharacter;
    public Vector3 StartPosition;
    private Vector2 unity_pos;
    private int rx, ry;

    public int speed;
    private Rigidbody2D rb;
    private Vector2 vector;

    public Coordinate characterPos;
    
    public int range_level, speed_level;
    private int range;
    [HideInInspector]
    public int maxInstall;

    private void range_apply(int temp)
    {
        //임시수치
        if(temp > 5)
        {
            temp = 5;
        }
        range = temp * 2;
    }

    private void speed_apply(int temp)
    {
        //임시수치
        if (temp > 5)
        {
            temp = 5;
        }
        speed = temp * 3;
    }

    private KeyCode myKey1, myKey2, myKey3, myKey4;    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = 6;
        getHit = false;
        rb = GetComponent<Rigidbody2D>();
        timer = 0f;
    }
    public void StartCharacter()
    {
        transform.position = StartPosition;
        if (FirstCharacter == true)
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
        speed_apply(speed_level);
        range_apply(range_level);
    }
    // Update is called once per frame
    int temp;
    int active;
    void Update()
    {
        characterPos.X = (int)Math.Round(transform.position.x);
        characterPos.Y = (int)Math.Round(transform.position.y);
        // 동시입력관리
        if (Input.GetKeyDown(myKey1))
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
            rb.velocity = vector * speed;
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
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(myKey2) && temp == 1)
        {
            active = 0;
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(myKey3) && temp == 2)
        {
            active = 0;
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(myKey4) && temp == 3)
        {
            active = 0;
            rb.velocity = new Vector2(0, 0);
        }

        //Test key
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetHittedByWater();
        }

        if (FirstCharacter)
        {
            if (maxInstall > 0)
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    create_ballon();
                    maxInstall--;
                }
        }
        else
            if (maxInstall> 0)
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    create_ballon();
                    maxInstall--;
                }
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
        gameObject.layer = 9;
        speed = 1;
        StartCoroutine(WaitForRescue());
    }

    public void GameOver()
    {
        StartCoroutine(ReturntoUI());
        GameOver_panel.SetActive(true);
    }

    private IEnumerator ReturntoUI()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("UI");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(getHit == true && collision.gameObject.layer == 8)
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
        else
        {
            getHit = false;
            gameObject.layer = 8;
            speed = 5;
        }
    }
 

    //캐릭터의 현재 위치에 물풍선 설치
    void create_ballon()
    {
        unity_pos = GameManager.Instance.gameGrid.grid_to_unity(characterPos);
        rx = (int)unity_pos.x;
        ry = (int)unity_pos.y;

        GameObject newball = Instantiate(waterBalloonprefab, new Vector3(rx, ry, ry), Quaternion.identity);
        GameManager.Instance.gameGrid.tileset[characterPos.X, characterPos.Y] = tilestate.ballon;

        if(FirstCharacter == true)
            newball.GetComponent<Water_delete>().First_owner = true;

        newball.GetComponent<Water_delete>().range = range;
    }
}
