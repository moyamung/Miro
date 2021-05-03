using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    List<GameObject> map = new List<GameObject>();
    int mapWidth, mapHeight;
    public PlayerController player;
    public GameObject enemy;
    public GameObject moneyPrefab;
    public int maxMoney;
    List<(int, int, GameObject)> moneyList = new List<(int, int, GameObject)>();

    int score;
    public Text scoreText;

    void Start()
    {
        score = 0;

        GetComponent<MapMaking>().MapMake();
        (mapWidth, mapHeight, map) = GetComponent<MapMaking>().MapData();
        player.init();
        enemy.GetComponent<Enemy>().init(player);
        InvokeRepeating("InstantiateMoney", 5f, 5f);
        InvokeRepeating("Remap", 60f, 60f);
    }

    public void Remap()
    {
        GetComponent<MapMaking>().Remap(player, enemy.GetComponent<Enemy>());
        (mapWidth, mapHeight, map) = GetComponent<MapMaking>().MapData();
        player.SetMap(map);
        enemy.GetComponent<Enemy>().SetMap(map);
        foreach ((int,int,GameObject) moneyInfo in moneyList)
        {
            Destroy(moneyInfo.Item3);
        }
        moneyList.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        int px, py;
        (px, py) = player.GetPlayerPos();
        foreach (var money in moneyList)
        {
            if (px == money.Item1 && py == money.Item2)
            {
                score++;
                Destroy(money.Item3);
                moneyList.Remove(money);

                scoreText.text = "score : " + score.ToString();
                if (score >= 100) SceneManager.LoadScene("HappyEnd");
                break;
            }
        }
    }

    void InstantiateMoney()
    {
        int x, y;
        while (true)
        {
            x = Random.Range(0, mapWidth);
            y = Random.Range(0, mapHeight);
            if (map[y * mapWidth + x].activeInHierarchy)
            {
                break;
            }
        }
        if (moneyList.Count < maxMoney)
        {
            GameObject moneyObject = Instantiate(moneyPrefab);
            moneyObject.transform.position = new Vector3(x, y, -1);
            moneyList.Add((x, y, moneyObject));
        }
    }
}
