using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> objectPool;

    public GameObject[] obstacle = new GameObject[2];

    public int poolSize;

    float currentTime;
    public float createTime = 1.0f;
    float minTime = 0.5f;
    float maxTime = 1.5f;

    void Start()
    {
        createTime = Random.Range(minTime, maxTime);
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(obstacle[Random.Range(0, obstacle.Length)]);
            objectPool.Add(enemy);
            enemy.SetActive(false);
        }
    }

    public void OBP()
    {
        currentTime += GameManager.hsmTimer;
        if (currentTime > createTime)
        {
            if (objectPool.Count > 0)
            {
                GameObject enemy = objectPool[0];
                objectPool.Remove(enemy);

                enemy.SetActive(true);
            }
            currentTime = 0; // 현재 시간 초기화
            createTime = Random.Range(minTime, maxTime);
        }
    }
}
