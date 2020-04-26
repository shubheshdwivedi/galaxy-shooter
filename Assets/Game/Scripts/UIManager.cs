using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private Sprite[] lives;
    [SerializeField] private Image livesImageDisplay;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject startText;
    private int _score = 0;
    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        _score += 10;
        scoreText.text = "Score: " + _score;
    }

    public void HideTitleScreen()
    {
        this.startText.SetActive(false);
        this.titleScreen.SetActive(false);
    }

    public void ShowTitleScreen()
    {
        this._score = 0;
        scoreText.text = "Score: 0";
        this.startText.SetActive(true);
        this.titleScreen.SetActive(true);
    }
    
}
