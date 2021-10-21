using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum State
    {
        Stopped, Running
    }
    public static GameManager Instance;
    [SerializeField] private GameObject playerPrefab;

    internal State gameState = State.Stopped;
    private int currentLevel = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            Application.targetFrameRate = 60;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            WinGame();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            LoseGame();
        }
    }

    public void LoadLevel()
    {
        currentLevel = PlayerPrefs.GetInt("CURRENT_LEVEL", 1);
        UIManager.Instance.SetLevelLabel(currentLevel);
        SpawnManager.Instance.SpawnPlayer(Vector3.zero);

        // Max 4 balls to make game a bit easier
        for (int i = 0; i < Mathf.Min(currentLevel, 4); i++)
        {
            SpawnManager.Instance.SpawnBallRandomly(Ball.Size.Big);
        }
    }

    public void StartGame()
    {
        LoadLevel();
        
        gameState = State.Running;
    }

    public void ResetGame()
    {
        gameState = State.Stopped;
        
        for (int i = 0; i < SpawnManager.Instance.players.Count; i++)
        {
            Destroy(SpawnManager.Instance.players[i].gameObject);
        }
        
        for (int i = 0; i < SpawnManager.Instance.balls.Count; i++)
        {
            Destroy(SpawnManager.Instance.balls[i].gameObject);
        }
        
        SpawnManager.Instance.players.Clear();
        SpawnManager.Instance.balls.Clear();
    }

    public void LoseGame()
    {
        ResetGame();
        StartCoroutine(UIManager.Instance.HideScreen(UIManager.Screen.Game));
        UIManager.Instance.ShowScreen(UIManager.Screen.Lose);
    }

    public void WinGame()
    {
        ResetGame();
        PlayerPrefs.SetInt("CURRENT_LEVEL", currentLevel + 1);
        StartCoroutine(UIManager.Instance.HideScreen(UIManager.Screen.Game));
        UIManager.Instance.ShowScreen(UIManager.Screen.Win);
    }

    public void CheckWin()
    {
        if (SpawnManager.Instance.balls.Count == 0)
        {
            WinGame();
        }
    }
}
