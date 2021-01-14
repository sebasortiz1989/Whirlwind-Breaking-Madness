using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //Configuration parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float initialVelocityX = 10f;
    [SerializeField] float initialVelocityY = 20f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomFactor = 0.1f;

    //State, distance between the paddle and the ball
    Vector2 paddleToBallVector;
    bool hasStarted = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position;
        myAudioSource = GetComponent<AudioSource>(); // I only get the component once, instead of multiple times.
        myRigidBody2D = GetComponent<Rigidbody2D>(); // Same here, only once
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnMouseClick();
            ControlBallVelocity();
        }
    }

    private void ControlBallVelocity()
    {
        if (myRigidBody2D.velocity.magnitude < 10 && myRigidBody2D.velocity.magnitude != 0)
        {
            myRigidBody2D.velocity = new Vector2(initialVelocityX, initialVelocityY);
        }
        if (myRigidBody2D.velocity.magnitude > 30)
        {
            myRigidBody2D.velocity = new Vector2(initialVelocityX, initialVelocityY);
        }
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(initialVelocityX, initialVelocityY);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 velocityTweak1 = new Vector2(
            UnityEngine.Random.Range(0.01f, -randomFactor), // x random
            UnityEngine.Random.Range(0.01f, -randomFactor)); // y random
        Vector2 velocityTweak2 = new Vector2(
            UnityEngine.Random.Range(0.01f, randomFactor), // x random
            UnityEngine.Random.Range(0.01f, randomFactor)); // y random
        Vector2 velocityTweak3 = new Vector2(
            UnityEngine.Random.Range(0.01f, randomFactor), // x random
            UnityEngine.Random.Range(0.01f, -randomFactor)); // y random
        Vector2 velocityTweak4 = new Vector2(
            UnityEngine.Random.Range(0.01f, -randomFactor), // x random
            UnityEngine.Random.Range(0.01f, randomFactor)); // y random

        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);

            if (myRigidBody2D.velocity.x <=0 && myRigidBody2D.velocity.y <= 0)
                myRigidBody2D.velocity += velocityTweak1;
            else if (myRigidBody2D.velocity.x > 0 && myRigidBody2D.velocity.y > 0)
                myRigidBody2D.velocity += velocityTweak2;
            else if (myRigidBody2D.velocity.x > 0 && myRigidBody2D.velocity.y <= 0)
                myRigidBody2D.velocity += velocityTweak3;
            else
                myRigidBody2D.velocity += velocityTweak4;
        }
            
    }
}
