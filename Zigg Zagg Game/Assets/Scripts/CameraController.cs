using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public GameObject player; // Reference to the player GameObject
    private Vector3 offset; // Offset between the camera and the player
    public float lerpSpeed; // Speed at which the camera will follow the player
    public bool gameOver; // Flag to check if the game is over

     void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offset = player.transform.position - transform.position; // Calculate the initial offset between the camera and the player
        gameOver = false; // Initialize the gameOver flag to false at the start of the game
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!gameOver)
        {
            FollowPlayer(); // Call the method to make the camera follow the player
        }
    }

    void FollowPlayer()
    {
        // Get the current position of the camera
        Vector3 pos = transform.position; 
        // Calculate the target position for the camera based on the player's position and the initial offset
        Vector3 targetPosition = player.transform.position - offset; 
        // Smoothly move the camera towards the target position using linear interpolation
        pos = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime); 
        // Update the camera's position to the new calculated position
        transform.position = pos;
    }
}
