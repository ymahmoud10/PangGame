using UnityEngine;

public class Player : MonoBehaviour
{
	public enum MoveDirection { Right, Left }
	
	[SerializeField] private float moveSpeed = 5f;
	internal bool isDead = false;
	
	private Animator animator;
	private Rigidbody2D thisRigidbody2D;
	private bool isFacingRight = true;
	
	private static readonly int AnimatorSpeedParam = Animator.StringToHash("Speed");

	private void Start()
	{
		animator = GetComponent<Animator>();
		thisRigidbody2D = GetComponent<Rigidbody2D>();

		GameController.Instance.SetControlledPlayer(this);
	}

	private void Update()
	{
		if (GameManager.Instance.gameState != GameManager.State.Running) return;

		animator.SetFloat(AnimatorSpeedParam, thisRigidbody2D.velocity.sqrMagnitude);
	}
	
	public void Move(MoveDirection dir)
	{
		float speed = dir == MoveDirection.Right ? moveSpeed : -moveSpeed;
		// Giving player rigidbody velocity
		thisRigidbody2D.velocity = new Vector2(speed, thisRigidbody2D.velocity.y);

		// If the input is moving the player right and the player is facing left
		if (speed > 0 && !isFacingRight)
		{
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right
		else if (speed < 0 && isFacingRight)
		{
			Flip();
		}
	}

	private void Flip()
	{
		// Switch direction flag
		isFacingRight = !isFacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	public void Die()
	{
		isDead = true;
		animator.Play("Player_Die");
	}
}
