using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
//using UnityEditor.TerrainTools;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Private variables
    private float speed;
    private float downSpeed;
    private AudioSource player;


    // Public variables
    public GameObject parentTarget;
    public GameObject positiveFireParticle;
    public GameObject negativeFireParticle;
    private SpawnManager spawnManager;
    private Colors colors;
    public AudioClip scoreSFX;
    public AudioClip looseSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Sets the random speed for moving cards
        speed = Random.Range(1f, 4.5f);
        downSpeed = 0f;

        player = GetComponent<AudioSource>();
        spawnManager = GetComponent<SpawnManager>();
        colors = GetComponent<Colors>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the cards from left to right
        parentTarget.transform.Translate(speed * Time.deltaTime, 0, - downSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Endpoint"))        
        {
            Debug.Log("collided with endpoint");
            speed = 0f;
            downSpeed = 5f;

            Destroy(parentTarget.gameObject, 1f);
            Instantiate(negativeFireParticle, parentTarget.transform.localPosition, parentTarget.transform.localRotation);
            player.PlayOneShot(looseSFX);
        }
        
    }

    private void OnMouseDown()
    {
        player.PlayOneShot(scoreSFX);        

        speed = 0f;
        downSpeed = 5f;

        UIScript.score += GetComponentInChildren<Colors>().amt;
        UIScript.hits += 1;

        Instantiate(positiveFireParticle, parentTarget.transform.localPosition, parentTarget.transform.localRotation);
        
        //Destroy(parentTarget.gameObject);      
    }
}
