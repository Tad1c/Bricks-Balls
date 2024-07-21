using System;
using UnityEngine;

public class WallsManager : MonoBehaviour
{

	private Camera cam;

	[SerializeField] private float heightOffset;
	
	[SerializeField] private Transform leftWall;
	[SerializeField] private Transform rightWall;
	[SerializeField] private Transform upperWall;
	[SerializeField] private Transform bottomWall;

	private void Awake()
	{
		cam = Camera.main;
	}

	[ContextMenu("SetWalls")]
	private void SetWallsBasedOnScreenRes()
	{
		cam = Camera.main;
		float camHeight = 2 * cam.orthographicSize;
		float camWidth = camHeight * cam.aspect;

		upperWall.position = new Vector2(0, camHeight / heightOffset);
		upperWall.localScale = new Vector2(1, camWidth);
		
		bottomWall.position = new Vector2(0, -camHeight / heightOffset);
		bottomWall.localScale = new Vector2(1, camWidth);
		
		leftWall.position = new Vector2(-camWidth / 2, 0);
		rightWall.position = new Vector2(camWidth / 2, 0);
	}
}
