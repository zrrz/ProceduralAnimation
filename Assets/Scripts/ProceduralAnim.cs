using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralAnim : MonoBehaviour {
	[System.Serializable]
	public class BodyPart {
		public Transform part;
		public List<Quaternion> rotations;
//		public List<Vector3> rotations;
		public List<Vector3> positions;
		
		public BodyPart(Transform p_part, KeyData keyData) {
			part = p_part;
			positions = keyData.positions;
			rotations = keyData.rotations;
		}
	}

	class AnimationPhase {
		public string name;
		public bool mirror;
		public AnimationPhase(string p_name, bool p_mirror) {
			name = p_name; mirror = p_mirror;
		}
	}
	
	public BodyPartData bodyPartData; 

//	[SerializeField]
	BodyMap bodyMap; 
	
	void Start () {
		if (bodyPartData == null) {
			Debug.LogWarning("bodyPartData missing", this);
			enabled = false;
			return;
		}

		Transform root = transform.GetChild (0);

		Transform lUpLeg = root.GetChild (0);
		Transform lLeg = lUpLeg.GetChild (0);
		Transform lFoot = lLeg.GetChild (0);
		Transform lToe = lFoot.GetChild (0);

		Transform rUpLeg = root.GetChild (1);
		Transform rLeg = rUpLeg.GetChild (0);
		Transform rFoot = rLeg.GetChild (0);
		Transform rToe = rFoot.GetChild (0);

		Transform spine1 = root.GetChild (2);
		Transform spine2 = spine1.GetChild (0);
		Transform spine3 = spine2.GetChild (0);

		Transform lShoulder = spine3.GetChild (0);
		Transform lUpArm = lShoulder.GetChild (0);
		Transform lForearm = lUpArm.GetChild (0);
		Transform lHand = lForearm.GetChild (0);

		Transform neck1 = spine3.GetChild (1);
//		Transform neck2 = neck1.GetChild (0);
		Transform head = neck1.GetChild (0);

		Transform rShoulder = spine3.GetChild (2);
		Transform rUpArm = rShoulder.GetChild (0);
		Transform rForearm = rUpArm.GetChild (0);
		Transform rHand = rForearm.GetChild (0);


		bodyMap = new BodyMap ();

		SafeBodyMapAdd (root, "root");

		SafeBodyMapAdd (lUpLeg, "lUpLeg");
		SafeBodyMapAdd (lLeg, "lLeg");
		SafeBodyMapAdd (lFoot, "lFoot");
		SafeBodyMapAdd (lToe, "lToe");

		SafeBodyMapAdd (rUpLeg, "rUpLeg");
		SafeBodyMapAdd (rLeg, "rLeg");
		SafeBodyMapAdd (rFoot, "rFoot");
		SafeBodyMapAdd (rToe, "rToe");

		SafeBodyMapAdd (spine1, "spine1");
		SafeBodyMapAdd (spine2, "spine2");
		SafeBodyMapAdd (spine3, "spine3");

		SafeBodyMapAdd (lShoulder, "lShoulder");
		SafeBodyMapAdd (lUpArm, "lUpArm");
		SafeBodyMapAdd (lForearm, "lForearm");
		SafeBodyMapAdd (lHand, "lHand");

		SafeBodyMapAdd (neck1, "neck1");
//		SafeBodyMapAdd (neck2, "neck2");
		SafeBodyMapAdd (head, "head");

		SafeBodyMapAdd (rShoulder, "rShoulder");
		SafeBodyMapAdd (rUpArm, "rUpArm");
		SafeBodyMapAdd (rForearm, "rForearm");
		SafeBodyMapAdd (rHand, "rHand");

		foreach (KeyValuePair<string, BodyPart> bodyPart in bodyMap.dictionary) {
			bodyPart.Value.part.name += bodyPart.Key;
		}
	}

	float timer = 0f;

	int curAnim = 0;
	int nextAnim = 1;

	const int animCount = 4;
	const float animSpeed = 5f;
	
	void Update () {
		timer += Time.deltaTime * animSpeed;
		foreach (KeyValuePair<string, BodyPart> bodyPart in bodyMap.dictionary) {
			if(bodyPart.Value.rotations.Count >= 4) {
				bodyPart.Value.part.transform.localRotation = Quaternion.Lerp(bodyPart.Value.rotations[curAnim], bodyPart.Value.rotations[nextAnim], timer);
//				bodyPart.Value.part.transform.localEulerAngles = Vector3.Lerp(bodyPart.Value.rotations[0], bodyPart.Value.rotations[1], t);
			}

//			if(bodyPart.Value.positions.Count >= 2)
//				bodyPart.Value.part.transform.localPosition = Vector3.Lerp(bodyPart.Value.positions[0], bodyPart.Value.positions[1], t);
		}

		if(timer > 1f) {
			timer = 0f;
			curAnim++;
			nextAnim++;

			if(curAnim >= animCount) {
				curAnim = 0;
			}
			if(curAnim >= animCount - 1) {
				nextAnim = 0;
			}
		}
	}

	public BodyPartData data {
		get {
			return bodyPartData;
		}
	}

	void SafeBodyMapAdd(Transform bodyPart, string key) {
		if(data.keyDataMap.dictionary.ContainsKey(key))
			bodyMap.Add(key, new BodyPart(bodyPart, data.keyDataMap[key]));
	}
}
