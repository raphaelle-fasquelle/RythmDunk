using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    [SerializeField]
    private float forwardForce;
    [SerializeField]
    private float verticalForce;
    private Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if(transform.position.y < -1.8f)
        {
            ResetBall();
        }
    }

    public void ThrowBall()
    {
        rb.useGravity = true;
        rb.AddForce(0f, verticalForce, forwardForce, ForceMode.Impulse);
    }

    private void ResetBall()
    {
        gameObject.SetActive(false);
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = new Vector3(0f, 0f, 0.2f);
    }

}
