using System.Collections.Generic;
using System.IO;
using System.Linq;
using MakotoStudio.Debugger.Constants;
using MakotoStudio.Debugger.Models;
using MakotoStudio.Debugger.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UEditor = UnityEditor.Editor;

namespace MakotoStudio.Debugger.Editor.Utils {
	[CustomEditor(typeof(MsDebuggerSettings))]
	public class SettingsUtil : UEditor {
		private MsDebuggerSettings m_MSDebuggerSettings;
		private MsMaterialSettings m_MaterialSettings;

		private const string MaterialSettingsSavePath =
			"Assets/MakotoStudioDebuggerResources/Resources/MaterialSettings.asset";

		/// <summary>
		/// Implement this function to make a custom inspector.
		/// </summary>
		public override void OnInspectorGUI() {
			DrawDefaultInspector();

			m_MSDebuggerSettings = (MsDebuggerSettings) target;

			var loadTagButtonName = "Load Tag List";
			if (m_MSDebuggerSettings.DebugObjectTagColors is not {Count: 0}) {
				loadTagButtonName = "Update tag list";
			}

			if (GUILayout.Button(loadTagButtonName)) {
				BtnLoadUpdateTagList();
			}
		}

		private void BtnLoadUpdateTagList() {
			var tags = UnityEditorInternal.InternalEditorUtility.tags;
			if (m_MSDebuggerSettings.DebugObjectTagColors == null) {
				m_MSDebuggerSettings.DebugObjectTagColors = new List<DebugObjectColor>();
			}

			FindAndAddToList(m_MSDebuggerSettings.DebugObjectTagColors, tags);
			RemoveNotExist(m_MSDebuggerSettings.DebugObjectTagColors, tags);
			UpdateMaterial(m_MSDebuggerSettings.DebugObjectTagColors);
		}

		private void FindAndAddToList(List<DebugObjectColor> debugObjectColors, string[] stringList) {
			foreach (var s in stringList) {
				if (debugObjectColors.Find(m => m.Name == s) == null) {
					debugObjectColors.Add(new DebugObjectColor {
						Name = s,
						ColorMaterial = new Material(m_MaterialSettings.DefaultMaterial)
					});
				}
			}
		}

		private void RemoveNotExist(List<DebugObjectColor> debugObjectColors, string[] stringList) {
			var toRemoveFromList = new List<DebugObjectColor>();
			debugObjectColors.ForEach(t => {
				var match = stringList.ToList().Find(m => m == t.Name);
				if (match == null) {
					toRemoveFromList.Add(t);
				}
			});
			toRemoveFromList.ForEach(t => debugObjectColors.Remove(t));
		}

		private void UpdateMaterial(List<DebugObjectColor> debugObjectColors) {
			debugObjectColors.ForEach(n => {
				var debugObjectName = n.Name.Replace(" ", "");
				if (debugObjectName == DefaultUnityTagType.Untagged.ToString()) {
					n.ColorMaterial = m_MaterialSettings.UntaggedTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagType.Respawn.ToString()) {
					n.ColorMaterial = m_MaterialSettings.RespawnTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagType.Finished.ToString()) {
					n.ColorMaterial = m_MaterialSettings.FinishTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagType.Player.ToString()) {
					n.ColorMaterial = m_MaterialSettings.PlayerTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagType.EditorOnly.ToString()) {
					n.ColorMaterial = m_MaterialSettings.EditorOnlyTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagType.GameController.ToString()) {
					n.ColorMaterial = m_MaterialSettings.GameControllerTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagType.MainCamera.ToString()) {
					n.ColorMaterial = m_MaterialSettings.MainCameraTagMaterial;
				}
			});
		}

		private void OnEnable() {
			if (File.Exists(MaterialSettingsSavePath)) {
				m_MaterialSettings = AssetDatabase.LoadAssetAtPath<MsMaterialSettings>(MaterialSettingsSavePath);
			}
		}
	}
}