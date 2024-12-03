/*
* Kayden Miller
* Assignment 8
* Game Maanger
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject title;

    private float spawnRate = 1.0f;
    private int score;

    public bool gameActive;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartGame(int difficulty)
    {
        title.SetActive(false);
        score = 0;
        spawnRate /= difficulty;
        gameActive = true;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTarget()
    {
        while(gameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int s)
    {
        score  += s;    
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
