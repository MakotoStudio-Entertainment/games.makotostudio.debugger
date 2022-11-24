using MakotoStudio.Debugger.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UEditor = UnityEditor.Editor;

namespace MakotoStudio.Debugger.Editor.Utils {
	[CustomEditor(typeof(MsDebuggerSettings))]
	public class SettingsUtil : UEditor {
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			MsDebuggerSettings devMaterialUtil = (MsDebuggerSettings) target;

			var loadTagButtonName = "Load Tag List";
			if (devMaterialUtil.DebugObjectTagColors.Count != 0) {
				loadTagButtonName = "Update tag list";
			}

			if (GUILayout.Button(loadTagButtonName)) {
				devMaterialUtil.BtnLoadUpdateTagList();
			}

			var loadLayerButtonName = "Load Layers List";
			if (devMaterialUtil.DebugObjectLayerColors.Count != 0) {
				loadLayerButtonName = "Update Layers list";
			}

			if (GUILayout.Button(loadLayerButtonName)) {
				devMaterialUtil.BtnLoadUpdateLayerList();
			}
		}
	}
}