using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralAnim : MonoBehaviour {
	[System.Serializable]
	public class BodyPart {
		public Transform part;
		public List<Quaternion> rotations;
		public List<Vector3> positions;
		
		public BodyPart(Transform p_part, BodyPartData.KeyData keyData) {
			part = p_part;
			positions = keyData.positions;
			rotations = keyData.rotations;
		}
	}
	
	public BodyPartData bodyPartData; 
	
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
		Transform lfoot = lLeg.GetChild (0);

		Transform rUpLeg = root.GetChild (1);
		Transform rLeg = rUpLeg.GetChild (0);
		Transform rfoot = rLeg.GetChild (0);

		Transform spine1 = root.GetChild (2);
		Transform spine2 = spine1.GetChild (0);
		Transform spine3 = spine2.GetChild (0);
		//Transform ribs = spine3.GetChild (0);

		Transform lShoulder = spine3.GetChild (0);
		Transform lUpArm = lShoulder.GetChild (0);
		Transform lForearm = lUpArm.GetChild (0);
		Transform lHand = lForearm.GetChild (0);

		Transform neck1 = spine3.GetChild (1);
		Transform neck2 = neck1.GetChild (0);
		Transform head = neck2.GetChild (0);

		Transform rShoulder = spine3.GetChild (2);
		Transform rUpArm = rShoulder.GetChild (0);
		Transform rForearm = rUpArm.GetChild (0);
		Transform rHand = rForearm.GetChild (0);


		bodyMap = new BodyMap ();

		bodyMap.Add("root", new BodyPart(root, data.keyDataMap["root"]));

		bodyMap.Add("lUpLeg", new BodyPart(lUpLeg, data.keyDataMap["lUpLeg"]));
		bodyMap.Add("lLeg", new BodyPart(lLeg, data.keyDataMap["lLeg"]));
		bodyMap.Add("lfoot", new BodyPart(lfoot, data.keyDataMap["lfoot"]));

		bodyMap.Add("rUpLeg", new BodyPart(rUpLeg, data.keyDataMap["rUpLeg"]));
		bodyMap.Add("rLeg", new BodyPart(rLeg, data.keyDataMap["rLeg"]));
		bodyMap.Add("rfoot", new BodyPart(rfoot, data.keyDataMap["rfoot"]));

		bodyMap.Add("spine1", new BodyPart(spine1, data.keyDataMap["spine1"]));
		bodyMap.Add("spine2", new BodyPart(spine2, data.keyDataMap["spine2"]));
        bodyMap.Add("spine3", new BodyPart(spine3, data.keyDataMap["spine3"]));
		//bodyMap.Add("ribs", new BodyPart(ribs, data.keyDataMap["ribs"]));

		bodyMap.Add("lShoulder", new BodyPart(lShoulder, data.keyDataMap["lShoulder"]));
		bodyMap.Add("lUpArm", new BodyPart(lUpArm, data.keyDataMap["lUpArm"]));
		bodyMap.Add("lForearm", new BodyPart(lForearm, data.keyDataMap["lForearm"]));
		bodyMap.Add("lHand", new BodyPart(lHand, data.keyDataMap["lHand"]));

		bodyMap.Add("neck1", new BodyPart(neck1, data.keyDataMap["neck1"]));
		bodyMap.Add("neck2", new BodyPart(neck2, data.keyDataMap["neck2"]));
		bodyMap.Add("head", new BodyPart(head, data.keyDataMap["head"]));

		bodyMap.Add("rShoulder", new BodyPart(rShoulder, data.keyDataMap["rShoulder"]));
		bodyMap.Add("rUpArm", new BodyPart(rUpArm, data.keyDataMap["rUpArm"]));
		bodyMap.Add("rForearm", new BodyPart(rForearm, data.keyDataMap["rForearm"]));
		bodyMap.Add("rHand", new BodyPart(rHand, data.keyDataMap["rHand"]));
	}
	
	void Update () {
		foreach (KeyValuePair<string, BodyPart> bodyPart in bodyMap) {
			bodyPart.Value.part.transform.localRotation = Quaternion.Lerp(bodyPart.Value.rotations[0], bodyPart.Value.rotations[1], Mathf.Cos(Time.time));
		}
	}

	public BodyPartData data {
		get {
			return bodyPartData;
		}
	}
}
