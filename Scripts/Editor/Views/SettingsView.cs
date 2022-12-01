using System.Collections.Generic;
using System.IO;
using MakotoStudio.Debugger.Editor.Constants;
using MakotoStudio.Debugger.Editor.Utils;
using MakotoStudio.Debugger.ScriptableObjects;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace MakotoStudio.Debugger.Editor.Views {
	public class SettingsView : EditorWindow {
		private static SettingsView _INSTANCE;
		private MsDebuggerSettings m_DebuggerSettings;
		private MsMaterialSettings m_MaterialSettings;

		private const string BaseResourceSavePath = "Assets/MakotoStudioDebuggerResources/Resources";
		private const string BaseMaterialSavePath = "Assets/MakotoStudioDebuggerResources/Materials";

		private const string DebuggerSettingsSavePath =
			"Assets/MakotoStudioDebuggerResources/Resources/DebuggerSettings.asset";

		private const string MaterialSettingsSavePath =
			"Assets/MakotoStudioDebuggerResources/Resources/MaterialSettings.asset";

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
			if (Directory.Exists(ResourceSavePath)) {
				Directory.CreateDirectory(BaseResourceSavePath);
				if (File.Exists(DebuggerSettingsSavePath)) {
					m_DebuggerSettings = AssetDatabase.LoadAssetAtPath<MsDebuggerSettings>(DebuggerSettingsSavePath);
				}
				else {
					m_MissingResources.Add(RequiredResources.DebuggerSettings);
				}

				if (File.Exists(MaterialSettingsSavePath)) {
					m_MaterialSettings = AssetDatabase.LoadAssetAtPath<MsMaterialSettings>(MaterialSettingsSavePath);
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
				m_DebuggerSettings = EditorUtils.CreateAsset<MsDebuggerSettings>(DebuggerSettingsSavePath);
				using (var r = new StreamReader(
					       Path.GetFullPath(BaseResourceSavePath + "/DefaultValues/ComponentsToIgnoreValueList.json"))) {
					var json = r.ReadToEnd();
					m_DebuggerSettings.ComponentsToIgnoreList = JsonConvert.DeserializeObject<List<string>>(json);
				}

				using (var r = new StreamReader(
					       Path.GetFullPath(BaseResourceSavePath + "/DefaultValues/ComponentsNotDisableValueList.json"))) {
					var json = r.ReadToEnd();
					m_DebuggerSettings.ComponentsNotDisableList = JsonConvert.DeserializeObject<List<string>>(json);
				}

				using (var r = new StreamReader(
					       Path.GetFullPath(BaseResourceSavePath + "/DefaultValues/PropertyValueList.json"))) {
					var json = r.ReadToEnd();
					m_DebuggerSettings.PropertiesToIgnore = JsonConvert.DeserializeObject<List<string>>(json);
				}

				m_MissingResources.Remove(RequiredResources.DebuggerSettings);
			}
		}

		private void DrawCreateMaterialSettingResources() {
			if (GUILayout.Button("Create Material Setting Resources", GUILayout.Height(40))) {
				m_MaterialSettings = EditorUtils.CreateAsset<MsMaterialSettings>(MaterialSettingsSavePath);
				m_MissingResources.Remove(RequiredResources.MaterialSettings);
			}
		}

		private void DrawDebuggerGeneralSettingsGUI() {
			EditorGUILayout.LabelField("General Settings", WindowGUIStyle.GetSubHeaderStyle());
			m_DebuggerSettings.LogType = (LogType) EditorGUILayout.EnumPopup("Debug Level", m_DebuggerSettings.LogType);
			m_DebuggerSettings.LogPath = EditorGUILayout.TextField("Log Path", m_DebuggerSettings.LogPath);
		}

		private void DrawMaterialSettingsGUI() {
			EditorGUILayout.LabelField("Materials", WindowGUIStyle.GetSubHeaderStyle());
			if (m_MaterialSettings.DefaultMaterial == null) {
				m_MaterialSettings.DefaultMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "defaultMaterial.mat"));
			}

			m_MaterialSettings.DefaultMaterial = (Material) EditorGUILayout.ObjectField("Default Material",
				m_MaterialSettings.DefaultMaterial, typeof(Material));

			if (m_MaterialSettings.UntaggedTagMaterial == null) {
				m_MaterialSettings.UntaggedTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "untaggedTagMaterial.mat"));
			}

			m_MaterialSettings.UntaggedTagMaterial = (Material) EditorGUILayout.ObjectField("Untagged Tag Material",
				m_MaterialSettings.UntaggedTagMaterial, typeof(Material));

			if (m_MaterialSettings.RespawnTagMaterial == null) {
				m_MaterialSettings.RespawnTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "respawnTagMaterial.mat"));
			}

			m_MaterialSettings.RespawnTagMaterial = (Material) EditorGUILayout.ObjectField("Respawn Tag Material",
				m_MaterialSettings.RespawnTagMaterial, typeof(Material));

			if (m_MaterialSettings.FinishTagMaterial == null) {
				m_MaterialSettings.FinishTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "finishTagMaterial.mat"));
			}

			m_MaterialSettings.FinishTagMaterial = (Material) EditorGUILayout.ObjectField("Finish Tag Material",
				m_MaterialSettings.FinishTagMaterial, typeof(Material));

			if (m_MaterialSettings.EditorOnlyTagMaterial == null) {
				m_MaterialSettings.EditorOnlyTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "editorOnlyTagMaterial.mat"));
			}

			m_MaterialSettings.EditorOnlyTagMaterial = (Material) EditorGUILayout.ObjectField("Editor Only Tag Material",
				m_MaterialSettings.EditorOnlyTagMaterial, typeof(Material));

			if (m_MaterialSettings.MainCameraTagMaterial == null) {
				m_MaterialSettings.MainCameraTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "mainCameraTagMaterial.mat"));
			}

			m_MaterialSettings.MainCameraTagMaterial = (Material) EditorGUILayout.ObjectField("Main Camara Tag Material",
				m_MaterialSettings.MainCameraTagMaterial, typeof(Material));

			if (m_MaterialSettings.PlayerTagMaterial == null) {
				m_MaterialSettings.PlayerTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "playerTagMaterial.mat"));
			}

			m_MaterialSettings.PlayerTagMaterial = (Material) EditorGUILayout.ObjectField("Player Tag Material",
				m_MaterialSettings.PlayerTagMaterial, typeof(Material));

			if (m_MaterialSettings.GameControllerTagMaterial == null) {
				m_MaterialSettings.GameControllerTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "gameControllerTagMaterial.mat"));
			}

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