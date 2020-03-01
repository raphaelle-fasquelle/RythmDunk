using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public Queue<GameObject> ballQueue;
    public GameObject balls;
    private int currentSpawn;
    public float gameStartTime;
    public GameObject spawnPositions;
    public GameManager gm;


    void Start()
    {
        ballQueue = new Queue<GameObject>();
        foreach(Transform child in balls.transform)
        {
            ballQueue.Enqueue(child.gameObject);
        }
    }

    private void Update()
    {
        if (gm.inGame && currentSpawn < MusicInfo.startTimes.Count && Time.time - gameStartTime >= MusicInfo.startTimes[currentSpawn])
        {
            currentSpawn++;
            Spawn();
            if (gm.infinteMode && currentSpawn >= MusicInfo.startTimes.Count)
            {
                currentSpawn = 0;
                gameStartTime = Time.time;
            }
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
