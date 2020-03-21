using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public List<GameObject> enemies;
    public float spawnTime = 5f;
    public Transform[] spawnPoints;
    public int maxEnemies = 10;
    private int enemyCounter = 0;

    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject slime in enemies)
        {
            if (slime == null)
            {
                enemies.Remove(slime);
            }
        }
    }

    public void Spawn()
    {
        enemyCounter = enemies.Count;
        if (enemyCounter < maxEnemies)
        {
            GameObject clone = (GameObject)Instantiate(enemy, transform.position, transform.rotation);
            enemies.Add(clone);
        }
        Debug.Log(enemyCounter);
    }


}
