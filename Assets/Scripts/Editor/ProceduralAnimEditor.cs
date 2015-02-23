using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class ProceduralAnimEditor : MonoBehaviour {

	[MenuItem ("ProceduralAnim/New PartData")]
	static void CreateAsset() {
		BodyPartData data = ScriptableObject.CreateInstance<BodyPartData>();
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/asset");
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
			KeyDataMap keyDataMap = new KeyDataMap();

			keyDataMap.Add("root", new KeyData());

			keyDataMap.Add("lUpLeg", new KeyData());
			keyDataMap.Add("lLeg", new KeyData());
			keyDataMap.Add("lFoot", new KeyData());
			keyDataMap.Add("lToe", new KeyData());
			
			keyDataMap.Add("rUpLeg", new KeyData());
			keyDataMap.Add("rLeg", new KeyData());
			keyDataMap.Add("rFoot", new KeyData());
			keyDataMap.Add("rToe", new KeyData());
			
			keyDataMap.Add("spine1", new KeyData());
			keyDataMap.Add("spine2", new KeyData());
			keyDataMap.Add("spine3", new KeyData());
//			keyDataMap.Add("ribs", new KeyData());
			
			keyDataMap.Add("lShoulder", new KeyData());
			keyDataMap.Add("lUpArm", new KeyData());
			keyDataMap.Add("lForearm", new KeyData());
			keyDataMap.Add("lHand", new KeyData());
			
			keyDataMap.Add("neck1", new KeyData());
//			keyDataMap.Add("neck2", new KeyData());
			keyDataMap.Add("head", new KeyData());
			
			keyDataMap.Add("rShoulder", new KeyData());
			keyDataMap.Add("rUpArm", new KeyData());
			keyDataMap.Add("rForearm", new KeyData());
			keyDataMap.Add("rHand", new KeyData());

			for (int i = 0; i < data.animations.Count; i++) {
				AnimationClipCurveData[] curveData = AnimationUtility.GetAllCurves(data.animations [i], true);

				int j = 0;
				keyDataMap ["root"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));						
				keyDataMap ["spine1"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["spine2"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["spine3"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["neck1"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["head"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lShoulder"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lUpArm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lForearm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lHand"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				j += 20;
				keyDataMap ["rShoulder"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rUpArm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rForearm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rHand"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				j += 20;
				keyDataMap ["lUpLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lFoot"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["lToe"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				j += 4; // Left toes
				keyDataMap ["rUpLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rFoot"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
				keyDataMap ["rToe"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));

//				j += 8; // Right toes
//				j += 4; // Ribs
//				keyDataMap ["neck2"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
			}

//			AnimationClipCurveData[] curveData1 = AnimationUtility.GetAllCurves(data.animations[0], true);
//			for(int i = 0; i < curveData1.Length; i++) {
//				Debug.Log(curveData1[i].path + " " + curveData1[i].propertyName + " " + KeyValue(curveData1[i]));
//			}

			data.keyDataMap = keyDataMap;

			serializedObject.ApplyModifiedProperties();
		}
	}

	float KeyValue(AnimationClipCurveData curveData) {
		return curveData.curve.keys [0].value;
	}
}
