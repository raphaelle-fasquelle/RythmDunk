using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Canvases")]
    public GameObject inGameCanvas;
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public GameObject menuCanvas;

    public bool trackOver;
    public bool inGame;

    private const string loseState = "Lose", inGameState = "InGame", winState = "Win", menuState = "Menu";

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartMusic();
        inGame = true;
    }

    private void Update()
    {
        if (GetComponent<AudioSource>().time >= MusicInfo.musicDuration)
            trackOver = true;
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

    public void StartMusic()
    {
        StartCoroutine(WaitToStartMusic());
    }

    IEnumerator WaitToStartMusic()
    {
        yield return new WaitForSeconds(1.86f);
        GetComponent<AudioSource>().Play();
    }

    public void Lost()
    {
        GetComponent<AudioSource>().Stop();
        inGame = false;
        StartCoroutine(WaitForLastBallsToDespawn());
    }

    IEnumerator WaitForLastBallsToDespawn()
    {
        yield return new WaitForSeconds(2.5f);
        ChangeUIState(loseState);
    }

    public void StartTrackGame()
    {
        ChangeUIState(inGameState);
        StartMusic();
        inGame = true;
        BallSpawner.Instance.ResetBallSpawner();
    }
}
