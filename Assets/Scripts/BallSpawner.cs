using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    /// <summary>
    /// GameObject containing the balls of the object pooler
    /// </summary>
    public GameObject balls;
    /// <summary>
    /// GameObject containing the transforms of the four possible spawn positions
    /// </summary>
    public GameObject spawnPositions;
    /// <summary>
    /// Object Pooling queue
    /// </summary>
    private Queue<GameObject> ballQueue;
    /// <summary>
    /// The index in GPGameLevelMakerFile events of the current ball to spawn
    /// </summary>
    private int currentSpawn;
    public float gameStartTime;
    public GameManager gm;


    /// <summary>
    /// Initiating the object pooler
    /// </summary>
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
        //Spawn ball according to start time in json
        if (gm.inGame && currentSpawn < MusicInfo.startTimes.Count && Time.time - gameStartTime >= MusicInfo.startTimes[currentSpawn])
        {
            currentSpawn++;
            Spawn();
            if(gm.difficultySlider.value == 1 && currentSpawn <= MusicInfo.startTimes.Count - 3)
            {
                //Spawn one out of three
                currentSpawn+=2;
            }else if(gm.difficultySlider.value == 2 && currentSpawn <= MusicInfo.startTimes.Count - 2)
            {
                //Spawn one out of two
                currentSpawn++;
            }
            //Loop if mode is infinite
            if (gm.infinteMode && Time.time - gameStartTime >= MusicInfo.musicDuration - gm.musicStartDelay)
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

    /// <summary>
    /// Spawns new ball using object pooling
    /// </summary>
    public void Spawn()
    {
        GameObject newBall = ballQueue.Dequeue();
        newBall.SetActive(true);
        SetBallStartPos(newBall);
        newBall.GetComponent<BallBehavior>().ThrowBall();
        ballQueue.Enqueue(newBall);
    }

    /// <summary>
    /// Set the new ball position to a random zone between the 4 possible zones
    /// </summary>
    /// <param name="ball">The ball to position</param>
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
