using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI scoreTitle;
    [SerializeField] private TextMeshProUGUI hiscoreTitle;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private GameObject playBtn;
    [SerializeField] private GameObject pauseBtn;
    [SerializeField] private GameObject gameOver;
    private PlayerMovement player;
    [SerializeField] private GameObject pausePanel;

    private int score;
    private int highScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        scoreText.gameObject.SetActive(false);
        hiscoreText.gameObject.SetActive(false);
        scoreTitle.gameObject.SetActive(false);
        hiscoreTitle.gameObject.SetActive(false);

        gameOver.gameObject.SetActive(false);
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        highScore = LoadHighScore();

        scoreTitle.gameObject.SetActive(true);
        hiscoreTitle.gameObject.SetActive(true);
        hiscoreText.text = LoadHighScore().ToString();

        scoreText.gameObject.SetActive(true);
        hiscoreText.gameObject.SetActive(true);
        pauseBtn.gameObject.SetActive(true);
        score = 0;
        scoreText.text = score.ToString();

        playBtn.SetActive(false);
        gameOver.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
        }
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            hiscoreText.text = score.ToString();
        }
    }

    public void PauseMenu()
    {
        Pause();
        pausePanel.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        player.enabled = false;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        player.enabled = true;
        pausePanel.SetActive(false);
    }

    public void GameOver()
    {
        Pause();
        gameOver.SetActive(true);
        playBtn.SetActive(true);
        pauseBtn.SetActive(false);

        AudioManager.instance.PlaySFX("Die");
        AudioManager.instance.StopMusic();

        //Save high score
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    public void IncreaseScore()
    {
        AudioManager.instance.PlaySFX("Point");
        score++;
        scoreText.text = score.ToString();
    }

    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    public void Back()
    {
        Time.timeScale = 1;
        AudioManager.instance.PlayMusic("Background");
        SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
    }
}
