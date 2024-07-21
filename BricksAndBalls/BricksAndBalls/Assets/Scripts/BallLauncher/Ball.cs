using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigidBody;
	
	private BallLauncher ballLauncher;
	private Tweener tweener;
	private bool isLunched = false;
	
	public void Launch(Vector2 direction, float force)
	{
		isLunched = true;
		rigidBody.isKinematic = false;
		rigidBody.AddForce(direction * force, ForceMode2D.Impulse);
	}

	public void Init(BallLauncher ballLauncher)
	{
		this.ballLauncher = ballLauncher;
	}

	private void FixedUpdate()
	{
		if (!isLunched)
		{
			return;
		}
		rigidBody.AddForce(Vector2.down * 0.1f);
	}

	public void MoveToPos(Vector2 pos)
	{
		transform.DOMove(pos, 0.1f).Play();
	}

	public void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("BottomWall"))
		{
			isLunched = false;
			rigidBody.velocity = Vector2.zero;
			rigidBody.angularVelocity = 0;
			rigidBody.isKinematic = true;
			ballLauncher.SetNewPosition(this);
		}
	}

	public void OnDestroy()
	{
		if (tweener != null)
		{
			tweener.Kill();
			tweener = null;
		}
	}
}
