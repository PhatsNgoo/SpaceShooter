using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] ObjectPooler objectPooler;
    int score;
    int waveCount;
    [SerializeField] GameObject playerPrefabs;
    [SerializeField] GameObject player;
    public GameState g_State
    {
        get;
        private set;
    }

    private void Awake() {
        g_State=GameState.Waiting;
    }
    public void StartGame()
    {
        player=Instantiate(playerPrefabs,(Vector2)Camera.main.ViewportToWorldPoint(new Vector2(0.5f,0.5f)),Quaternion.identity);
        player.GetComponent<PlayerController>().lives=3;
        UIManager.Instance.UpdateLivesText(player.GetComponent<PlayerController>().lives);
        ClearRemainingAsterroid();
        waveCount=0;
        score=0;
        g_State = GameState.Playing;
        UIManager.Instance.UpdateScoreText(score);
        SpawnNewWave();
    }
    void SpawnAsteroid()
    {
        var asteroid = objectPooler.GetPooledObject(TagManager.ASTEROID);
        asteroid.transform.localScale = Vector3.one;
        asteroid.transform.position = RandomPositionToSpawn();
        asteroid.gameObject.SetActive(true);
        asteroid.GetComponent<Asteroid>().childrensToSpawn = 2;
    }
    Vector2 RandomPositionToSpawn()
    {
        var edge = Random.Range(1, 4);
        //1=bottom, 2=right,3=top,4=left
        switch (edge)
        {
            case 1:
                return new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0f, 1f), 0)).x, Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y);
            case 2:
                return new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(1, 0)).x, Camera.main.ViewportToWorldPoint(new Vector2(0, Random.Range(0f, 1f))).y);
            case 3:
                return new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0f, 1f), 0)).x, Camera.main.ViewportToWorldPoint(new Vector2(0, 1)).y);
            case 4:
                return new Vector2(Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).x, Camera.main.ViewportToWorldPoint(new Vector2(0, Random.Range(0f, 1f))).y);
        }
        return new Vector2(1, 1);
    }
    void SpawnNewWave()
    {
        waveCount++;
        for (int i = 0; i < waveCount * 5; i++)
        {
            SpawnAsteroid();
        }
    }
    public GameObject GetAsteroidFromPool()
    {
        return objectPooler.GetPooledObject(TagManager.ASTEROID);
    }
    bool CheckWaveCompleted()
    {
        var objectList = FindObjectsOfType<Asteroid>();
        return objectList.All(x => !x.gameObject.activeInHierarchy);
    }
    public void UpdateScore()
    {
        score += 10;
        UIManager.Instance.UpdateScoreText(score);
        if (CheckWaveCompleted())
        {
            SpawnNewWave();
        }
    }
    void ClearRemainingAsterroid()
    {
        var objectList = FindObjectsOfType<Asteroid>();
        foreach(var item in objectList)
        {
            item.gameObject.SetActive(false);
        }
    }
    public int GetCurrentScore()
    {
        return score;
    }
    public void RegisterHighScore()
    {
        g_State=GameState.Waiting;
        var currentHighScrore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > currentHighScrore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }
}
public enum GameState
{
    Playing,
    Waiting
}
