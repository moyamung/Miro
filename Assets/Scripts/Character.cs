using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected List<GameObject> map = new List<GameObject>();
    public GameObject gameManager;
    protected int x, y, mapWidth, mapHeight;

    public void init()
    {
        (mapWidth, mapHeight, map) = gameManager.GetComponent<MapMaking>().MapData();
        while (true)
        {
            x = Random.Range(0, mapWidth);
            y = Random.Range(0, mapHeight);
            if (map[y * mapWidth + x].activeInHierarchy)
            {
                break;
            }
        }
        this.transform.position = new Vector3(x, y, -1);
    }

    protected virtual void Move(int dx, int dy)
    {
        if (x + dx < 0 || x + dx >= mapWidth) return;
        if (y + dy < 0 || y + dy >= mapHeight) return;
        if (map[(y + dy) * mapWidth + x + dx].activeInHierarchy)
        {
            x += dx;
            y += dy;
            this.transform.position = new Vector3(x, y, -1);
        }
    }

    public (int, int) GetPlayerPos()
    {
        return (x, y);
    }

    public void SetMap(List<GameObject> _map)
    {
        map = _map;
    }
}
