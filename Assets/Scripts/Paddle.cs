using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Configuration parameters
    [SerializeField] float screenWidthInUnits = 32f;
    [SerializeField] float minX = 2.25f;
    [SerializeField] float maxX = 29.75f;

    // Cached reference
    Ball myBall;
    GameStatus myGameStatus;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y);
        transform.position = paddlePosition;
        myBall = FindObjectOfType<Ball>();
        myGameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 paddlePosition = new Vector2(transform.position.x, transform.position.y); // A compact way to store x and y coordinates
        paddlePosition.x = Mathf.Clamp(GetXPos(), minX, maxX);
        transform.position = paddlePosition;
    }

    private float GetXPos()
    {
        if (myGameStatus.IsAutoPlayEnabled())
        {
            return myBall.transform.position.x;
        }
        else 
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
    }
}
