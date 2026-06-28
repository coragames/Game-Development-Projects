using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class MenuScript : MonoBehaviour
{
    public Text timeLimit;
    private AudioSource musicMenu;

    void Start()
    {
        Cursor.visible = true;
        SaveScript.chosenTime = 60;

        musicMenu = GetComponent<AudioSource>();
        musicMenu.Play();
    }
    public void IncreaseTime()
    {
        if(SaveScript.chosenTime < 300)
        {
            SaveScript.chosenTime += 60;
        }
    }

    public void DecreaseTime()
    {
        if(SaveScript.chosenTime > 60)
        {
            SaveScript.chosenTime -= 60;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        musicMenu.Stop();
    }

    void OnGUI()
    {
        timeLimit.text = SaveScript.chosenTime.ToString();
    }
}
