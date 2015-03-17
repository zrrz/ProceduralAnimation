using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BodyPartData {
	public BodyPartData() {
		name = "animation";
		animations = new List<AnimationClip> ();
	}
	public string name;
	public List<AnimationClip> animations;
	[SerializeField]
	public KeyDataMap keyDataMap;
}

[System.Serializable]
public class KeyData {
	public List<Quaternion> rotations;
//	public List<Vector3> rotations;
	public List<Vector3> positions;
	public KeyData() {
		rotations = new List<Quaternion>();
//		rotations = new List<Vector3>();
		positions = new List<Vector3>();
	}
	
	public void AddRotation(Quaternion rot) {
		rotations.Add (rot);
	}
	
	public void AddPosition(Vector3 pos) {
		positions.Add (pos);
	}
}