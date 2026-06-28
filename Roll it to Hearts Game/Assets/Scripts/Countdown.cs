using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 60; 
    public TextMeshProUGUI timerText; 
    private bool timerIsRunning = false;
    public GameObject player;
    public GameObject enemy;
    public TextMeshProUGUI bestTimes;
    public TextMeshProUGUI timeUpText;
    private float bestTime;
    private float levelTime;

    private void Start()
    {
        timerIsRunning = true;

        GameManager.Instance.LoadData();
        bestTime = GameManager.Instance.bestTimeGM;

        bestTimes.gameObject.SetActive(false);
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;

                timeUpText.gameObject.SetActive(true);
                timeUpText.GetComponent<TextMeshProUGUI>().text = "Time is Up!";
                GameObject.FindGameObjectWithTag("Player").SetActive(false);
            }
        }

        if (player == null)
        {
            timerIsRunning = false; 
            bestTimes.text = "Try Again !!!"; 
            bestTimes.gameObject.SetActive(true);          
        }

        if (enemy == null)
        {
            timerIsRunning = false;
            CalculateBestTime();                  
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        timerIsRunning = false;
    }

    public void CalculateBestTime()
    {
        // Calculate the time taken for the current level
        levelTime = 60 - timeRemaining;

        // Check if this is the first time the level is completed
        if (bestTime == 0)
        {
            bestTime = levelTime;
        }

        // Compare the current level time with the best time
        if (levelTime < bestTime)
        {
            bestTimes.text = "New Best Time : " + (int)levelTime + " sec";
            bestTime = levelTime; // Update the best time
            GameManager.Instance.bestTimeGM = bestTime; // Update the GameManager
            GameManager.Instance.WriteData(); // Save the new best time
        }
        else
        {
            bestTimes.text = "Best Time : " + (int)bestTime + " sec";
        }

        bestTimes.gameObject.SetActive(true);
    }
}