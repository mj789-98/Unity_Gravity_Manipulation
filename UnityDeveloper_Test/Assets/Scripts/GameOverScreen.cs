using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup(float currentTime)
    {
        gameObject.SetActive(true);

    }

    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene");
    }

    void Start()
    {
        gameObject.SetActive(false);
    }
}
