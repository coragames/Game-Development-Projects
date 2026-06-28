using System;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int gameLevelGM = 0;
    public int levelScoreGM;
    public float bestTimeGM;
    public TMP_InputField playerName;

    private void Awake()
    {
        if (Instance != null)   
        {
            Destroy(gameObject);
            return;
        }

        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } 

        PlayerPrefs.DeleteAll();
        playerName.text = PlayerPrefs.GetString("playerName");
    } 

    public void SavePlayerName()
    {
        PlayerPrefs.SetString("playerName", playerName.text);
        PlayerPrefs.Save();
    }

    [System.Serializable]

        class SaveData
        {
            public int gameLevel;
            public int levelScore;
            public float bestTime;
        }

        public void WriteData()
        {
            SaveData saveData = new SaveData();

            saveData.gameLevel = gameLevelGM;
            saveData.levelScore = levelScoreGM;
            saveData.bestTime = bestTimeGM;           

            string json = JsonUtility.ToJson(saveData);
            File.WriteAllText(Application.persistentDataPath + "savefile.json", json);
        }

        public void LoadData()
        {
            string path = Application.persistentDataPath + "/savefile.json";
            
            if(File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SaveData saveData = JsonUtility.FromJson<SaveData>(json);

                gameLevelGM = saveData.gameLevel;
                levelScoreGM = saveData.levelScore;
                bestTimeGM = saveData.bestTime;
            }
        }

}
