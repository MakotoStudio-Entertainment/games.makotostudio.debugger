using System.Collections.Generic;
using System.IO;
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
			if (m_MaterialSettings.defaultMaterial == null) {
				m_MaterialSettings.defaultMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "defaultMaterial.mat"));
			}

			m_MaterialSettings.defaultMaterial = (Material) EditorGUILayout.ObjectField("Default Material",
				m_MaterialSettings.defaultMaterial, typeof(Material));

			if (m_MaterialSettings.untaggedTagMaterial == null) {
				m_MaterialSettings.untaggedTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "untaggedTagMaterial.mat"));
			}

			m_MaterialSettings.untaggedTagMaterial = (Material) EditorGUILayout.ObjectField("Untagged Tag Material",
				m_MaterialSettings.untaggedTagMaterial, typeof(Material));

			if (m_MaterialSettings.respawnTagMaterial == null) {
				m_MaterialSettings.respawnTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "respawnTagMaterial.mat"));
			}

			m_MaterialSettings.respawnTagMaterial = (Material) EditorGUILayout.ObjectField("Respawn Tag Material",
				m_MaterialSettings.respawnTagMaterial, typeof(Material));

			if (m_MaterialSettings.finishTagMaterial == null) {
				m_MaterialSettings.finishTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "finishTagMaterial.mat"));
			}

			m_MaterialSettings.finishTagMaterial = (Material) EditorGUILayout.ObjectField("Finish Tag Material",
				m_MaterialSettings.finishTagMaterial, typeof(Material));

			if (m_MaterialSettings.editorOnlyTagMaterial == null) {
				m_MaterialSettings.editorOnlyTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "editorOnlyTagMaterial.mat"));
			}

			m_MaterialSettings.editorOnlyTagMaterial = (Material) EditorGUILayout.ObjectField("Editor Only Tag Material",
				m_MaterialSettings.editorOnlyTagMaterial, typeof(Material));

			if (m_MaterialSettings.mainCameraTagMaterial == null) {
				m_MaterialSettings.mainCameraTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "mainCameraTagMaterial.mat"));
			}

			m_MaterialSettings.mainCameraTagMaterial = (Material) EditorGUILayout.ObjectField("Main Camara Tag Material",
				m_MaterialSettings.mainCameraTagMaterial, typeof(Material));

			if (m_MaterialSettings.playerTagMaterial == null) {
				m_MaterialSettings.playerTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "playerTagMaterial.mat"));
			}

			m_MaterialSettings.playerTagMaterial = (Material) EditorGUILayout.ObjectField("Player Tag Material",
				m_MaterialSettings.playerTagMaterial, typeof(Material));

			if (m_MaterialSettings.gameControllerTagMaterial == null) {
				m_MaterialSettings.gameControllerTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "gameControllerTagMaterial.mat"));
			}

			m_MaterialSettings.gameControllerTagMaterial = (Material) EditorGUILayout.ObjectField(
				"Game Controller Tag Material", m_MaterialSettings.gameControllerTagMaterial, typeof(Material));
		}

		private void DrawCloseGUI() {
			if (GUILayout.Button("Close", GUILayout.Height(40))) {
				Close();
			}
		}
	}

	public enum RequiredResources {
		Resources,
		MaterialSettings,
		DebuggerSettings
	}
}