using System;
using UnityEngine;

public class CameraSettings : MonoBehaviour
{
	[SerializeField] private Transform leftWall;
	[SerializeField] private Transform rightWall;
	[SerializeField] private Transform topWall;
	[SerializeField] private Transform bottomWall;


	private void Awake()
	{
		Application.targetFrameRate = 60;
		AdjustCameraSize();
	}

	[ContextMenu("SetSize")]
	private void AdjustCameraSize()
	{
		float gameAreaWidth = rightWall.position.x - leftWall.position.x;
		float gameAreaHeight = topWall.position.y - bottomWall.position.y;

		float targetAspectRation = gameAreaWidth / gameAreaHeight;

		float screenAspectRation = (float)Screen.width / (float)Screen.height;

		Camera cam = Camera.main;

		if (screenAspectRation >= targetAspectRation)
		{
			cam.orthographicSize = gameAreaHeight / 2;
		}
		else
		{
			float differenceInSize = targetAspectRation / screenAspectRation;
			cam.orthographicSize = gameAreaHeight / 2 * differenceInSize;
		}

		cam.transform.position = new Vector3(
			(leftWall.position.x + rightWall.position.x) / 2,
			(topWall.position.y + bottomWall.position.y) / 2,
			cam.transform.position.z);
	}

	public bool IsWithinBoardBounds(Vector3 position)
	{
		return position.x >= leftWall.position.x && position.x <= rightWall.position.x &&
		       position.y >= bottomWall.position.y && position.y <= topWall.position.y;
	}
}