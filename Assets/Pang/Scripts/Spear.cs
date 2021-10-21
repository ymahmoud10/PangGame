using System;
using UnityEngine;

public class Spear : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    
    private float elapsedTime = 0f;
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private float distanceCheckThreshold = 0.01f;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
        
        initialPosition = transform.position;
        targetPosition = new Vector3(transform.position.x, 7, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        float duration = 1f / moveSpeed;
        
        if (Vector3.Distance(transform.position, targetPosition) > distanceCheckThreshold)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
