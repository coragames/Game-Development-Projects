using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float startTime = 60f;
    public TextMeshProUGUI timeText; 
    private bool timerIsRunning = true;
    public GameObject gameOverMessage;
    public GameObject player;
    public GameObject bgMusic;

    // Start is called before the first frame update
    void Start()
    {        
        if (timeText == null)
        {
            timeText = GetComponent<TextMeshProUGUI>();

            if (timeText == null)
            {
                Debug.LogError("TextMeshProUGUI component not found on this GameObject!  Disabling Timer script.");
                enabled = false; 
                return; 
            }
        }
        
        UpdateTimerText();
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (startTime > 0)
            {
                startTime -= Time.deltaTime;
                UpdateTimerText();
            }
            else
            {
                startTime = 0;
                timerIsRunning = false;
                UpdateTimerText();
                gameOverMessage.SetActive(true);
                Destroy(player);
                Destroy(bgMusic);
            }
        }
    }


    void UpdateTimerText()
    {
        timeText.text = "Time Left: " + Mathf.Round(startTime).ToString();
    }
}