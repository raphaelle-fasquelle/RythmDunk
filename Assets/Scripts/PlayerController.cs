using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Contains the four transforms of the possible player positions
    /// </summary>
    public GameObject zones;
    public GameManager gm;
    /// <summary>
    /// Contains the three background corresponding to the four possible player and spawn positions
    /// </summary>
    public GameObject backgroundZones;

    private Material backgroundMat1, backgroundMat2, backgroundMat3, backgroundMat4;
    private Color backgroundCol1, backgroundCol2, backgroundCol3, backgroundCol4;

    private void Start()
    {
        backgroundMat1 = backgroundZones.transform.GetChild(0).GetComponent<Renderer>().material;
        backgroundMat2 = backgroundZones.transform.GetChild(1).GetComponent<Renderer>().material;
        backgroundMat3 = backgroundZones.transform.GetChild(2).GetComponent<Renderer>().material;
        backgroundMat4 = backgroundZones.transform.GetChild(3).GetComponent<Renderer>().material;
        backgroundCol1 = backgroundMat1.color;
        backgroundCol2 = backgroundMat2.color;
        backgroundCol3 = backgroundMat3.color;
        backgroundCol4 = backgroundMat4.color;
    }

    /// <summary>
    /// Move player to the zone that was touched
    /// Briefly changes the zone background color to give feedback
    /// </summary>
    void Update()
    {
        if (gm.inGame && Input.GetMouseButtonDown(0))
        {
            int width = Screen.width;
            float mousePos = Input.mousePosition.x;
            if (mousePos < width / 4f)
            {
                //zone 1
                transform.position = zones.transform.GetChild(0).position;
                backgroundMat1.color = new Color(0.4f, 0.4f, 0.4f);
                StartCoroutine(ChangeColorBack(backgroundMat1, backgroundCol1));
            }
            else if(mousePos < width / 2f)
            {
                //zone 2
                transform.position = zones.transform.GetChild(1).position;
                backgroundMat2.color = new Color(0.25f, 0.25f, 0.25f);
                StartCoroutine(ChangeColorBack(backgroundMat2, backgroundCol2));
            }
            else if(mousePos < 3 * width / 4f)
            {
                //zone 3
                transform.position = zones.transform.GetChild(2).position;
                backgroundMat3.color = new Color(0.4f, 0.4f, 0.4f);
                StartCoroutine(ChangeColorBack(backgroundMat3, backgroundCol3));
            }
            else
            {
                //zone 4
                transform.position = zones.transform.GetChild(3).position;
                backgroundMat4.color = new Color(0.25f, 0.25f, 0.25f);
                StartCoroutine(ChangeColorBack(backgroundMat4, backgroundCol4));
            }
        }
    }

    /// <summary>
    /// Changes the zone background color back to its original color
    /// </summary>
    /// <param name="m">The material of the background to change</param>
    /// <param name="c">The original color of the background</param>
    /// <returns></returns>
    IEnumerator ChangeColorBack(Material m, Color c)
    {
        yield return new WaitForSeconds(0.2f);
        m.color = c;
    }
}
