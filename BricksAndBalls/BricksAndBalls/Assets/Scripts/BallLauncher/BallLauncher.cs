using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using VContainer;

public class BallLauncher : MonoBehaviour
{
	[SerializeField] private Ball ballPrefab;

	[SerializeField] private AimDots aimDots;
	[SerializeField] private int numberOfBalls;
	[SerializeField] private float force;

	[SerializeField] private float delayShootBetweenBalls;

	[SerializeField] private Transform launchPoint;
	[SerializeField] private float maxAngle = 45f;

	private Tweener tween;
	private List<Ball> ballPrefabs = new List<Ball>();
	private Camera cam;
	private bool newPosSet = false;
	private Vector2 tempPosition;
	private int ballsRetrieved = 0;
	private bool canShoot = true;
	private ICameraSettings cameraSettings;
	private GamePlayEvents gamePlayEvents;

	[Inject]
	public void Constructor(ICameraSettings cameraSettings, GamePlayEvents gamePlayEvents)
	{
		this.cameraSettings = cameraSettings;
		this.gamePlayEvents = gamePlayEvents;
		
		cam = cameraSettings.GetMainCamera();
	}

	private void Awake()
	{
		InitBalls();
	}

	private void Update()
	{
		if (!canShoot)
		{
			return;
		}

		if (Input.GetMouseButton(0))
		{
			TryToAim();
		}

		if (Input.GetMouseButtonUp(0))
		{
			TryToShoot();
		}
	}

	private void TryToAim()
	{
		if (!CanGetLaunchDir(out Vector2 dir))
		{
			aimDots.UpdateDots(dir, false);
			return;
		}

		aimDots.UpdateDots(dir, true);
	}

	private void TryToShoot()
	{
		if (!CanGetLaunchDir(out Vector2 dir))
		{
			return;
		}
			
		LaunchBalls(dir).Forget();
		newPosSet = false;
		canShoot = false;
		aimDots.UpdateDots(dir, false);
	}

	private async UniTaskVoid LaunchBalls(Vector2 launchDirection)
	{
		foreach (Ball ball in ballPrefabs)
		{
			ball.transform.position = launchPoint.transform.position;
			await UniTask.Delay(TimeSpan.FromSeconds(delayShootBetweenBalls));
			ball.Launch(launchDirection, force);
		}
	}

	private void InitBalls()
	{
		for (int i = 0; i < numberOfBalls; i++)
		{
			CreatBall();
		}
	}

	private void CreatBall()
	{
		Ball ball = Instantiate(ballPrefab, launchPoint.position, Quaternion.identity, transform);
		ball.Init(this);
		ballPrefabs.Add(ball);
	}

	public void SetNewPosition(Ball ball)
	{
		if (!newPosSet)
		{
			tempPosition = ball.transform.position;
			newPosSet = true;
		}
		else
		{
			ball.MoveToPos(tempPosition);
		}

		BallReturned();
	}

	private void BallReturned()
	{
		ballsRetrieved++;
		if (ballsRetrieved == ballPrefabs.Count)
		{
			canShoot = true;
			launchPoint.position = tempPosition;
			ballsRetrieved = 0;
			gamePlayEvents.OnRoundEnd?.Invoke();
		}
	}

	private bool CanGetLaunchDir(out Vector2 dir)
	{
		Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

		if (!cameraSettings.IsWithinBoardBounds(mousePosition))
		{
			dir = Vector2.zero;
			return false;
		}

		Vector2 launchDirection = (mousePosition - (Vector2)launchPoint.position).normalized;

		float angle = Vector2.SignedAngle(Vector2.up, launchDirection);

		angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

		dir = Quaternion.Euler(0, 0, angle) * Vector2.up;
		return true;
	}
}