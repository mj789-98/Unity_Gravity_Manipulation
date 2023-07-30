using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //UI references
    public Button startButton;
    public Button restartButton;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI gameWinText;
    public TextMeshProUGUI cubesLeftText;

    public GameObject player;

    public int collectibles;
    public float totalTimeInSeconds = 120f;
    void Start()
    {
        Time.timeScale = 0f;
        collectibles = GameObject.FindGameObjectsWithTag("collectible").Length;
    }

    void Update()
    {
        //Check if the player is falling freely
        if(player.GetComponent<Rigidbody>().velocity.y < -30f)
        {
            EndGame(false);
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        player.SetActive(true);
        startButton.gameObject.SetActive(false);
        StartCoroutine(Countdown());
    }

    public void EndGame(bool win)
    {
        Time.timeScale = 0f;
        if(win)
            gameWinText.gameObject.SetActive(true);
        else
            gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateTimerText(float timeInSeconds)
    {
        // Convert the time to minutes and seconds format for display
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private IEnumerator Countdown()
    {
        float timer = totalTimeInSeconds;

        while (timer > 0f)
        {
            UpdateTimerText(timer);
            yield return new WaitForSeconds(1f);
            timer--;
        }

        UpdateTimerText(timer);
        EndGame(false);
    }

    public void UpdateScore()
    {
        collectibles--;
        cubesLeftText.text = "Cubes Left: " + collectibles;
        if (collectibles == 0)
        {
            EndGame(true);
        }
        
    }
}
