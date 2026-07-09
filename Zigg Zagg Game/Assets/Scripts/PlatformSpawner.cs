using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab; // Reference to the platform prefab that will be spawned
    public GameObject diamondPrefab; // Reference to the diamond prefab that will be spawned on the platforms
    Vector3 lastPos; // Variable to keep track of the last position where a platform was spawned
    float size; // Variable to store the size of the platform
    public bool gameOver = false; // Flag to check if the game is over

    void Awake()
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPos = platformPrefab.transform.position; // Initialize lastPos to the position of the platform prefab
        size = platformPrefab.transform.localScale.x; // Get the size of the platform based on its local scale in the x-axis
    }

    public void StartSpawnPlatforms()
    {
        InvokeRepeating("SpawnPlatforms", 0.1f, 0.2f); // Start invoking the SpawnPlatforms method repeatedly with a delay of 0.1 seconds and a repeat rate of 0.2 seconds to continuously spawn platforms during the game
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.isGameOver == true)
        {
            CancelInvoke("SpawnPlatforms"); // If the game is over, cancel the repeated invocation of the SpawnPlatforms method to stop spawning new platforms
        }
    }

    void SpawnPlatforms()
    {
        int rand = Random.Range(0, 6); // Generate a random integer between 0 and 5 (inclusive)
        if (rand < 3)
        {
            SpawnX(); // If the random number is less than 3, spawn a platform in the x-axis
        }
        else if (rand >= 3)
        {
            SpawnY(); // If the random number is 3 or greater, spawn a platform in the z-axis
        }
    }
    void SpawnX()
    {
        // Calculate the new position for the platform by adding the size of the platform to the last position in the x-axis
        Vector3 pos = lastPos;
        pos.x += size;         
        Instantiate(platformPrefab, pos, Quaternion.identity); // Instantiate a new platform at the calculated position with no rotation  
        lastPos = pos; // Update lastPos to the new position where the platform was spawned     

        int rand = Random.Range(0, 4); // Generate a random integer between 0 and 3 (inclusive)
        if(rand < 1)
        {
            Vector3 diamondPos = pos; // Initialize the position for the diamond to be the same as the platform position
            diamondPos.y += 1f; // Adjust the y-position of the diamond to be above the platform
            Instantiate(diamondPrefab, diamondPos, diamondPrefab.transform.rotation); // Instantiate a new diamond at the calculated position with no rotation
        }  
    }

    void SpawnY()
    {
        // Calculate the new position for the platform by adding the size of the platform to the last position in the z-axis
        Vector3 pos = lastPos;
        pos.z += size;        
        Instantiate(platformPrefab, pos, Quaternion.identity); // Instantiate a new platform at the calculated position with no rotation   
        lastPos = pos; // Update lastPos to the new position where the platform was spawned   

        int rand = Random.Range(0, 4); // Generate a random integer between 0 and 3 (inclusive)
        if(rand < 1)
        {
            Vector3 diamondPos = pos; // Initialize the position for the diamond to be the same as the platform position
            diamondPos.y += 1f; // Adjust the y-position of the diamond to be above the platform
            Instantiate(diamondPrefab, diamondPos, diamondPrefab.transform.rotation); // Instantiate a new diamond at the calculated position with no rotation
        }    
    }
}
