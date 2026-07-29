using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallControls : MonoBehaviour
{
    private void OnMouseDown()
    {
        GameManager gameManager = Object.FindFirstObjectByType<GameManager>();

        gameManager.OnBallDestroyed(transform.position);

        Destroy(gameObject);
    }
}
