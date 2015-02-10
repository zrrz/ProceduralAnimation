using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BodyPartData : ScriptableObject {
	public List<AnimationClip> animations;
	[SerializeField]
	public Dictionary<string, KeyData> keyDataMap;

	public class KeyData {
		public List<Quaternion> rotations;
		public List<Vector3> positions;
		public KeyData() {
			rotations = new List<Quaternion>();
			positions = new List<Vector3>();
		}

		public void AddRotation(Quaternion rot) {
			rotations.Add (rot);
		}

		public void AddPosition(Vector3 pos) {
			positions.Add (pos);
		}
	}
}
