using UnityEngine;
using System.Collections;
using System;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;

//public class AnimationProcessor : AssetPostprocessor
//{
//	string fileSuffix = "_evts.txt";    // This will search for a file with the same name as the asset but ending in _evts.txt
//	// For example if the asset is Skeleton@walk.fbx it will search for Skeleton@walk_evts.txt
//	
//	Dictionary<string, List<AnimationEvent>> clipEvents;
//	List<AnimationClip> animationClipList;
//	
//	void OnPostprocessModel(GameObject g)
//	{
//		if (assetPath.Contains("@"))    // The script will only process assets with an @ in the name (animation convention). Just change this if you wish
//		{
//			clipEvents = new Dictionary<string, List<AnimationEvent>>();
//			
//			ModelImporter modelImporter = (ModelImporter)assetImporter;
//
//		}
//	}
//}

public class FBXConstraintImport : EditorWindow {

	System.Reflection.Assembly[] assemblies;

	Vector2 scrollPosition = Vector2.zero;

	// Add menu named "My Window" to the Window menu
	[MenuItem ("FBX/FBXConstraintImport")]
	static void Init () {
		// Get existing open window or if none, make a new one:
		FBXConstraintImport window = (FBXConstraintImport)EditorWindow.GetWindow (typeof (FBXConstraintImport));
		window.Show();
	}
	
	void OnGUI () {
		if(GUILayout.Button("Load assemblies", GUILayout.MaxWidth(EditorGUIUtility.labelWidth))) {
			assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			Type tidObject = typeof(ModelImporter);
			BindingFlags bindingFlags =  BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
			MethodInfo[] aminfo = tidObject.GetMethods( bindingFlags );

//			Type modelImporter = typeof(ModelImporter);
//			ConstructorInfo modelImporterConstructor = modelImporter.GetConstructor(Type.EmptyTypes);
//			object modelImporterClassObject = modelImporterConstructor.Invoke(new object[]{});

//			MethodInfo mi = modelImporter.GetMethod("get_sourceAvatarInternal", bindingFlags);

//			Debug.Log("print");

//			Avatar a = new ModelImporter().sourceAvatar;
//
//			GameObject go = new GameObject();
//			go.AddComponent<Animator>();
//			go.GetComponent<Animator>().avatar = a;
			
//			object avatar = mi.Invoke(modelImporterClassObject, new object[]{}); 
			//-------------------------
//			GameObject.Instantiate((UnityEngine.Object)obj);

			Debug.Log( "###################  METHODS FOR - " + tidObject.Name );
			
			foreach( MethodInfo minf in aminfo )
			{
				string strParamList = "()";             
				
				ParameterInfo[] apinfo = minf.GetParameters();
				if( 0 < apinfo.Length )
				{
					strParamList = "( ";
					foreach( ParameterInfo pinfo in apinfo )
					{
						strParamList += pinfo.ParameterType.Name + ", ";
					}
					strParamList += ")";
				}
				Debug.Log(minf.ReturnType.Name + " " + minf.Name + strParamList );
			}
		}
		EditorGUILayout.Space();
		if(null != assemblies) {
			scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
			assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			foreach (var A in assemblies) {
				if(GUILayout.Button(A.GetLoadedModules()[0].Name)) {
					Type[] types = A.GetTypes();//GetAllSubTypes();
					foreach (Type type in types) {
						Debug.Log(type.Name);
						if(type.Name.ToString().Contains("FBX"))
							Debug.LogWarning(type.ToString());
					}
				}
//				AssetImporter.GetAtPath
//				Debug.Log("Done");
			}
			EditorGUILayout.EndScrollView();
//			EditorGUIUtility.ExitGUI();
		}
	}


	public static System.Type[] GetAllSubTypes(System.Type aBaseClass) {
		var result = new System.Collections.Generic.List<System.Type>();
		System.Reflection.Assembly[] AS = System.AppDomain.CurrentDomain.GetAssemblies();
		foreach (var A in AS)
		{
			System.Type[] types = A.GetTypes();
			foreach (var T in types)
			{

				if (T.IsSubclassOf(aBaseClass))
					result.Add(T);
			}
		}
		return result.ToArray();
	}
}
