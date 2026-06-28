using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LoadAllScenes : MonoBehaviour
{
    private AudioSource click;
    public AudioClip clickSound;
    private int currentGameLevel = 1; // Initialize to the first level

    public TMP_InputField playerNameInput;


    void Start()
    {
        click = GetComponent<AudioSource>();

        // Load game level from GameManager, default to 1 if no data
        currentGameLevel = GameManager.Instance.gameLevelGM > 0 ? GameManager.Instance.gameLevelGM : 1;
    }

    // Centralized method to play click sound
    private void PlayClickSound()
    {
        if (click != null && clickSound != null)
        {
            click.PlayOneShot(clickSound, 1);
        }
    }

    // Method to increment the game level and save it
    public void IncrementGameLevel()
    {
        currentGameLevel++;
        SaveGameLevel();
    }

    // Method to save the current game level
    private void SaveGameLevel()
    {
        GameManager.Instance.gameLevelGM = currentGameLevel;
        GameManager.Instance.WriteData();
    }

    // Method to load a specific scene
    private void LoadScene(int sceneIndex)
    {
        PlayClickSound(); // Play sound before loading scene
        SceneManager.LoadScene(sceneIndex);
    }

    // Start a new game from level 1
    public void StartGame()
    {
        if(string.IsNullOrEmpty(playerNameInput.text))
        {
            playerNameInput.text = "Please enter player name!";
        }
        else if (playerNameInput.text != "Please enter player name!")
        {
            currentGameLevel = 1; // Reset to level 1
            SaveGameLevel(); // Save the reset level
            LoadScene(1); // Load the first level
        }   

        PlayerPrefs.SetString("playerName", playerNameInput.text);     
    }

    // Restart the current level
    public void PlayAgain()
    {
        LoadScene(currentGameLevel); // Load the current level
    }

    // Load the next level
    public void NextLevel()
    {
        IncrementGameLevel(); // Increment the level
        LoadScene(currentGameLevel); // Load the next level
    }

    // Start the game from the beginning (level 1)
    public void StartAgain()
    {
        LoadScene(1); // Load the first level
    }

    // Load the menu scene (scene 0)
    public void MenuScene()
    {
        LoadScene(0); // Load the menu scene
    }

    // Resume the game from the last saved level
    public void ResumeGame()
    {
        LoadScene(currentGameLevel); // Load the current level 
        playerNameInput.text = PlayerPrefs.GetString("playerName"); 

    }

    // Quit the game
    public void QuitGame()
    {
        PlayClickSound(); // Play sound before quitting

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }
}