using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Reflection;

public class CustomSelectionGrid : Editor {
	public static int DoButtonGrid(int selected, string[] texts, int xCount, params GUILayoutOption[] options) {
		GUIContent[] array = new GUIContent[texts.Length];
		for (int i = 0; i < texts.Length; i++) {
			array [i] = new GUIContent(texts [i]);
		}

		return DoButtonGrid(selected, array, xCount, GUI.skin.button, options);
	}

	public static  int DoButtonGrid(int selected, GUIContent[] contents, int xCount, GUIStyle style, params GUILayoutOption[] options) {
		Rect rect = (Rect)System.Type.GetType("GUIGridSizer").GetMethod("GetRect").Invoke(null, new object[]
		{
			contents,
			xCount,
			style,
			options
		});
		return DoButtonGrid(rect, selected, contents, xCount, style);
	}

	public static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style) {
		if (style == null) {
			style = GUI.skin.button;
		}
		return ButtonGrid(position, selected, contents, xCount, style, style, style, style);
	}

	static int ButtonGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle) {
		BindingFlags bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
		typeof(GUIUtility).GetMethod("CheckOnGUI", bindingFlags).Invoke(null, new Object[]{});
		int num = contents.Length;
		if (num == 0) {
			return selected;
		}
		if (xCount <= 0) {
			Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set xCount to a positive value.");
			return selected;
		}
		int controlID = GUIUtility.GetControlID((int)typeof(GUI).GetProperty("buttonGridHash", bindingFlags).GetValue(null, null), FocusType.Native, position);
		int num2 = num / xCount;
		if (num % xCount != 0) {
			num2++;
		}
		float num3 = (float)CalcTotalHorizSpacing(xCount, style, firstStyle, midStyle, lastStyle);
		float num4 = (float)(Mathf.Max(style.margin.top, style.margin.bottom) * (num2 - 1));
		float elemWidth = (position.width - num3) / (float)xCount;
		float elemHeight = (position.height - num4) / (float)num2;
		if (style.fixedWidth != 0f) {
			elemWidth = style.fixedWidth;
		}
		if (style.fixedHeight != 0f) {
			elemHeight = style.fixedHeight;
		}
		switch (Event.current.GetTypeForControl(controlID)) {
			case EventType.MouseDown:
				if (position.Contains(Event.current.mousePosition)) {
					Rect[] array = CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
					if (GetButtonGridMouseSelection(array, Event.current.mousePosition, true) != -1) {
						GUIUtility.hotControl = controlID;
						Event.current.Use();
					}
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == controlID) {
					GUIUtility.hotControl = 0;
					Event.current.Use();
					Rect[] array = CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
					int buttonGridMouseSelection = GetButtonGridMouseSelection(array, Event.current.mousePosition, true);
					GUI.changed = true;
					return buttonGridMouseSelection;
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == controlID) {
					Event.current.Use();
				}
				break;
			case EventType.Repaint:
				{
					GUIStyle gUIStyle = null;
					System.Type.GetType("GUIClip").GetMethod("Push", bindingFlags).Invoke(null, new System.Object[]
					{
						position,
						Vector2.zero,
						Vector2.zero,
						false
					});
					position = new Rect(0f, 0f, position.width, position.height);
					Rect[] array = CalcMouseRects(position, num, xCount, elemWidth, elemHeight, style, firstStyle, midStyle, lastStyle, false);
					int buttonGridMouseSelection2 = GetButtonGridMouseSelection(array, Event.current.mousePosition, controlID == GUIUtility.hotControl);
					bool flag = position.Contains(Event.current.mousePosition);
					object mouseUsed = (bool)typeof(GUIUtility).GetProperty("mouseUsed", bindingFlags).GetValue(null, null) | flag;
					typeof(GUIUtility).GetProperty("mouseUsed", bindingFlags).SetValue(null, mouseUsed, null);
					for (int i = 0; i < num; i++) {
						GUIStyle gUIStyle2;
						if (i != 0) {
							gUIStyle2 = midStyle;
						} else {
							gUIStyle2 = firstStyle;
						}
						if (i == num - 1) {
							gUIStyle2 = lastStyle;
						}
						if (num == 1) {
							gUIStyle2 = style;
						}
						if (i != selected) {
							gUIStyle2.Draw(array [i], contents [i], i == buttonGridMouseSelection2 && (GUI.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl && GUI.enabled, false, false);
						} else {
							gUIStyle = gUIStyle2;
						}
					}
					if (selected < num && selected > -1) {
						gUIStyle.Draw(array [selected], contents [selected], selected == buttonGridMouseSelection2 && (GUI.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl, true, false);
					}
					if (buttonGridMouseSelection2 >= 0) {
						GUI.tooltip = contents [buttonGridMouseSelection2].tooltip;
					}
					System.Type.GetType("GUIClip").GetMethod("Pop", bindingFlags).Invoke(null, new Object[]{});
					break;
				}
		}
		return selected;
	}

	// Ripped from GUI assembly
	public static int CalcTotalHorizSpacing(int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle) {
		if (xCount < 2) {
			return 0;
		}
		if (xCount == 2) {
			return Mathf.Max(firstStyle.margin.right, lastStyle.margin.left);
		}
		int num = Mathf.Max(midStyle.margin.left, midStyle.margin.right);
		return Mathf.Max(firstStyle.margin.right, midStyle.margin.left) + Mathf.Max(midStyle.margin.right, lastStyle.margin.left) + num * (xCount - 3);
	}

	// Ripped from GUI assembly
	static Rect[] CalcMouseRects(Rect position, int count, int xCount, float elemWidth, float elemHeight, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, bool addBorders) {
		int num = 0;
		int num2 = 0;
		float num3 = position.xMin;
		float num4 = position.yMin;
		GUIStyle gUIStyle = style;
		Rect[] array = new Rect[count];
		if (count > 1) {
			gUIStyle = firstStyle;
		}
		for (int i = 0; i < count; i++) {
			if (!addBorders) {
				array [i] = new Rect(num3, num4, elemWidth, elemHeight);
			} else {
				array [i] = gUIStyle.margin.Add(new Rect(num3, num4, elemWidth, elemHeight));
			}
			array [i].width = Mathf.Round(array [i].xMax) - Mathf.Round(array [i].x);
			array [i].x = Mathf.Round(array [i].x);
			GUIStyle gUIStyle2 = midStyle;
			if (i == count - 2) {
				gUIStyle2 = lastStyle;
			}
			num3 += elemWidth + (float)Mathf.Max(gUIStyle.margin.right, gUIStyle2.margin.left);
			num2++;
			if (num2 >= xCount) {
				num++;
				num2 = 0;
				num4 += elemHeight + (float)Mathf.Max(style.margin.top, style.margin.bottom);
				num3 = position.xMin;
			}
		}
		return array;
	}

	// Ripped from GUI assembly
	static int GetButtonGridMouseSelection(Rect[] buttonRects, Vector2 mousePos, bool findNearest) {
		for (int i = 0; i < buttonRects.Length; i++) {
			if (buttonRects [i].Contains(mousePos)) {
				return i;
			}
		}
		if (!findNearest) {
			return -1;
		}
		float num = 1E+07f;
		int result = -1;
		for (int j = 0; j < buttonRects.Length; j++) {
			Rect rect = buttonRects [j];
			Vector2 b = new Vector2(Mathf.Clamp(mousePos.x, rect.xMin, rect.xMax), Mathf.Clamp(mousePos.y, rect.yMin, rect.yMax));
			float sqrMagnitude = (mousePos - b).sqrMagnitude;
			if (sqrMagnitude < num) {
				result = j;
				num = sqrMagnitude;
			}
		}
		return result;
	}
}
