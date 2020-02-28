using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;
    public Queue<GameObject> ballQueue;
    public GameObject balls;
    private int currentSpawn;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {
        currentSpawn = 0;
        ballQueue = new Queue<GameObject>();
        foreach(Transform child in balls.transform)
        {
            ballQueue.Enqueue(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(!GameManager.Instance.trackOver && Time.time >= MusicInfo.startTimes[currentSpawn])
        {
            currentSpawn++;
            Spawn();
        }
    }

    public void Spawn()
    {
        GameObject newBall = ballQueue.Dequeue();
        newBall.SetActive(true);
        newBall.GetComponent<BallBehavior>().ThrowBall();
        ballQueue.Enqueue(newBall);
    }
}
