using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.IO;

public class ProceduralAnimEditor : MonoBehaviour {

	[MenuItem ("ProceduralAnim/New AnimData")]
	static void CreateAsset() {
		ProceduralAnimData asset = ScriptableObject.CreateInstance<ProceduralAnimData> ();
		
		string path = AssetDatabase.GetAssetPath (Selection.activeObject);
		if (path == "")  {
			path = "Assets";
		} else if (Path.GetExtension (path) != "") {
			path = path.Replace (Path.GetFileName (AssetDatabase.GetAssetPath (Selection.activeObject)), "");
		}
		
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath (path + "/New " + typeof(ProceduralAnimData).ToString() + ".asset");
		
		AssetDatabase.CreateAsset (asset, assetPathAndName);
		
		AssetDatabase.SaveAssets ();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow ();
		Selection.activeObject = asset;
	}
}

[CustomEditor(typeof(ProceduralAnimData))]
public class BodyPartDataEditor : Editor {

	static bool editing = false;
	static bool lockInspector = false; 

	static string lastLevel = null;

	int selectedBodyPartData = 0;

	void EditData() {
		if (!EditorApplication.isUpdating) {
			lastLevel = EditorApplication.currentScene;
			editing = true;

			EditorApplication.NewScene ();
			Selection.activeObject = target;
			lockInspector = true;

			SpawnSampleModel();
			ProceduralAnimWindow.Init(serializedObject);
			EditorGUIUtility.ExitGUI();
		}
	}

	void SpawnSampleModel() {
		GameObject original = (GameObject)EditorGUIUtility.Load("Avatar/DefaultAvatar.fbx");
		GameObject obj = (GameObject)Instantiate(original, Vector3.zero, Quaternion.identity);	
	}

	void ToggleInspectorLock() {
		Type type = Assembly.GetAssembly(typeof(Editor)).GetType("UnityEditor.InspectorWindow");
		UnityEngine.Object[] findObjectsOfTypeAll = Resources.FindObjectsOfTypeAll(type);
		EditorWindow _mouseOverWindow = (EditorWindow)findObjectsOfTypeAll[0];
		
		if (_mouseOverWindow != null && _mouseOverWindow.GetType().Name == "InspectorWindow") {
			PropertyInfo propertyInfo = type.GetProperty("isLocked");
			bool value = (bool)propertyInfo.GetValue(_mouseOverWindow, null);
			propertyInfo.SetValue(_mouseOverWindow, !value, null);
			_mouseOverWindow.Repaint();
		}
		lockInspector = false;
	}

	void ListAnimations(ProceduralAnimData data) {
		List<string> animationNames = new List<string> ();
		for (int i = 0; i < data.data.Count; i++) {
			animationNames.Add(data.data[i].name);
		}

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.BeginVertical(GUILayout.Width(30f));
		for(int i = 0; i < data.data.Count; i++) {
			if(GUILayout.Button("-", GUI.skin.button)) {
				data.data.RemoveAt(i);
			}
		}
		EditorGUILayout.EndVertical();
		selectedBodyPartData = GUILayout.SelectionGrid(selectedBodyPartData, animationNames.ToArray(), 1);
		EditorGUILayout.EndHorizontal();
		
		if(GUILayout.Button("+", EditorStyles.miniButton, GUILayout.Width(40f))) {
			AddAnimation(data);
		}
	}

	void AddAnimation(ProceduralAnimData data) {
		BodyPartData partData = new BodyPartData ();
		data.data.Add (partData);
	}

	void DrawInformationWindow(ProceduralAnimData data) {
		data.data[selectedBodyPartData].name = EditorGUILayout.TextField("Name", data.data[selectedBodyPartData].name);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("data").GetArrayElementAtIndex(selectedBodyPartData).FindPropertyRelative("animations"), true);
	}

	void DrawAnimationListWindow(ProceduralAnimData data) {
		if (GUILayout.Button ("Done")) {
			Debug.Log("done");
			if (!EditorApplication.isUpdating) {
				editing = false;
				lockInspector = true;
				EditorApplication.OpenScene(lastLevel);
				ProceduralAnimWindow.CloseWindow();
				EditorGUIUtility.ExitGUI();
			}
		}
		ListAnimations(data);
	}

	void ParseAnimations() {/*
		for(int k = 0; k < data.data.Count; k++) {
			BodyPartData partData = data.data[k];
			
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
			
//			for (int i = 0; i < partData.animations.Count; i++) {
//				AnimationClipCurveData[] curveData = AnimationUtility.GetAllCurves(partData.animations [i], true);
//
//				int j = 0;
//				keyDataMap ["root"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));						
//				keyDataMap ["spine1"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["spine2"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["spine3"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["neck1"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["head"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["lShoulder"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["lUpArm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["lForearm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["lHand"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				j += 20;
//				keyDataMap ["rShoulder"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["rUpArm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["rForearm"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["rHand"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				j += 20;
//				keyDataMap ["lUpLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["lLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["lFoot"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["lToe"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
////				j += 4; // Left toes
//				keyDataMap ["rUpLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["rLeg"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["rFoot"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//				keyDataMap ["rToe"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//
////				j += 8; // Right toes
////				j += 4; // Ribs
////				keyDataMap ["neck2"].AddRotation(new Quaternion(KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++]), KeyValue(curveData [j++])));
//		}
			
			AnimationClipCurveData[] curveData1 = AnimationUtility.GetAllCurves(partData.animations[0], true);
			for(int i = 0; i < curveData1.Length; i++) {
				Debug.Log(curveData1[i].path + " " + curveData1[i].propertyName + " " + KeyValue(curveData1[i]));
			}
			
			partData.keyDataMap = keyDataMap;
		}*/

	}

	public override void OnInspectorGUI() {
		if (lockInspector) {
			ToggleInspectorLock ();
		}

		ProceduralAnimData data = (ProceduralAnimData)target;

		if (editing) {
			DrawAnimationListWindow(data);
			EditorGUILayout.Separator();
			DrawInformationWindow(data);
		} else {
			if (GUILayout.Button ("Edit Animations")) {
				EditData();
			}

			if (GUILayout.Button("Parse Animations")) {
				ParseAnimations();
			}
		}
		serializedObject.ApplyModifiedProperties();
	}

	float KeyValue(AnimationClipCurveData curveData) {
		return curveData.curve.keys [0].value;
	}
}
