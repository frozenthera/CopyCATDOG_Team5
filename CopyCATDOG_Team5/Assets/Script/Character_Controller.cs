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
using System.Net;

public class Character_Controller : MonoBehaviour
{
    private AudioSource charater_audioSource;
    [SerializeField]
    private GameObject waterBalloonprefab, GameOver_panel;
    [SerializeField]
    private AudioClip[] audioClips;
    public bool FirstCharacter;
    public Vector3 StartPosition;
    //물풍선
    private Vector2 unity_pos;
    private int rx, ry;
    public bool WaterBallonDeployed;

    public int speed;
    private Rigidbody2D rb;
    private Vector2 vector;

    public Coordinate characterPos;
    
    public int range_level, speed_level;
    private int range;
    [HideInInspector]
    public int maxInstall;


    //능력치 증가 함수

    public void addrange()
    {
        range_level += 1;
        range_apply(range_level);
    }

    public void addspeed()
    {
        speed += 2;
        speed_apply(speed_level);
    }

    public void addmaxinstall()
    {
        maxInstall += 1;
        maxInstall = Mathf.Max(maxInstall, 5); 
    }

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

    private KeyCode myKey1, myKey2, myKey3, myKey4, myKey_n;    
    // Start is called before the first frame update
    void Start()
    {
        charater_audioSource = GetComponent<AudioSource>();
        WaterBallonDeployed = false;
        gameObject.layer = 6;
        getHit = false;
        rb = GetComponent<Rigidbody2D>();
        timer = 0f;
    }
    public void StartCharacter()
    {
        //gameOver = false;
        Anim = transform.GetChild(0).GetComponent<Animator>();
        Anim.speed = 0f;
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

    public bool needlecount = true;

    Animator Anim;
    void Update()
    {
        if (GameManager.Instance.game_is_pause == true) return;

        Coordinate nextCoord = GameManager.Instance.gameGrid.unity_to_grid(transform.position);
        if (characterPos != nextCoord)
        {
            if (GameManager.Instance.gameGrid.is_reachable(nextCoord) && GameManager.Instance.gameGrid.tileset[nextCoord.X, nextCoord.Y] == tilestate.item)
            {
                switch (GameManager.Instance.Object_List[nextCoord.X, nextCoord.Y].GetComponent<Item>().itemname)
                {
                    case itemEnum.bubble:
                        addmaxinstall();
                        audio_play(1);
                        break;

                    case itemEnum.potion:
                        addrange();
                        audio_play(1);
                        break;

                    case itemEnum.roller:
                        addspeed();
                        audio_play(1);
                        break;
                }

                GameManager.Instance.destroy_tile(nextCoord);
            }
        }

        characterPos = nextCoord;

        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            if (FirstCharacter && needlecount && getHit)
            {
                getHit = false;
                dying = false;
                gameObject.layer = 8;
                speed = speed_save;
                Anim.SetTrigger("live");
                Anim.speed = 1f;
                needlecount = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!FirstCharacter && needlecount && getHit)
            {
                getHit = false;
                dying = false;
                gameObject.layer = 8;
                speed = speed_save;
                Anim.SetTrigger("live");
                Anim.speed = 1f;
                needlecount = false;
            }
        }

        //물폭탄 맞았을 때
        if (dying == true && Anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1f)
        {
            if(Anim.GetCurrentAnimatorStateInfo(0).IsName("die1") || Anim.GetCurrentAnimatorStateInfo(0).IsName("die2"))
            {
                Anim.speed = 0f;
                dying = false;
            }
        }
        if (gameOver == true)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        //물풍선 관리
        if (WaterBallonDeployed == true)
        {
            float x = transform.position.x, y = transform.position.y;
            if (x >= rx + 1 || x <= rx - 1 || y >= ry + 1 || y <= ry - 1)
            {
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), newball.GetComponent<BoxCollider2D>(), false);
            }
        }
        characterPos = GameManager.Instance.gameGrid.unity_to_grid(transform.position);
        transform.position = new Vector3(transform.position.x, transform.position.y, characterPos.Y);
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
            SpriteRenderer characterSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
            rb.velocity = vector * speed;


            //애니메이션
            if (getHit == false)
            {
                Anim.speed = 1f;
                switch (temp)
                {
                    case 0:
                        Anim.SetTrigger("up");
                        break;
                    case 1:
                        Anim.SetTrigger("down");
                        break;
                    case 2:
                        Anim.SetTrigger("moving_x");
                        break;
                    case 3:
                        Anim.SetTrigger("moving_x");
                        break;
                }
            }
            switch (temp)
            {
                case 0:
                    vector = Vector2.up;
                    break;
                case 1:
                    vector = Vector2.down;
                    break;
                case 2:
                    characterSprite.flipX = false;
                    vector = Vector2.left;
                    break;
                case 3:
                    characterSprite.flipX = true;
                    vector = Vector2.right;
                    break;
            }
        }
        if (Input.GetKeyUp(myKey1) && temp == 0)
        {
            Anim.speed = 0f;
            active = 0;
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(myKey2) && temp == 1)
        {
            Anim.speed = 0f;
            active = 0;
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(myKey3) && temp == 2)
        {
            Anim.speed = 0f;
            active = 0;
            rb.velocity = new Vector2(0, 0);
        }
        if (Input.GetKeyUp(myKey4) && temp == 3)
        {
            Anim.speed = 0f;
            active = 0;
            rb.velocity = new Vector2(0, 0);
        }

        if (FirstCharacter)
        {
            if (maxInstall > 0)
            {
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    create_ballon();
                    maxInstall--;
                }
            }
        }
        else
        {
            if (maxInstall > 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    create_ballon();
                    maxInstall--;
                }
            }
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

    public bool getHit, dying, gameOver;
    public int speed_save;
    //플레이어가 물줄기에 맞았을 때의 동작 구현
    public Sprite hitted_sprite;
    public void GetHittedByWater()
    {
        SpriteRenderer characterSprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        characterSprite.sprite = hitted_sprite;
        Anim.speed = 0.1f;
        Anim.SetTrigger("die");
        getHit = true;
        dying = true;
        gameObject.layer = 9;
        speed_save = speed;
        speed = 1;
        StartCoroutine(WaitForRescue());
    }

    public void GameOver()
    {
        StartCoroutine(ReturntoUI());
        Anim.speed = 1f;
        gameOver = true;
    }

    private IEnumerator ReturntoUI()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Lobby");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(getHit == true && collision.gameObject.layer == 7)
        {
            GameOver();
        }
    }

    IEnumerator WaitForRescue()
    {
        yield return new WaitForSeconds(3.0f);
        if(getHit == true)
        {
            audio_play(0);
            print("die!");
            GameOver();
            GameOver_panel.SetActive(true);
            GameManager.Instance.game_is_over = true;
        }
        else
        {
            print("live!");
        }
    }

    void audio_play(int x)
    {
        charater_audioSource.clip = audioClips[x];
        charater_audioSource.Play();
    }

    private GameObject newball;
    //캐릭터의 현재 위치에 물풍선 설치
    void create_ballon()
    {
        if (!GameManager.Instance.gameGrid.is_empty(characterPos)) return;

        unity_pos = GameManager.Instance.gameGrid.grid_to_unity(characterPos);
        rx = (int)unity_pos.x;
        ry = (int)unity_pos.y;
        Debug.Log(rx + " " + ry);
        //테스트용으로 z값 -5로 바꿔놓음
        newball = Instantiate(waterBalloonprefab, new Vector3(rx, ry, ry), Quaternion.identity);
        GameManager.Instance.gameGrid.tileset[characterPos.X, characterPos.Y] = tilestate.ballon;
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), newball.GetComponent<BoxCollider2D>());
        WaterBallonDeployed = true;
        if(FirstCharacter == true)
            newball.GetComponent<Water_delete>().First_owner = true;
        newball.GetComponent<Water_delete>().range = range;
    }
}
