using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting_UI : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private GameObject gamerule_obj;

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
}
