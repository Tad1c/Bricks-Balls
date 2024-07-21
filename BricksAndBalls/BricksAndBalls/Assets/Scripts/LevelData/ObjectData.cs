using System;
using UnityEngine;

[Serializable]
public struct ObjectData
{
	public string PrefabName;
	public Vector3 Position;
	public Quaternion Rotation;
	public Vector3 Scale;
	public int Health;
}