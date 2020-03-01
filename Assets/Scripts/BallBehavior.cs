using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public GameManager gm;


    [SerializeField]
    private float forwardForce;
    [SerializeField]
    private float verticalForce;
    private Rigidbody rb;

    private bool scored;

    private void OnEnable()
    {
        scored = false;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if(transform.position.y < -2.5f)
        {
            if (!scored)
            {
                if (gm.inGame)
                    gm.Lost();
            }
            ResetBall();
        }
    }

    public void ThrowBall()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.drag = 1f;
        Vector3 addedForce = new Vector3(0f, verticalForce, forwardForce);
        rb.AddForce(addedForce, ForceMode.Impulse);
    }

    private void ResetBall()
    {
        gameObject.SetActive(false);
        rb.useGravity = false;
        rb.isKinematic = true;
        //transform.position = initPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gm.inGame)
        {
            scored = true;
            gm.UpdateScore(true);
            gm.scoreFeedback.Play();
        }
    }

}
