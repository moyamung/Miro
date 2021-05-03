using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaking : MonoBehaviour
{
    public int maxstage;
    public GameObject cube;
    public int mapWidth, mapHeight;

    public List<GameObject> map = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MapMake()
    {
        MapInit(map, mapWidth, mapHeight);
        MapPartition(map, 0, 0, 0, mapWidth, mapHeight);
    }

    public void Remap(Character c1, Character c2)
    {
        //List<GameObject> newMap= new List<GameObject>();
        int x1, y1, x2, y2;
        (x1, y1) = c1.GetPlayerPos();
        (x2, y2) = c2.GetPlayerPos();
        do
        {
            MapReset(map);
            MapPartition(map, 0, 0, 0, mapWidth, mapHeight);
        } while (!(map[y1 * mapWidth + x1].activeInHierarchy && map[y2 * mapWidth + x2].activeInHierarchy));

    }

    void MapInit(List<GameObject> map, int width,int height)
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject tile = Instantiate(cube);
                tile.transform.position = new Vector3(j, i);
                map.Add(tile);
                tile.SetActive(false);
            }
        }
    }

    void MapReset(List<GameObject> map)
    {
        foreach(GameObject obj in map)
        {
            obj.SetActive(false);
        }
    }

    (int,int) MapPartition(List<GameObject> map, int stage, int x, int y, int width, int height)
    {
        if (stage == maxstage)
        {
            int xBias = Random.Range((int)(0.4 * width), (int)(0.6 * width));
            int yBias = Random.Range((int)(0.4 * height), (int)(0.6 * height));
            int xRange = Random.Range((int)(0.6 * width), (int)(0.7 * width));
            int yRange = Random.Range((int)(0.6 * height), (int)(0.7 * height));
            //GameObject mycube = Instantiate(cube);
            //mycube.transform.position = new Vector3(x + xBias, y + yBias);
            //mycube.transform.localScale = new Vector3(xRange, yRange);
            for (int i = y + yBias -yRange/2;i < y + yBias + yRange / 2; i++)
            {
                for (int j = x + xBias - xRange / 2; j < x + xBias + xRange / 2; j++)
                {
                    map[i * mapWidth + j].SetActive(true);
                }
            }
            return (x + xBias, y + yBias);
        }
        int partwidth = Random.Range((int)(0.35 * width), (int)(0.65 * width));
        int partheight = Random.Range((int)(0.35 * height), (int)(0.65 * height));
        int dir = Random.Range(0, 2);
        if (width > 4 * height)
        {
            dir = 0;
        }
        else if (height > 4 * width)
        {
            dir = 1;
        }
        int x1, x2, y1, y2;
        if (dir == 0) // x분할
        {
            (x1, y1) = MapPartition(map, stage + 1, x, y, partwidth, height);
            (x2, y2) = MapPartition(map, stage + 1, x + partwidth, y, width - partwidth, height);
            for (int i = x1; i <= x + partwidth; i++)
            {
                map[y1 * mapWidth + i].SetActive(true);
            }
            for (int i = x + partwidth; i < x2; i++)
            {
                map[y2 * mapWidth + i].SetActive(true);
            }
            for (int i = Mathf.Min(y1, y2); i <= Mathf.Max(y1, y2); i++)
            {
                map[i * mapWidth + x + partwidth].SetActive(true);
            }
            return (x1, y1);
        }
        else  // y분할
        {
            (x1,y1) = MapPartition(map, stage + 1, x, y, width, partheight);
            (x2,y2) = MapPartition(map, stage + 1, x, y + partheight, width, height - partheight);
            for (int i = y1; i <= y + partheight; i++)
            {
                map[i * mapWidth + x1].SetActive(true);
            }
            for (int i = y + partheight; i < y2; i++)
            {
                map[i * mapWidth + x2].SetActive(true);
            }
            for (int i = Mathf.Min(x1, x2); i <= Mathf.Max(x1, x2); i++)
            {
                map[(y + partheight) * mapWidth + i].SetActive(true);
            }
            return (x2, y2);
        }
    }

    public (int, int, List<GameObject>) MapData()
    {
        return (mapWidth, mapHeight, map);
    }
}
