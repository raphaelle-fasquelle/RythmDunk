using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public static BallSpawner Instance;
    public Queue<GameObject> ballQueue;
    public GameObject balls;
    private int currentSpawn;
    private float gameStartTime;
    public GameObject spawnPositions;

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
        }
        InitTime();
    }

    private void Update()
    {
        if(GameManager.Instance.inGame && !GameManager.Instance.trackOver && Time.time - gameStartTime >= MusicInfo.startTimes[currentSpawn])
        {
            currentSpawn++;
            Spawn();
        }
    }

    public void InitTime()
    {
        gameStartTime = Time.time;
    }

    public void Spawn()
    {
        GameObject newBall = ballQueue.Dequeue();
        newBall.SetActive(true);
        SetBallStartPos(newBall);
        newBall.GetComponent<BallBehavior>().ThrowBall();
        ballQueue.Enqueue(newBall);
    }

    public void SetBallStartPos(GameObject ball)
    {
        int rand = Random.Range(0, 4);
        ball.transform.position = spawnPositions.transform.GetChild(rand).position;
    }

    public void ResetBallSpawner()
    {
        currentSpawn = 0;
        InitTime();
    }
}
