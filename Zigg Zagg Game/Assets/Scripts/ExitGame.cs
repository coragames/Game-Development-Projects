using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ExitGame : MonoBehaviour
{
    public void Exit()
    {   
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.clickSound);
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Stop Play Mode in Editor
        #else
            Application.Quit(); // Quit built game
        #endif
    }
}
