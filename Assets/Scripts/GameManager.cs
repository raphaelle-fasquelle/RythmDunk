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

    [Header("UI texts")]
    public Text scoreText;
    public Text finalScoreText;
    public GameObject bestSore;

    public Slider difficultySlider;

    /// <summary>
    /// The time that take the balls during spawning and reaching the hoop
    /// </summary>
    [Range(0.2f, 3f)]
    public float musicStartDelay;

    public BallSpawner ballSpawner;

    public GameObject player;
    public ParticleSystem scoreFeedback;

    private int score;

    public bool inGame;
    public bool infinteMode;
    private bool musicStarted;

    private const string loseState = "Lose", inGameState = "InGame", winState = "Win", menuState = "Menu";
    private AudioSource music;

    private void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    /// <summary>
    /// If the game mode isn't infinite and the music stopped playing, the player has won
    /// </summary>
    private void FixedUpdate()
    {
        if (musicStarted && !infinteMode && !music.isPlaying)
        {
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
        player.SetActive(true);
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
        yield return new WaitForSeconds(musicStartDelay);
        music.Play();
        musicStarted = true;
    }

    public void Lost()
    {
        StartCoroutine(MusicFadeOut(music, 1f));
        inGame = false;
        StartCoroutine(WaitForLastBallsToDespawn());
        int totalToCatch = MusicInfo.startTimes.Count / (4 - (int)difficultySlider.value);
        finalScoreText.text = "Your final score is " + score +"\n"+(totalToCatch - score)+ " to go to win !";
    }

    public void Victory()
    {
        inGame = false;
        ChangeUIState(winState);
        player.SetActive(false);
    }

    /// <summary>
    /// Displays the defeat canvas after the last ball on the screen despawned
    /// </summary>
    /// <returns></returns>
    IEnumerator WaitForLastBallsToDespawn()
    {
        yield return new WaitForSeconds(2.5f);
        ChangeUIState(loseState);
        player.SetActive(false);
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

    public static IEnumerator MusicFadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
