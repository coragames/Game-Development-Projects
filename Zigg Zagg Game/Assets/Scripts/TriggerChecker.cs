using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the object that exited the trigger has the tag "Player"
        if (other.CompareTag("Player"))
        {
            Invoke("FallDown", 0.5f); // Call the FallDown method after a delay of 0.5 seconds
        }
    }

    void FallDown()
    {
        // Set the Rigidbody component of the current GameObject to be non-kinematic, allowing it to be affected by physics and fall down
        GetComponentInParent<Rigidbody>().useGravity = true; // Enable gravity on the Rigidbody to allow it to fall down
        GetComponentInParent<Rigidbody>().isKinematic = false; // Set the Rigidbody to be affected by physics and allow it to fall down
        Destroy(transform.parent.gameObject, 2f); // Destroy the parent GameObject after a delay of 2 seconds to allow it to fall down before being removed from the scene
    }
}
