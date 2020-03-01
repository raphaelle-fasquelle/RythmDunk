using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject inGameCanvas;
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public GameObject menuCanvas;

    public BallSpawner ballSpawner;

    public Text scoreText;
    public Text finalScoreText;
    private int score;

    public bool trackOver;
    public bool inGame;
    public bool infinteMode;

    private bool musicStarted;

    public Text musicTime;

    private const string loseState = "Lose", inGameState = "InGame", winState = "Win", menuState = "Menu";
    private AudioSource music;

    private void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (musicStarted && !infinteMode && !music.isPlaying)
        {
            trackOver = true;
            if (inGame)
            {
                Victory();
            }
        }
    }

    /// <summary>
    /// Manages canvases according to game state
    /// </summary>
    /// <param name="state"></param>
    public void ChangeUIState(string state)
    {
        switch (state)
        {
            case inGameState:
                SetActiveOrInactive(new List<GameObject> { inGameCanvas }, new List<GameObject> { winCanvas, loseCanvas, menuCanvas });
                break;
            case winState:
                SetActiveOrInactive(new List<GameObject> { winCanvas }, new List<GameObject> { inGameCanvas, loseCanvas, menuCanvas });
                break;
            case loseState:
                SetActiveOrInactive(new List<GameObject> { loseCanvas }, new List<GameObject> { winCanvas, inGameCanvas, menuCanvas });
                break;
            case menuState:
                SetActiveOrInactive(new List<GameObject> { menuCanvas }, new List<GameObject> { winCanvas, loseCanvas, inGameCanvas });
                break;
            default:
                break;
        }
    }

    private void SetActiveOrInactive(List<GameObject> activObjs, List<GameObject> inactivObjs)
    {
        foreach(GameObject go in activObjs)
            go.SetActive(true);
        foreach(GameObject go in inactivObjs)
            go.SetActive(false);
    }

    public void StartTrackGame()
    {
        infinteMode = false;
        music.loop = false;
        InitGame();
    }

    public void StartInfiniteMode()
    {
        infinteMode = true;
        music.loop = true;
        InitGame();
    }

    private void InitGame()
    {
        musicStarted = false;
        UpdateScore(false);
        ChangeUIState(inGameState);
        StartMusic();
        inGame = true;
        ballSpawner.ResetBallSpawner();
    }

    public void StartMusic()
    {
        StartCoroutine(WaitToStartMusic());
    }

    IEnumerator WaitToStartMusic()
    {
        yield return new WaitForSeconds(1.86f);
        music.Play();
        musicStarted = true;
    }

    public void Lost()
    {
        music.Stop();
        inGame = false;
        StartCoroutine(WaitForLastBallsToDespawn());
        finalScoreText.text = "Your final score is " + score;
    }

    public void Victory()
    {
        inGame = false;
        ChangeUIState(winState);
    }

    IEnumerator WaitForLastBallsToDespawn()
    {
        yield return new WaitForSeconds(2.5f);
        ChangeUIState(loseState);
    }

    public void UpdateScore(bool plusOne)
    {
        score = plusOne ? score + 1 : 0;
        scoreText.text = "Score : " + score;
    }

    public void BackToMenu()
    {
        ChangeUIState(menuState);
    }
}
