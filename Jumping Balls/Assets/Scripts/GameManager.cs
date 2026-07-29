using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region UI

    // Game Over text panel
    public GameObject gameOverText;

    // Restart button
    public GameObject resetButton;

    // Exit button
    public GameObject exitButton;

    // Hit animation prefab
    public GameObject hitAnimationPrefab;

    // Restart button component
    private Button restartButton;

    // Timer display
    private TMP_Text timeTextDisplay;

    // Displays the number of balls destroyed this game
    public TMP_Text ballsDestroyedText;

    // Displays the highest score saved
    public TMP_Text highestBallsDestroyedText;

    // Displays the best score on the main menu/game over screen
    public TMP_Text bestScoreText;

    #endregion


    #region Timer

    // Starting game time
    public int startTime = 60;

    // Current remaining time
    private float currentTime;

    #endregion


    #region Animations

    // Logo animation
    public Animator logoAnimator;

    // Play button animation
    public Animator buttonAnimator;

    #endregion


    #region Ball Spawning

    // Array of different ball prefabs
    public GameObject[] spawnBallPrefabs;

    // Number of balls to spawn in the current wave
    private int ballsToSpawnInWave = 1;

    // Number of balls currently in the scene
    private int activeBallsCount = 0;

    // Reference to the spawn coroutine
    private Coroutine spawnCoroutine;

    #endregion


    #region Audio

    // Audio Source
    private AudioSource audioSource;

    // Background music
    public AudioClip bgMusicClip;

    // Button click sound
    public AudioClip buttonClick;

    // Ball destroy sound
    public AudioClip ballDestroySound;

    #endregion


    #region Game State

    // True while the game is running
    private bool isGameActive = false;

    // True once the game ends
    private bool isGameOver = false;

    #endregion


    #region Spawn Area

    // Width of spawning area
    private float spawnAreaWidth = 4f;

    // Height of spawning area
    private float spawnAreaHeight = 8f;

    #endregion


    #region Score

    // Current game's score
    private int ballsDestroyed = 0;

    // Highest score saved on the computer
    private int highScore = 0;

    #endregion


    void Start()
    {
        // Get AudioSource component
        audioSource = GetComponent<AudioSource>();

        // Find timer text
        timeTextDisplay = GameObject.Find("Time").GetComponent<TMP_Text>();

        // Get restart button component
        restartButton = resetButton.GetComponent<Button>();

        // Add listener to restart button
        restartButton.onClick.AddListener(ResetGame);

        // Hide Game Over UI
        gameOverText.SetActive(false);
        resetButton.SetActive(false);
        exitButton.SetActive(false);

        ballsDestroyedText.gameObject.SetActive(false);
        highestBallsDestroyedText.gameObject.SetActive(false);

        // Initialize timer
        currentTime = startTime;
        timeTextDisplay.text = "Remaining Time: " + startTime;

        // Load saved high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Display best score
        bestScoreText.text = "Best Score: " + highScore;
    }


    void Update()
    {
        // Update timer while the game is active
        if (isGameActive && !isGameOver)
        {
            UpdateTimer();
        }
    }


    // Countdown timer
    void UpdateTimer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime < 0)
            currentTime = 0;

        timeTextDisplay.text = "Remaining Time: " + Mathf.CeilToInt(currentTime);

        // Time has expired
        if (currentTime <= 0)
        {
            // Save new high score
            if (ballsDestroyed > highScore)
            {
                highScore = ballsDestroyed;

                PlayerPrefs.SetInt("HighScore", highScore);

                PlayerPrefs.Save();
            }

            EndGame();
        }
    }


    // Starts the game
    public void Play()
    {
        isGameActive = true;
        isGameOver = false;

        logoAnimator.SetTrigger("LogoAnim");
        buttonAnimator.SetTrigger("ButtonAnim");

        // Play background music
        if (bgMusicClip != null)
        {
            audioSource.clip = bgMusicClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        // Play click sound
        if (buttonClick != null)
        {
            audioSource.PlayOneShot(buttonClick);
        }

        // Begin spawning balls
        spawnCoroutine = StartCoroutine(SpawnWaveCoroutine());
    }


    // Spawns one wave of balls
    IEnumerator SpawnWaveCoroutine()
    {
        yield return new WaitForSeconds(1f);

        if (isGameOver)
            yield break;

        for (int i = 0; i < ballsToSpawnInWave; i++)
        {
            if (isGameOver)
                yield break;

            SpawnBall();
        }
    }


    // Creates one ball
    void SpawnBall()
    {
        int randomIndex = Random.Range(0, spawnBallPrefabs.Length);

        Instantiate(
            spawnBallPrefabs[randomIndex],
            new Vector3(
                Random.Range(-spawnAreaWidth / 2f, spawnAreaWidth / 2f),
                Random.Range(-spawnAreaHeight / 2f, spawnAreaHeight / 2f),
                0
            ),
            Quaternion.identity
        );

        activeBallsCount++;
    }


    // Called from BallControls when a ball is destroyed
    public void OnBallDestroyed(Vector3 hitPosition)
    {
        if (!isGameActive || isGameOver)
            return;

        // Reward player with extra time
        currentTime += 5f;

        activeBallsCount--;

        // Increase score
        ballsDestroyed++;

        // Play destroy sound
        if (ballDestroySound != null)
        {
            audioSource.PlayOneShot(ballDestroySound);
        }

        // Spawn hit animation
        if (hitAnimationPrefab != null)
        {
            Instantiate(hitAnimationPrefab, hitPosition, Quaternion.identity);
        }

        // Spawn next wave if all balls are gone
        if (activeBallsCount <= 0)
        {
            ballsToSpawnInWave++;

            spawnCoroutine = StartCoroutine(SpawnWaveCoroutine());
        }
    }


    // Ends the game
    public void EndGame()
    {
        if (isGameOver)
            return;

        isGameOver = true;
        isGameActive = false;

        // Stop spawning
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }

        // Stop music
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Destroy every remaining ball
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");

        foreach (GameObject ball in balls)
        {
            Destroy(ball);
        }

        // Update UI
        ballsDestroyedText.text = "Balls Destroyed: " + ballsDestroyed;

        highestBallsDestroyedText.text = "Highest Balls Destroyed: " + highScore;

        bestScoreText.text = "Best Score: " + highScore;

        // Show Game Over screen
        gameOverText.SetActive(true);
        resetButton.SetActive(true);
        exitButton.SetActive(true);

        ballsDestroyedText.gameObject.SetActive(true);
        highestBallsDestroyedText.gameObject.SetActive(true);
        bestScoreText.gameObject.SetActive(true);
    }


    // Reload the current scene
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    // Quit the game
    public void Quit()
    {
        // Play button click
        if (buttonClick != null)
        {
            audioSource.PlayOneShot(buttonClick);
        }

#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}