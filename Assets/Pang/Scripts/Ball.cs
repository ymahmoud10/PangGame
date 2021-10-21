using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    public enum Size { Small, Medium, Big }

    [SerializeField] private Size ballSize;
    [SerializeField] private float hitGroundForce;
    [SerializeField] private float hitWallForce;
    
    internal Vector2 direction = Vector2.zero;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (direction == Vector2.zero)
        {
            direction = Random.value > 0.5f ? Vector2.right : Vector2.left;
        }
        
        HitGround();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {
            HitGround();
        }
        
        if (other.gameObject.layer == 9)
        {
            HitWall();
        }
        
        if (other.gameObject.layer == 11)
        {
            HitPlayer(other.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 10)
        {
            HitSpear(other.gameObject);
        }
    }

    private void HitGround()
    {
        rb.AddForce((Vector2.up + direction * 0.3f) * hitGroundForce);
    }
    
    private void HitWall()
    {
        direction = direction == Vector2.right ? Vector2.left : Vector2.right;
        rb.AddForce(direction * hitWallForce);
    }

    private void HitSpear(GameObject spear)
    {
        Destroy(spear);
        
        SpawnManager.Instance.balls.Remove(gameObject);
        Destroy(gameObject);

        switch (ballSize)
        {
            case Size.Small:
                GameManager.Instance.CheckWin();
                break;
            case Size.Medium:
                SpawnManager.Instance.SpawnBall(Size.Small, transform.position, Vector2.right);
                SpawnManager.Instance.SpawnBall(Size.Small, transform.position, Vector2.left);
                break;
            case Size.Big:
                SpawnManager.Instance.SpawnBall(Size.Medium, transform.position, Vector2.right);
                SpawnManager.Instance.SpawnBall(Size.Medium, transform.position, Vector2.left);
                break;
        }
    }

    private void HitPlayer(GameObject player)
    {
        HitGround();
        player.GetComponent<Player>().Die();
        GameManager.Instance.Invoke(nameof(GameManager.Instance.LoseGame), 1f);
    }
}
