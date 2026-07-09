using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class BallController : MonoBehaviour
{
    
    [SerializeField] 
    private float speed; // Speed of the ball
    // Flag to check if the game has started
    bool gameStarted;
    // Flag to check if the game is over
    bool gameOver;
    public AudioClip collectSound; // Reference to the sound effect that will be played when the ball collects a diamond    
    public AudioClip tapSound; // Reference to the sound effect that will be played when the player taps to start the game or switch directions
    public AudioClip gameOverSound; // Reference to the sound effect that will be played when the game is over
    // Reference to the Rigidbody component
    Rigidbody rb;

    public GameObject particleEffect; // Reference to the particle effect prefab that will be instantiated when the ball collects a diamond

    void Awake()
    {
        // Get the Rigidbody component attached to the ball
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize the gameStarted and gameOver flags to false at the start of the game
        gameStarted = false;
        // Set the gameOver flag to false at the start of the game
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Gradually increase the speed of the ball over time to make the game more challenging as it progresses
        speed += Time.deltaTime * 0.05f; 

        // Check if the game has not started yet
        if (!gameStarted)
        {
            // If the left mouse button is clicked, set the ball's velocity to start moving and mark the game as started
            if (Input.GetMouseButtonDown(0))
            {
                
                // Set the ball's velocity to move in the positive x direction at the specified speed
                rb.linearVelocity = new Vector3(speed, 0, 0);
                gameStarted = true;

                GameManager.instance.StartGame(); // Call the StartGame method on the GameManager instance to signal that the game has started
            }
        }



        // Draw a red ray downwards from the ball's position for debugging purposes
        Debug.DrawRay(transform.position, Vector3.down, Color.red); 

        // Perform a raycast downwards from the ball's position to check if it is grounded
        if (!Physics.Raycast(transform.position, Vector3.down, 1f))
        {
            // If the raycast does not hit anything, it means the ball is not grounded, so mark the game as over
            gameOver = true;
            AudioSource.PlayClipAtPoint(gameOverSound, transform.position); // Play the game over sound effect to provide audio feedback for the game ending
            rb.linearVelocity = new Vector3(0,-30f,0); // Set the ball's velocity to fall downwards rapidly
            Destroy(gameObject, 1.5f); // Destroy the ball GameObject after a delay of 1.5 seconds to allow it to fall down before being removed from the scene

            Camera.main.GetComponent<CameraController>().gameOver = true; // Access the main camera's CameraController component and set its gameOver flag to true
            
            GameManager.instance.GameOver(); // Call the GameOver method on the GameManager instance to signal that the game is over
        }

        // If the game has started and the left mouse button is clicked, switch the ball's direction
        if (Input.GetMouseButtonDown(0) && !gameOver)
        {
            SwitchDirection();
        }
    }
    
    // Method to switch the ball's direction based on its current velocity
    void SwitchDirection()
    {
        AudioSource.PlayClipAtPoint(tapSound, transform.position); // Play the tap sound effect to provide audio feedback for starting the game or switching directions
        // If the ball is currently moving in the positive x direction, change its velocity to move in the positive z direction.
        if (rb.linearVelocity.x > 0)
        {
            rb.linearVelocity = new Vector3(0, 0, speed);
        }
        // If the ball is currently moving in the positive z direction, change its velocity to move in the positive x direction.
        else if (rb.linearVelocity.z > 0)
        {
            rb.linearVelocity = new Vector3(speed, 0, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the ball has collided with an object tagged as "Diamond"
        if (other.CompareTag("Diamond"))
        {
            GameObject particle = Instantiate(particleEffect, other.transform.position, Quaternion.identity) as GameObject; // Instantiate the particle effect at the diamond's position with no rotation to provide visual feedback for collecting the diamond
            Destroy(other.gameObject); // Destroy the diamond GameObject to simulate collecting it
            Destroy(particle, 1f); // Destroy the instantiated particle effect after a delay of 1 second to allow it to play before being removed from the scene  
            AudioSource.PlayClipAtPoint(collectSound, transform.position); // Play the collect sound effect to provide audio feedback for collecting the diamond          
        }
    }
}
