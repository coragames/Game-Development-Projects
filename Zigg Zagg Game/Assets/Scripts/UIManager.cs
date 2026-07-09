using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject ziggyZaggPanel;
    public GameObject gameOverPanel;
    public GameObject tapClick;
    public GameObject exitGame;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText1;
    public TextMeshProUGUI highScoreText2;

    public static UIManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
         else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highScoreText1.text = "Highest Score : " + PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GameStart()
    {
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.clickSound);
        ziggyZaggPanel.GetComponent<Animator>().Play("Ziggy Zagg Up");
        tapClick.SetActive(false);
        exitGame.SetActive(false);
    }
    public void GameOver()
    {
        scoreText.text = PlayerPrefs.GetInt("score").ToString();
        highScoreText2.text = PlayerPrefs.GetInt("highScore").ToString();
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponent<Animator>().Play("GameOver Panel");
        exitGame.SetActive(true);
    }

    public void GameReset()
    {   
        GameManager.instance.audioSource.PlayOneShot(GameManager.instance.clickSound);  
        SceneManager.LoadScene(0);
    }
}