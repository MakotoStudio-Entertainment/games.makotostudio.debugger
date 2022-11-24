using System;
using System.IO;
using MakotoStudio.Debugger.Editor.Utils;
using MakotoStudio.Debugger.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace MakotoStudio.Debugger.Editor.Views {
	public class SettingsView : EditorWindow {
		public static SettingsView Instance;
		private MsDebuggerSettings m_MsDebuggerSettings;
		private const string SavePath = "Assets/MakotoStudioDebuggerResources/Resources/DebuggerSettings.asset";

		private LogType m_LogType;
		private bool m_FileExist;

		public static void ShowSettingsView() {
			Instance = (SettingsView) GetWindow(typeof(SettingsView));
			Instance.titleContent = new GUIContent("Settings");
		}

		private void OnEnable() {
			if (File.Exists(SavePath)) {
				m_FileExist = true;
				m_MsDebuggerSettings = AssetDatabase.LoadAssetAtPath<MsDebuggerSettings>(SavePath);
				m_LogType = m_MsDebuggerSettings.LogType;
			}
		}

		private void OnGUI() {
			if (m_FileExist) {
				DrawFieldsGUI();
				DrawButtonGUI();
			}
			else {
				DrawImportResources();
			}
		}

		private void DrawImportResources() {
			if (GUILayout.Button("Load Resources", GUILayout.Height(40))) {
				PackageUtils.ImportResources();
				Close();
			}
		}

		[Obsolete("Obsolete")]
		private void DrawFieldsGUI() {
			EditorGUILayout.Space(10);
			EditorGUILayout.LabelField("General Settings");
			m_MsDebuggerSettings.LogType = (LogType) EditorGUILayout.EnumPopup("Debug Level", m_MsDebuggerSettings.LogType);
			// Default Materials
			EditorGUILayout.Space(10);
			EditorGUILayout.LabelField("Default Material");
			m_MsDebuggerSettings.defaultMaterial = (Material) EditorGUILayout.ObjectField("Default Material",m_MsDebuggerSettings.defaultMaterial,typeof(Material));
			m_MsDebuggerSettings.defaultLayerMaterial = (Material) EditorGUILayout.ObjectField("Default Layer Material",m_MsDebuggerSettings.defaultLayerMaterial,typeof(Material));
			// Tag Materials
			EditorGUILayout.Space(10);
			EditorGUILayout.LabelField("Tag Material");
			m_MsDebuggerSettings.untaggedTagMaterial = (Material) EditorGUILayout.ObjectField("Untagged Tag Material",m_MsDebuggerSettings.untaggedTagMaterial,typeof(Material));
			m_MsDebuggerSettings.respawnTagMaterial = (Material) EditorGUILayout.ObjectField("Respawn Tag Material",m_MsDebuggerSettings.respawnTagMaterial,typeof(Material));
			m_MsDebuggerSettings.finishTagMaterial = (Material) EditorGUILayout.ObjectField("Finish Tag Material",m_MsDebuggerSettings.finishTagMaterial,typeof(Material));
			m_MsDebuggerSettings.editorOnlyTagMaterial = (Material) EditorGUILayout.ObjectField("Editor Only Tag Material",m_MsDebuggerSettings.editorOnlyTagMaterial,typeof(Material));
			m_MsDebuggerSettings.mainCameraTagMaterial = (Material) EditorGUILayout.ObjectField("Main Camara Tag Material",m_MsDebuggerSettings.mainCameraTagMaterial,typeof(Material));
			m_MsDebuggerSettings.playerTagMaterial = (Material) EditorGUILayout.ObjectField("Player Tag Material",m_MsDebuggerSettings.playerTagMaterial,typeof(Material));
			m_MsDebuggerSettings.gameControllerTagMaterial = (Material) EditorGUILayout.ObjectField("Game Controller Tag Material",m_MsDebuggerSettings.gameControllerTagMaterial,typeof(Material));

			// Layer Materials
			EditorGUILayout.Space(10);
			EditorGUILayout.LabelField("Layer Material");
			m_MsDebuggerSettings.transparentFxLayerMaterial = (Material) EditorGUILayout.ObjectField("Transparent Layer Material",m_MsDebuggerSettings.transparentFxLayerMaterial,typeof(Material));
			m_MsDebuggerSettings.ignoreRaycastLayerMaterial = (Material) EditorGUILayout.ObjectField("Ignore Raycast Layer Material",m_MsDebuggerSettings.ignoreRaycastLayerMaterial,typeof(Material));
			m_MsDebuggerSettings.waterLayerMaterial = (Material) EditorGUILayout.ObjectField("Water Layer Material",m_MsDebuggerSettings.waterLayerMaterial,typeof(Material));
			m_MsDebuggerSettings.uiLayerMaterial = (Material) EditorGUILayout.ObjectField("UI Raycast Layer Material",m_MsDebuggerSettings.uiLayerMaterial,typeof(Material));
		}

		private void DrawButtonGUI() {
			if (GUILayout.Button("Save", GUILayout.Height(40))) {
				UpdateAsset();
			}
		}

		private void UpdateAsset() {
			EditorUtils.UpdateDebuggerSettingAsset(m_MsDebuggerSettings);
		}

		private void CreateAsset() {
			m_MsDebuggerSettings = CreateInstance<MsDebuggerSettings>();
			EditorUtils.CreateDebuggerSettingAsset(SavePath, m_MsDebuggerSettings);
		}
	}
}