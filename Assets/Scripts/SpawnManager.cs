using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] healthItem;
    public GameObject[] pickupItem;
    private GameObject player;

    private float xMin = 50.0f;
    private float xMax = 150.0f;
    private float zRange = 30.0f;
    private float xPos;
    private float yPos = 2.5f;
    private float zPos;
    private float xOffset;

    private float delayTime = 3;

    private int enemyCount;
    private int waveCount = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerController>().isGameActive)
        {
            SpawnEnemyWave();
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        xPos = Random.Range(xMin, xMax);
        zPos = Random.Range(-zRange, zRange);
        Vector3 randomPos = new Vector3(xPos, yPos, zPos);

        return randomPos;
    }

    void SpawnEnemyWave()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        float currentTime = Time.time;
        float spawnDelay = player.GetComponent<PlayerController>().lastEnemyDefeatedTime + delayTime;
        bool gameOver = player.GetComponent<PlayerController>().gameOver;
        if(enemyCount == 0 && currentTime > spawnDelay && !gameOver)
        {
          for(int i = 0; i < waveCount; i++)
          {
              int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
              int randomHealthItemIndex = Random.Range(0, healthItem.Length);
              int randomPickupItemIndex = Random.Range(0, pickupItem.Length);
              Instantiate(enemyPrefabs[randomEnemyIndex], GenerateRandomPosition(), enemyPrefabs[randomEnemyIndex].transform.rotation);
              Instantiate(healthItem[randomHealthItemIndex], GenerateRandomPosition(), healthItem[randomHealthItemIndex].transform.rotation);
              Instantiate(pickupItem[randomPickupItemIndex], GenerateRandomPosition(), pickupItem[randomPickupItemIndex].transform.rotation);
          }
          waveCount++;
        }
    }
}
