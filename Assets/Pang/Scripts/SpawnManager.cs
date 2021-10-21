using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;

    public List<GameObject> balls = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();

    [Header("References:")]
    [SerializeField] internal List<Transform> ballSpawnPoints = new List<Transform>();
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject spearPrefab;
    [SerializeField] private GameObject smallBallPrefab;
    [SerializeField] private GameObject mediumBallPrefab;
    [SerializeField] private GameObject bigBallPrefab;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnPlayer(Vector3 position)
    {
        players.Add(Instantiate(playerPrefab, position, Quaternion.identity));
    }

    public void SpawnSpear(Vector3 position)
    {
        Instantiate(spearPrefab, position, Quaternion.Euler(0, 0, 90));
    }

    public void SpawnBallRandomly(Ball.Size size)
    {
        GameObject ball = null;
        Vector3 position = ballSpawnPoints[Random.Range(0, ballSpawnPoints.Count)].position;
        switch (size)
        {
            case Ball.Size.Small:
                ball = Instantiate(smallBallPrefab, position, Quaternion.identity);
                break;
            case Ball.Size.Medium:
                ball = Instantiate(mediumBallPrefab, position, Quaternion.identity);
                break;
            case Ball.Size.Big:
                ball = Instantiate(bigBallPrefab, position, Quaternion.identity);
                break;
        }
        
        balls.Add(ball);
    }
    
    public void SpawnBall(Ball.Size size, Vector3 position, Vector2 direction)
    {
        GameObject ball = null;
        switch (size)
        {
            case Ball.Size.Small:
                ball = Instantiate(smallBallPrefab, position, Quaternion.identity);
                break;
            case Ball.Size.Medium:
                ball = Instantiate(mediumBallPrefab, position, Quaternion.identity);
                break;
            case Ball.Size.Big:
                ball = Instantiate(bigBallPrefab, position, Quaternion.identity);
                break;
        }
        
        ball.GetComponent<Ball>().direction = direction;
        balls.Add(ball);
    }
}
