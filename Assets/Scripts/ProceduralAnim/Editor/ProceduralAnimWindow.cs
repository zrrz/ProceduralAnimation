using UnityEngine;
using System.Collections;
using UnityEditor;

public class ProceduralAnimWindow : EditorWindow {
	static SerializedObject serializedObject;
	static UnityEngine.Object target;

	public static void Init (SerializedObject obj) {
		EditorWindow.GetWindow (typeof (ProceduralAnimWindow)).Show();

		serializedObject = obj;
		target = obj.targetObject;
	}

	public static void CloseWindow () {
		EditorWindow.GetWindow (typeof(ProceduralAnimWindow)).Close ();
	} 

	
	void OnGUI() {
		GUILayout.BeginHorizontal ();
		float width = position.width - 4f;
		GUILayout.BeginVertical (GUILayout.Width(width/2f)); // Left window

		SerializedObject target = new SerializedObject (Selection.activeObject);
		ProceduralAnimData animData = (ProceduralAnimData)target.targetObject;

		for (int i = 0; i < animData.data.Count; i++) {
			GUILayout.Label(animData.data[i].name);
		}

		GUILayout.EndVertical ();
		GUI.DrawTexture (new Rect (width/2f, 0f, 4f, position.height), EditorGUIUtility.whiteTexture);
		GUILayout.BeginVertical (GUILayout.Width(width/2f)); // Right window



		GUILayout.EndVertical ();
		GUILayout.EndHorizontal ();

//		GUILayout.BeginVertical();
//		scrollPos = GUILayout.BeginScrollView(scrollPos,GUILayout.Height(currentScrollViewHeight));
//		for(int i=0;i<20;i++)
//			GUILayout.Label("dfs");
//		GUILayout.EndScrollView();
//		
//		ResizeScrollView();
//		
//		GUILayout.FlexibleSpace();
//		GUILayout.Label("Lower part");
//		
//		GUILayout.EndVertical();
//		Repaint();
	}
	
	private void ResizeScrollView(){
//		GUI.DrawTexture(cursorChangeRect,EditorGUIUtility.whiteTexture);
//		EditorGUIUtility.AddCursorRect(cursorChangeRect,MouseCursor.ResizeVertical);
//		
//		if( Event.current.type == EventType.mouseDown && cursorChangeRect.Contains(Event.current.mousePosition)){
//			resize = true;
//		}
//		if(resize){
//			currentScrollViewHeight = Event.current.mousePosition.y;
//			cursorChangeRect.Set(cursorChangeRect.x,currentScrollViewHeight,cursorChangeRect.width,cursorChangeRect.height);
//		}
//		if(Event.current.type == EventType.MouseUp)
//			resize = false;        
	}
}
