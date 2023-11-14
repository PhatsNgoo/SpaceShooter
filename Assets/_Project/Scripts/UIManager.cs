using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : Singleton<UIManager>
{
    [SerializeField] Button startBtn;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Transform endGamePanel;

    private void Start() {
        startBtn.onClick.AddListener(()=>StartGame());
    }
    public void UpdateScoreText(int value)
    {
        scoreText.text="Your score: "+value.ToString();
    }
    void StartGame()
    {

    }
}
