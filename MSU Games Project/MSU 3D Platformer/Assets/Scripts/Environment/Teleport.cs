using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("The destination object with a Teleport script attached.")]
    public Teleport destinationTeleporter;
    
    [Tooltip("The effect to create when teleporting.")]
    public GameObject teleportEffect;

    // teleporteAvailable tracks the state of the teleporter. 
    // It is used to tunr off the destination Teleporter just before the player is moved to it so the player does not teleport back immediately.
    private bool teleporterAvailable = true;

    void OnTriggerEnter(Collider other)
    {
        // if player, then teleport
        if ((other.tag == "Player") && (teleporterAvailable == true) && (destinationTeleporter != null))
        {
            // reposition the player
            Debug.Log(other.name + " collided with " + this.transform.parent.name + " teleport to " + destinationTeleporter.transform.parent.name);

            // spawn the teleport effect
            if (teleportEffect != null)
            {
                Instantiate(teleportEffect, transform.position, transform.rotation, null);
            }
            // turn off destination teleporter so it will not teleport player right back
            destinationTeleporter.teleporterAvailable = false;

            // turn oof Character Controller if there is one, so we can move this gameObject
            CharacterController characterController = other.gameObject.GetComponent<CharacterController>();
            
            if (characterController != null)
            {
                characterController.enabled = false;
            }

            // calculate height offset of the player from the Teleporter middle
            float heightOffset = transform.position.y - other.transform.position.y;

            // reposition the player
            other.transform.position = destinationTeleporter.transform.position - new Vector3(0, heightOffset, 0);

            // if Character Controller is specified then turn it back on
            if (characterController != null)
            {
                characterController.enabled = true;
            }

        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            // when player exits the teleporter, make it available again for next time they enter
            teleporterAvailable = true;
        }
    }
}
