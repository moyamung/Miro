using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Character
{
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void init(PlayerController _player)
    {
        base.init();
        player = _player;
        InvokeRepeating("Follow", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        int px, py;
        (px, py) = player.GetPlayerPos();
        if (x == px && y == py)
        {
            SceneManager.LoadScene("BadEnd");
            //Debug.Log("You dead");
        }
    }

    void Follow()
    {
        int px, py;
        (px, py) = player.GetPlayerPos();
        int tryNum = 0, ranX, ranY;
        int[] xPick = { 0, Dir(x, px) }, yPick = { 0, Dir(y, py) };
        do
        {
            ranX = xPick[Random.Range(0, 2)];
            ranY = yPick[Random.Range(0, 2)];
            tryNum++;
        }
        while (tryNum < 5 && ranX == 0 && ranY == 0);
        Move(ranX, ranY);
    }

    int Dir(int a, int b)
    {
        if (a < b) return 1;
        else if (a > b) return -1;
        else return 0;
    }

}
