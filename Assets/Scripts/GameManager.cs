using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject inGameCanvas;
    public GameObject winCanvas;
    public GameObject loseCanvas;
    public GameObject menuCanvas;

    private void Start()
    {
    }

    public void ChangeUIState(string state)
    {
        switch (state)
        {
            case "InGame":
                SetActiveOrInactive(new List<GameObject> { inGameCanvas }, new List<GameObject> { winCanvas, loseCanvas, menuCanvas });
                break;
            case "Win":
                SetActiveOrInactive(new List<GameObject> { winCanvas }, new List<GameObject> { inGameCanvas, loseCanvas, menuCanvas });
                break;
            case "Lose":
                SetActiveOrInactive(new List<GameObject> { loseCanvas }, new List<GameObject> { winCanvas, inGameCanvas, menuCanvas });
                break;
            case "Menu":
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
}
