using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CountdownTimer : MonoBehaviour
{
    public GameOverScreen GameOverScreen;

    float currentTime = 0f;
    float startingTime = 120f;

    [SerializeField] TextMeshProUGUI countdownText;
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }

    }

    public void GameOver()
    {
        GameOverScreen.Setup(currentTime);
    }
}
