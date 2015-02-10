using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class ProceduralAnimEditor : MonoBehaviour {

	[MenuItem ("ProceduralAnim/New PartData")]
	static void CreateAsset() {
		BodyPartData data = ScriptableObject.CreateInstance<BodyPartData>();
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/BodyPartData.asset");
		AssetDatabase.CreateAsset(data, assetPathAndName);
		
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = data;
	}
}

[CustomEditor(typeof(BodyPartData))]
public class BodyPartDataEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		BodyPartData data = (BodyPartData)target;

		if (data.keyDataMap == null) {
			GUILayout.Label("Not Parsed");
		} else {
			GUILayout.Label("Parsed!");
		}

		if (GUILayout.Button("Parse Animations")) {
			Dictionary<string, BodyPartData.KeyData> keyDataMap = new Dictionary<string, BodyPartData.KeyData>();

			keyDataMap.Add("root", new BodyPartData.KeyData());

			keyDataMap.Add("lUpLeg", new BodyPartData.KeyData());
			keyDataMap.Add("lLeg", new BodyPartData.KeyData());
			keyDataMap.Add("lfoot", new BodyPartData.KeyData());
			
			keyDataMap.Add("rUpLeg", new BodyPartData.KeyData());
			keyDataMap.Add("rLeg", new BodyPartData.KeyData());
			keyDataMap.Add("rfoot", new BodyPartData.KeyData());
			
			keyDataMap.Add("spine1", new BodyPartData.KeyData());
			keyDataMap.Add("spine2", new BodyPartData.KeyData());
			keyDataMap.Add("spine3", new BodyPartData.KeyData());
			keyDataMap.Add("ribs", new BodyPartData.KeyData());
			
			keyDataMap.Add("lShoulder", new BodyPartData.KeyData());
			keyDataMap.Add("lUpArm", new BodyPartData.KeyData());
			keyDataMap.Add("lForearm", new BodyPartData.KeyData());
			keyDataMap.Add("lHand", new BodyPartData.KeyData());
			
			keyDataMap.Add("neck1", new BodyPartData.KeyData());
			keyDataMap.Add("neck2", new BodyPartData.KeyData());
			keyDataMap.Add("head", new BodyPartData.KeyData());
			
			keyDataMap.Add("rShoulder", new BodyPartData.KeyData());
			keyDataMap.Add("rUpArm", new BodyPartData.KeyData());
			keyDataMap.Add("rForearm", new BodyPartData.KeyData());
			keyDataMap.Add("rHand", new BodyPartData.KeyData());

			for (int i = 0; i < data.animations.Count; i++) {
				AnimationClipCurveData[] curveData = AnimationUtility.GetAllCurves(data.animations [i], true);

				int j = 0;
				keyDataMap ["root"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));						
				keyDataMap ["spine1"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["spine2"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["spine3"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["neck1"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["neck2"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["head"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lShoulder"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lUpArm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lForearm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lHand"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				j += 56; // I THINK. HELP
				keyDataMap ["rShoulder"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rUpArm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rForearm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rHand"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				j += 56; // I THINK. HELP
				keyDataMap ["lUpLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lfoot"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rUpLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rfoot"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
			}

//			AnimationClipCurveData[] curveData = AnimationUtility.GetAllCurves(data.animations[0], true);
//			for(int i = 0; i < curveData.Length; i++) {
//				Debug.Log(curveData[i].path + " " + curveData[i].propertyName + " " + KeyValue(curveData[i]));
//			}

			data.keyDataMap = keyDataMap;

			serializedObject.ApplyModifiedProperties();
		}
	}

	float KeyValue(AnimationClipCurveData curveData) {
		return curveData.curve.keys [0].value;
	}
}
