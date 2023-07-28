using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class minigame_script : MonoBehaviour
{
    [SerializeField]
    private GameObject waterBalloonprefab;
    private IEnumerator coroutine;

    [SerializeField]
    Text time_text;

    float timecount = 0;
    int Min;
    // Start is called before the first frame update
    void Start()
    {
        print("Mini Game Start !!!!!!!!!!!!");
        coroutine = WaitAndballon(0.8f);
        StartCoroutine(coroutine);
    }

    private GameObject newball;
    // Update is called once per frame
    void Update()
    {
        timecount += Time.deltaTime;
        Clock();
    }

    void Clock()
    {
        time_text.text = string.Format("{0:D2}:{1:D2}",Min,(int)timecount);

        if((int)timecount > 59)
        {
            timecount = 0;
            Min++;
        }
    }

    private IEnumerator WaitAndballon(float waitTime)
    {
        while (true)
        {
<<<<<<< Updated upstream
=======
            yield return new WaitForSeconds(waitTime);
>>>>>>> Stashed changes
            create_ballon();
            Debug.Log("Time = " +Time.time);
            yield return new WaitForSeconds(waitTime);
        }
    }

    void create_ballon()
    {
        int rx = (int)Random.Range(1f, 14f);
        int ry = (int)Random.Range(1f, 12f);
        newball = Instantiate(waterBalloonprefab, new Vector3(rx, ry, ry), Quaternion.identity);
        GameManager.Instance.gameGrid.tileset[rx, ry] = tilestate.ballon;
        newball.GetComponent<Water_delete>().range = 10;
    }
}
