using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameOver;

    [SerializeField]public AudioSource audioSource;
    public AudioClip bgMusic;
    public AudioClip clickSound;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGameOver = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        audioSource.clip = bgMusic;
        audioSource.Play();
        UIManager.instance.GameStart();
        ScoreManager.instance.StartScore();
        // Find the GameObject named "PlatformSpawner" in the scene, get its PlatformSpawner component, and call the StartSpawnPlatforms method to start spawning platforms
        GameObject.Find("Platform Spawner").GetComponent<PlatformSpawner>().StartSpawnPlatforms();
    }

    public void GameOver()
    {
        audioSource.Stop();
        UIManager.instance.GameOver();
        ScoreManager.instance.StopScore();
        isGameOver = true; // Set the isGameOver flag to true to indicate that the game is over, which can be used by other scripts to stop certain actions (e.g., spawning platforms)
    }
}
