using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] Button startBtn;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] Transform endGamePanel;
    [SerializeField] Button retryBtn;
    [SerializeField] TextMeshProUGUI highScoreText;
    [SerializeField] TextMeshProUGUI lastScoreText;

    private void Start() {
        startBtn.onClick.AddListener(()=>StartGame());
        retryBtn.onClick.AddListener(()=>Retry());
    }
    public void UpdateScoreText(int value)
    {
        scoreText.text="Your score: "+value.ToString();
    }
    
    public void UpdateLivesText(int value)
    {
        livesText.text="Lives: "+value.ToString();
    }
    void StartGame()
    {
        GameManager.Instance.StartGame();
        startBtn.transform.parent.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }
    public void EndGame()
    {
        scoreText.gameObject.SetActive(false);
        endGamePanel.gameObject.SetActive(true);
        highScoreText.text="High Score: "+PlayerPrefs.GetInt("HighScore", 0);
        lastScoreText.text="Your Score: "+GameManager.Instance.GetCurrentScore();
        GameManager.Instance.RegisterHighScore();
    }
    void Retry()
    {
        GameManager.Instance.StartGame();
        endGamePanel.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
    }
}
