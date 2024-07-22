using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimDots : MonoBehaviour
{

	[SerializeField] private GameObject dotPrefab;
	[SerializeField] private GameObject launchPoint;
	[SerializeField] private int numberOfDots = 10;
	[SerializeField] private float dotSpacing = 0.5f;
	[SerializeField] private LayerMask wallLayer;

	private List<GameObject> dots;

	private void Awake()
	{
		InitDots();
	}

	public void UpdateDots(Vector2 direction, bool isAiming)
	{
		Vector2 currentPos = launchPoint.transform.position;
		Vector2 currentDir = direction;
		if (!isAiming)
		{
			foreach (GameObject dot in dots)
			{
				dot.SetActive(false);
			}
		}
		else
		{
			for (int i = 0; i < numberOfDots; i++)
			{
				dots[i].transform.position = currentPos;
				dots[i].SetActive(true);

				RaycastHit2D hit = Physics2D.Raycast(currentPos, currentDir, dotSpacing, wallLayer);
				if (hit.collider)
				{
					currentDir = Vector2.Reflect(currentDir, hit.normal);
					currentPos = hit.point + currentDir * 0.01f;
				}
				else
				{
					currentPos += currentDir * dotSpacing;
				}
			}
		}
	}

	private void InitDots()
	{
		dots = new List<GameObject>();
		for (int i = 0; i < numberOfDots; i++)
		{
			GameObject dot = Instantiate(dotPrefab, transform);
			dot.SetActive(false);
			dots.Add(dot);
		}
	}
	

}
