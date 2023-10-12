using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI endScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    public static GameManager I;
    private float score;
    private float bestScore;

    private void Awake()
    {
        I = this; 
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        InvokeRepeating("EnemySpawn", 0.0f, 2.3f);
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.activeSelf == false)
        {
            PauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && pausePanel.activeSelf == true) 
        {
            ResumeGame();
        }
        score += Time.deltaTime * 5;
        scoreText.text = score.ToString("N0");
    }
    private void EnemySpawn()
    {
        int enemyIndex = Random.Range(0, enemies.Length);
        int randomNum = Random.Range(0, 10);
        if (score < 100)
        {
            if (randomNum < 5)
                Instantiate(enemies[0]);
            else if(randomNum > 7)
                Instantiate(enemies[3]);
        }
        else if (score < 200)
        {
            if (randomNum < 7)
            {
                Instantiate(enemies[0]);
                if (randomNum < 3)
                    Instantiate(enemies[3]);
            }
            else
            {
                Instantiate(enemies[1]);
            }
        }
        else if (score < 300)
        {
            if (randomNum < 7)
            {
                Instantiate(enemies[0]);
            }
            if (randomNum < 5)
                Instantiate(enemies[3]);
            if (randomNum < 3)
            {
                Instantiate(enemies[1]);
            }
        }
        else
        {
            Instantiate(enemies[enemyIndex]);
            if (randomNum < 3)
                Instantiate(enemies[3]);
        }

    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOverPanel.SetActive(true);
        if (PlayerPrefs.HasKey("bestScore")==false)
        {
            PlayerPrefs.SetFloat("bestScore", score);
        }
        else if (score > PlayerPrefs.GetFloat("bestScore")) 
        {
            PlayerPrefs.SetFloat("bestScore", score);
        }
        bestScore = PlayerPrefs.GetFloat("bestScore");
        endScoreText.text = score.ToString("N0");
        bestScoreText.text = bestScore.ToString("N0");
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }

    public void AddScore(int point)
    {
        score += point;
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
