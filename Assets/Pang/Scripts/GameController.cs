using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    [SerializeField] private float moveThreshold = 1f;
    [SerializeField] private GameObject linePrefab;
    private Player controlledPlayer;
    private Vector3 initialPosition;

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

    private void Update()
    {
        if (!controlledPlayer || controlledPlayer.isDead || GameManager.Instance.gameState != GameManager.State.Running) return;

        if (Input.touchCount > 1) return;

        if (Input.GetMouseButtonDown(0))
        {
            initialPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            float diff = Input.mousePosition.x - initialPosition.x;
            float ratio = Mathf.Abs(diff) / Screen.width;
            if (ratio > moveThreshold)
            {
                if (diff > 0)
                {
                    controlledPlayer.Move(Player.MoveDirection.Right);
                    initialPosition.x = Input.mousePosition.x - (moveThreshold * Screen.width * 1.1f);
                }

                if (diff < 0)
                {
                    controlledPlayer.Move(Player.MoveDirection.Left);
                    initialPosition.x = Input.mousePosition.x + (moveThreshold * Screen.width * 1.1f);
                }
                
            }
        }
        
        // DEBUG PURPOSES
        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }
    
    public void SetControlledPlayer(Player player)
    {
        controlledPlayer = player;
    }

    public void Shoot()
    {
        if (!controlledPlayer || controlledPlayer.isDead) return;

        SpawnManager.Instance.SpawnSpear(controlledPlayer.transform.position);
    }
}
