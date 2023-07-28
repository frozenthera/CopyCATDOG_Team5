using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class animal_move : MonoBehaviour
{
    public Vector3 StartPosition;
    Vector3 Target;
    bool target_locked;
    Coordinate now_position;

    public void StartAnimal()
    {
        transform.position = StartPosition;
    }
    // Start is called before the first frame update
    void Start()
    {
        target_locked = false;
        Target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        now_position = GameManager.Instance.gameGrid.unity_to_grid(transform.position);
        if (target_locked)
        {
            print("targetlocked");
            transform.position = Vector3.MoveTowards(transform.position, Target, Time.deltaTime * 2);
        }
        if (transform.position == Target)
        {
            target_locked = false;
            Target = new Vector3(-10, -10, -10);
            StartCoroutine(Wait());
        }
    }

    void AnimalPosition(Coordinate now)
    {
        bool blocked = true;
        bool[] temp = {false, false, false, false};
        int x = now_position.X;
        int y = now_position.Y;
        Coordinate[] coordinates = {new Coordinate(x-1, y), new Coordinate(x, y-1), new Coordinate(x+1, y), new Coordinate(x, y+1)};
;       for(int i = 0; i < 4; i++)
        {
            if (GameManager.Instance.gameGrid.is_reachable(coordinates[i]) && GameManager.Instance.gameGrid.tileset[coordinates[i].X, coordinates[i].Y] == tilestate.empty)
            {
                temp[i] = true;
                blocked = false;
            }
        }
        if(blocked)
        {
            return;
        }
        int a;
        while(true)
        {
            a = Random.Range(0, 4);
            if (temp[a])
            {
                break;
            }
        }
        Target = GameManager.Instance.gameGrid.grid_to_unity(coordinates[a]);
        target_locked = true;
        print(Target);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2.0f);
        AnimalPosition(now_position);
    }
}