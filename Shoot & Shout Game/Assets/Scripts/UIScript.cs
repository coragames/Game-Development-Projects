using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    // Public variables
    public static int score;
    public static int hits;
    public static int highScore;    
    public Text scoreText;
    public Text hitsText;
    public Text timeText;
    public Text highScoreText;
    public AudioSource bgMusic;
    public GameObject endPanel;
    public GameObject newHighScoreObj;
    public Text yourScoreText;

    // Private variables
    private int timeAmt = 60;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeAmt = SaveScript.chosenTime;
        score = 0;
        hits = 0;
        Time.timeScale = 1;

        InvokeRepeating("TimeCountDown", 1 , 1);
        highScoreText.text = highScore.ToString();

        newHighScoreObj.SetActive(false);
        endPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            bgMusic.Stop();
        }       

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        } 
    }

    void OnGUI()
    {
        scoreText.text = score.ToString();
        hitsText.text = hits.ToString();
        timeText.text = timeAmt.ToString();
    }

    void TimeCountDown()
    {
        if (timeAmt > 0)
        {
            timeAmt -= 1 ;
        }
        else
        {
            if(score > highScore)
            {
                newHighScoreObj.SetActive(true);
            }
            yourScoreText.text = score.ToString();
            endPanel.SetActive(true);
            Time.timeScale = 0;

            bgMusic.Stop();
        }
    }

    public void RestartGame()
    {
        if(score > highScore)
        {
            highScore = score;
        }

        SceneManager.LoadScene(0);
        bgMusic.Play();
    }
}
