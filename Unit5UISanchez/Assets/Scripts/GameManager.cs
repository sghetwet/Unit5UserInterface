using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Xml.Linq;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    private float spawnRate = 1;
    private int lives;
    private int score;
    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;
    public GameObject TitleScreen;
    private AudioSource Background;
    bool gamePaused = false;
    // Start is called before the first frame update
    void Start()
    {
        Background = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PauseMenu();
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int Index = Random.Range(0, targets.Count);
            Instantiate(targets[Index]);
        }

    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int Difficulty)
    {
        //this means the spawn rate divided by the difficulty number (1/1 for easy every second, 1/2 for medium twice a second, and 1/3 for hard third)
        //this means the spawn rate divided by the difficulty number (1/1 for easy every second, 1/2 for medium twice a second, and 1/3 for hard third)
        spawnRate /= Difficulty;
        isGameActive = true;
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
        score = 0;
        scoreText.text = "Score: " + score;
        TitleScreen.SetActive(false);
        lives = 3;
        lives++;
        UpdateLives();
        Background.Play();
    }
    public void UpdateLives()
    {
        if (isGameActive)
        {
            lives--;
            livesText.text = "Lives:" + lives;
            if (lives == 0)
            {
                GameOver();
                Background.Stop();
            }
        }
    }
    public void PauseMenu() { }
}
