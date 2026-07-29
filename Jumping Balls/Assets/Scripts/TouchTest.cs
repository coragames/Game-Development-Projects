using UnityEngine;
using System.Collections;
using System;

public class TouchTest : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // stores the ray from the camera to the touch position
        ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

        // draws the ray in the scene view for debugging purposes
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        // checks if the ray hits any colliders in the scene
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // if the ray hits a collider, it logs the name of the hit object
            Debug.Log("Hit: " + hit.collider.gameObject.name);
            Destroy(hit.transform.gameObject);
        }
    }
}
