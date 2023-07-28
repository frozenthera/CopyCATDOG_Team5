using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class UI : MonoBehaviour
{
    private AudioSource UI_AudioSource;
    [SerializeField]
    private List<Image> maps;
    [SerializeField]
    private AudioClip[] audioClip;
    [SerializeField]
    private List<Image> character1_select_image;
    [SerializeField]
    private List<Image> character2_select_image;
    [SerializeField]
    private Button startbutton, left_check, right_check, Minigame_button;
    [SerializeField]
    private GameObject fixed_1p, fixed_2p;
    [SerializeField]
    private List<GameObject> character1_stats_image;
    [SerializeField]
    private List<GameObject> character2_stats_image;

    private MainmenuData mainmenuData;

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
        UI_AudioSource = GetComponent<AudioSource>();
        startbutton.gameObject.SetActive(false);
        mainmenuData = new MainmenuData() { map_select = 0, player1_select = 0, player2_select = 0, player1_shift = false, player2_shift = false };
        Update_mapimages();
        Update_character_selcet();

        startbutton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Main");
        });

        Minigame_button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Main_chaehun");
        });

        left_check.onClick.AddListener(() =>
        {
            left_arrow();
        });

        right_check.onClick.AddListener(() =>
        {
            right_arrow();
        });
    }

    public void left_arrow()
    {
        UI_AudioSource.clip = audioClip[1];
        UI_AudioSource.Play();

        if (mainmenuData.map_select != 0)
        {
            mainmenuData.map_select--;
        }
        else
            mainmenuData.map_select = 2;

        Update_mapimages();
    }

    public void right_arrow()
    {
        UI_AudioSource.clip = audioClip[1];
        UI_AudioSource.Play();

        if (mainmenuData.map_select != 2)
        {
            mainmenuData.map_select++;
        }
        else
            mainmenuData.map_select = 0;

        Update_mapimages();
    }

    private void Update_mapimages() 
    {
        int m = mainmenuData.map_select;

        for(int i = 0; i < 3; i++)
        {
            if(i == m)
                maps[i].gameObject.SetActive(true);
            else
                maps[i].gameObject.SetActive(false);
        }

    }
    private void Update_character_selcet()
    {
        int c1 = mainmenuData.player1_select;
        int c2 = mainmenuData.player2_select;

        for (int i = 0; i < 3; i++)
        {
            if (i == c1)
            {
                character1_select_image[i].gameObject.SetActive(true);
                character1_stats_image[i].gameObject.SetActive(true);
            }
            else
            {
                character1_select_image[i].gameObject.SetActive(false);
                character1_stats_image[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (i == c2)
            {
                character2_select_image[i].gameObject.SetActive(true);
                character2_stats_image[i].gameObject.SetActive(true);
            }
            else
            {
                character2_select_image[i].gameObject.SetActive(false);
                character2_stats_image[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (mainmenuData.player2_shift == false)
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                if (mainmenuData.player2_select != 0)
                {
                    mainmenuData.player2_select--;
                }
                else
                    mainmenuData.player2_select = 2;

                Update_character_selcet();
            }

            if (Input.GetKeyUp(KeyCode.D))
            {
                if (mainmenuData.player2_select != 2)
                {
                    mainmenuData.player2_select++;
                }
                else
                    mainmenuData.player2_select = 0;

                Update_character_selcet();
            }
        }

        if (mainmenuData.player1_shift == false)
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                if (mainmenuData.player1_select != 0)
                {
                    mainmenuData.player1_select--;
                }
                else
                    mainmenuData.player1_select = 2;

                Update_character_selcet();
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                if (mainmenuData.player1_select != 2)
                {
                    mainmenuData.player1_select++;
                }
                else
                    mainmenuData.player1_select = 0;

                Update_character_selcet();

            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (mainmenuData.player2_shift == false)
            {
                mainmenuData.player2_shift = true;

                UI_AudioSource.clip = audioClip[0];

                fixed_1p.gameObject.SetActive(true);
                UI_AudioSource.Play();

                GameManager.Instance.player2Select = mainmenuData.player2_select;
            }
            else
            {
                mainmenuData.player2_shift = false;
                fixed_1p.gameObject.SetActive(false);
                startbutton.gameObject.SetActive(false);
            }

        }

        if (Input.GetKeyUp(KeyCode.RightShift))
        {
            if (mainmenuData.player1_shift == false)
            {
                mainmenuData.player1_shift = true;

                UI_AudioSource.clip = audioClip[0];

                fixed_2p.gameObject.SetActive(true);
                UI_AudioSource.Play();

                GameManager.Instance.player1Select = mainmenuData.player1_select;
            }
            else
            {
                mainmenuData.player1_shift = false;
                fixed_2p.gameObject.SetActive(false);
                startbutton.gameObject.SetActive(false);
            }
                
        }

        if(mainmenuData.player2_shift == true && mainmenuData.player1_shift == true)
        {
            startbutton.gameObject.SetActive(true);
        }
    }
}

public class MainmenuData
{
    public int map_select;
    public int player1_select;
    public int player2_select;
    public bool player1_shift;
    public bool player2_shift;
}
