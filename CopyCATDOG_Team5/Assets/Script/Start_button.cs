using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_button : MonoBehaviour
{
    public void StartButton()
    {
        Debug.Log("!");
        SceneManager.LoadScene("Sehyeon_Scene");
    }
}
