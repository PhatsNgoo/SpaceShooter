using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] ObjectPooler objectPooler;
    int score;



    void SpawnAsteroid()
    {
        var objectToSpawn=objectPooler.GetPooledObject(TagManager.ASTEROID);
    }
    public void UpdateScore()
    {
        score+=10;
        UIManager.Instance.UpdateScoreText(score);
    }
    public void RegisterHighScore()
    {
        var currentHighScrore=PlayerPrefs.GetInt("HighScore",0);
        if(score>currentHighScrore)
        {
            PlayerPrefs.SetInt("HighScore",score);
        }
    }
}
