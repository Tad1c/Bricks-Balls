using UnityEngine;

public interface ICameraSettings
{
	public bool IsWithinBoardBounds(Vector3 position);
	public Camera GetMainCamera();
}
