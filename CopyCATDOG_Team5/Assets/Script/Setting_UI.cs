using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject gamerule_obj;
    [SerializeField]
    private GameObject pause_UI;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Setactive()
    {
        gamerule_obj.SetActive(true);
    }

    public void close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    private IEnumerator CloseAfterDelay()
    {
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        animator.ResetTrigger("Close");
    }

    public void pause_start()
    {
        Time.timeScale = 0;
        pause_UI.SetActive(true);
        GameManager.Instance.game_is_pause = true;
    }

    public void pause_finish()
    {
        Time.timeScale = 1;
        pause_UI.SetActive(false);
        GameManager.Instance.game_is_pause = false;
    }

    public void go_UI()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Lobby");
        GameManager.Instance.game_is_pause = true;
    }
}
