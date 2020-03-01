using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public GameManager gm;

    /// <summary>
    /// Ball rotates on itself at rotatingSpeed
    /// </summary>
    public float rotatingSpeed;

    [SerializeField]
    private float forwardForce = -7.5f;
    [SerializeField]
    private float verticalForce = 10f;

    private Rigidbody rb;
    /// <summary>
    /// Whether or not the ball went through the hoop before being despawned
    /// </summary>
    private bool scored;

    private void OnEnable()
    {
        scored = false;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        //Despawn the ball when it reaches a certain y position
        if(transform.position.y < -2.5f)
        {
            if (!scored)
            {
                //If the ball didn't go through the hoop, the game is lost
                if (gm.inGame)
                    gm.Lost();
            }
            ResetBall();
        }
    }

    /// <summary>
    /// Rotate the ball around its x axis
    /// </summary>
    private void FixedUpdate()
    {
        transform.Rotate(rotatingSpeed * Time.fixedDeltaTime, 0, 0);
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
        transform.eulerAngles = Vector3.zero;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    /// <summary>
    /// Update score if ball went through the hoop
    /// </summary>
    /// <param name="other"></param>
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
