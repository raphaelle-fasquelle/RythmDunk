using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject zones;
    public GameManager gm;

    private void Start()
    {
    }
    // Update is called once per frame
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
            }
            else if(mousePos < width / 2f)
            {
                //zone 2
                transform.position = zones.transform.GetChild(1).position;
            }
            else if(mousePos < 3 * width / 4f)
            {
                //zone 3
                transform.position = zones.transform.GetChild(2).position;
            }
            else
            {
                //zone 4
                transform.position = zones.transform.GetChild(3).position;
            }
        }
    }
}
