using UnityEngine;

namespace MakotoStudio.Debugger.Editor.Utils {
	public static class WindowGUIStyle {
		public static GUIStyle GetHeaderStyle() => new() {
			fontStyle = FontStyle.Bold,
			fontSize = 20,
			normal = new GUIStyleState {
				textColor = Color.white
			}
		};
		
		public static GUIStyle GetSubHeaderStyle() => new() {
			fontStyle = FontStyle.Bold,
			fontSize = 12,
			normal = new GUIStyleState {
				textColor = Color.white
			}
		};
	}
}