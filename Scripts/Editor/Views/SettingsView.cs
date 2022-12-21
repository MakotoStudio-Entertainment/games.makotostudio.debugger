using System.Collections.Generic;
using System.IO;
using MakotoStudio.Debugger.Editor.Constants;
using MakotoStudio.Debugger.Editor.Utils;
using MakotoStudio.Debugger.ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace MakotoStudio.Debugger.Editor.Views {
	public class SettingsView : EditorWindow {
		private static SettingsView _INSTANCE;
		private MsDebuggerSettings m_DebuggerSettings;
		private MsMaterialSettings m_MaterialSettings;

		private const string ResourceSavePath = "Assets/MakotoStudioDebuggerResources";

		private readonly List<RequiredResources> m_MissingResources = new();


		/// <summary>
		/// Instantiate a new MS Debugger Setting View
		/// </summary>
		public static void ShowSettingsView() {
			_INSTANCE = (SettingsView) GetWindow(typeof(SettingsView));
			_INSTANCE.titleContent = new GUIContent("Settings");
		}

		private void OnEnable() {
			if (Directory.Exists(SettingCreationUtil.ResourceSavePath)) {
				Directory.CreateDirectory(SettingCreationUtil.BaseResourceSavePath);
				if (File.Exists(SettingCreationUtil.DebuggerSettingsSavePath)) {
					m_DebuggerSettings =
						AssetDatabase.LoadAssetAtPath<MsDebuggerSettings>(SettingCreationUtil.DebuggerSettingsSavePath);
				}
				else {
					m_MissingResources.Add(RequiredResources.DebuggerSettings);
				}

				if (File.Exists(SettingCreationUtil.MaterialSettingsSavePath)) {
					m_MaterialSettings =
						AssetDatabase.LoadAssetAtPath<MsMaterialSettings>(SettingCreationUtil.MaterialSettingsSavePath);
				}
				else {
					m_MissingResources.Add(RequiredResources.MaterialSettings);
				}
			}
			else {
				m_MissingResources.Add(RequiredResources.Resources);
			}
		}

		private void OnGUI() {
			if (m_MissingResources.Contains(RequiredResources.Resources)) {
				DrawImportResources();
			}
			else {
				EditorGUILayout.Space(10);
				EditorGUILayout.LabelField("Debugger Settings", WindowGUIStyle.GetHeaderStyle());
				EditorGUILayout.Space(10);
				if (m_MissingResources.Contains(RequiredResources.DebuggerSettings)) {
					DrawCreateDebuggerSettingResources();
				}
				else {
					DrawDebuggerGeneralSettingsGUI();
				}

				EditorGUILayout.Space(10);
				EditorGUILayout.LabelField("Material Settings", WindowGUIStyle.GetHeaderStyle());
				EditorGUILayout.Space(10);
				if (m_MissingResources.Contains(RequiredResources.MaterialSettings)) {
					DrawCreateMaterialSettingResources();
				}
				else {
					DrawMaterialSettingsGUI();
				}
			}

			DrawCloseGUI();
		}

		private void DrawImportResources() {
			if (GUILayout.Button("Load Resources", GUILayout.Height(40))) {
				PackageUtils.ImportResources();
				Close();
			}
		}

		private void DrawCreateDebuggerSettingResources() {
			if (GUILayout.Button("Create Debugger Setting Resources", GUILayout.Height(40))) {
				m_DebuggerSettings =
					EditorUtils.CreateAsset<MsDebuggerSettings>(SettingCreationUtil.DebuggerSettingsSavePath);
				SettingCreationUtil.SeedDefaultValues(m_DebuggerSettings);
				m_MissingResources.Remove(RequiredResources.DebuggerSettings);
			}
		}

		private void DrawCreateMaterialSettingResources() {
			if (GUILayout.Button("Create Material Setting Resources", GUILayout.Height(40))) {
				m_MaterialSettings =
					EditorUtils.CreateAsset<MsMaterialSettings>(SettingCreationUtil.MaterialSettingsSavePath);
				SettingCreationUtil.SeedDefaultValues(m_MaterialSettings);
				m_MissingResources.Remove(RequiredResources.MaterialSettings);
			}
		}

		private void DrawDebuggerGeneralSettingsGUI() {
			EditorGUILayout.LabelField("General Settings", WindowGUIStyle.GetSubHeaderStyle());
			m_DebuggerSettings.LogType = (LogType) EditorGUILayout.EnumPopup("Debug Level", m_DebuggerSettings.LogType);
			m_DebuggerSettings.LogPath = EditorGUILayout.TextField("Log Path", m_DebuggerSettings.LogPath);
			m_DebuggerSettings.LayerMaskField = EditorGUILayout.MaskField(
				"Layers to Ignore",
				m_DebuggerSettings.LayerMaskField,
				LayerUtil.GetAllLayers());
		}

		private void DrawMaterialSettingsGUI() {
			EditorGUILayout.LabelField("Materials", WindowGUIStyle.GetSubHeaderStyle());
			SettingCreationUtil.SeedDefaultValues(m_MaterialSettings);

			m_MaterialSettings.DefaultMaterial = (Material) EditorGUILayout.ObjectField("Default Material",
				m_MaterialSettings.DefaultMaterial, typeof(Material));

			m_MaterialSettings.UntaggedTagMaterial = (Material) EditorGUILayout.ObjectField("Untagged Tag Material",
				m_MaterialSettings.UntaggedTagMaterial, typeof(Material));

			m_MaterialSettings.RespawnTagMaterial = (Material) EditorGUILayout.ObjectField("Respawn Tag Material",
				m_MaterialSettings.RespawnTagMaterial, typeof(Material));

			m_MaterialSettings.FinishTagMaterial = (Material) EditorGUILayout.ObjectField("Finish Tag Material",
				m_MaterialSettings.FinishTagMaterial, typeof(Material));

			m_MaterialSettings.EditorOnlyTagMaterial = (Material) EditorGUILayout.ObjectField("Editor Only Tag Material",
				m_MaterialSettings.EditorOnlyTagMaterial, typeof(Material));

			m_MaterialSettings.MainCameraTagMaterial = (Material) EditorGUILayout.ObjectField("Main Camara Tag Material",
				m_MaterialSettings.MainCameraTagMaterial, typeof(Material));

			m_MaterialSettings.PlayerTagMaterial = (Material) EditorGUILayout.ObjectField("Player Tag Material",
				m_MaterialSettings.PlayerTagMaterial, typeof(Material));

			m_MaterialSettings.GameControllerTagMaterial = (Material) EditorGUILayout.ObjectField(
				"Game Controller Tag Material", m_MaterialSettings.GameControllerTagMaterial, typeof(Material));
		}

		private void DrawCloseGUI() {
			if (GUILayout.Button("Close", GUILayout.Height(40))) {
				Close();
			}
		}
	}
}