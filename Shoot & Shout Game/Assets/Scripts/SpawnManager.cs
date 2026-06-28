using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Public variables
    public string targetToFind;
    public GameObject theTarget;
    public Transform spawnPoint;
    
    // Private variables
    private GameObject targetInGame;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {       
        InvokeRepeating("FindTarget", 0.2f, 3.5f);
    }

    void FindTarget()
    {
        targetInGame = GameObject.Find("targetToFind");

        if (targetInGame == null)
        {
            Instantiate(theTarget, spawnPoint.position, spawnPoint.rotation);
        }        
    }
}
