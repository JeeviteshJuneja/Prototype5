using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button retartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public bool isGameActive;
    public bool isGamePaused;
    [SerializeField] private float spawnRate = 1f;
    private int score;
    private int lives;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGameActive)
        {
            isGamePaused = !isGamePaused;
            if (isGamePaused)
            {
                pauseScreen.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pauseScreen.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToRemove)
    {
        lives -= livesToRemove;
        if (lives <= 0)
        {
            livesText.text = "Lives: 0";
            GameOver();
        }
        else
        {
            livesText.text = "Lives: " + lives;
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        retartButton.gameObject.SetActive(true);
        isGameActive = false;
        isGamePaused = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        isGamePaused = false;
        score = 0;
        lives = 3;
        spawnRate /= difficulty;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(0);

        titleScreen.gameObject.SetActive(false);
    }
}
