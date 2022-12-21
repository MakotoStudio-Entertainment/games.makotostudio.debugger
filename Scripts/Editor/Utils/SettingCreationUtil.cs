using System.Collections.Generic;
using System.IO;
using MakotoStudio.Debugger.ScriptableObjects;
using Newtonsoft.Json;
using UnityEngine;

namespace MakotoStudio.Debugger.Editor.Utils {
	public static class SettingCreationUtil {
		public const string BaseResourceSavePath = "Assets/MakotoStudioDebuggerResources/Resources";
		public const string BaseMaterialSavePath = "Assets/MakotoStudioDebuggerResources/Materials";
		public const string DebuggerSettingsSavePath =
			"Assets/MakotoStudioDebuggerResources/Resources/DebuggerSettings.asset";
		public const string MaterialSettingsSavePath =
			"Assets/MakotoStudioDebuggerResources/Resources/MaterialSettings.asset";
		public const string ResourceSavePath = "Assets/MakotoStudioDebuggerResources";
		
		/// <summary>
		/// Seed default values from config json in to Material settings
		/// </summary>
		/// <param name="materialSettings"></param>
		public static void SeedDefaultValues(MsMaterialSettings materialSettings) {
			if (materialSettings.DefaultMaterial == null) {
				materialSettings.DefaultMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "defaultMaterial.mat"));
			}

			if (materialSettings.UntaggedTagMaterial == null) {
				materialSettings.UntaggedTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "untaggedTagMaterial.mat"));
			}

			if (materialSettings.RespawnTagMaterial == null) {
				materialSettings.RespawnTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "respawnTagMaterial.mat"));
			}

			if (materialSettings.FinishTagMaterial == null) {
				materialSettings.FinishTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "finishTagMaterial.mat"));
			}

			if (materialSettings.EditorOnlyTagMaterial == null) {
				materialSettings.EditorOnlyTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "editorOnlyTagMaterial.mat"));
			}

			if (materialSettings.MainCameraTagMaterial == null) {
				materialSettings.MainCameraTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "mainCameraTagMaterial.mat"));
			}

			if (materialSettings.PlayerTagMaterial == null) {
				materialSettings.PlayerTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "playerTagMaterial.mat"));
			}

			if (materialSettings.GameControllerTagMaterial == null) {
				materialSettings.GameControllerTagMaterial =
					EditorUtils.GetAssets<Material>(Path.Combine(BaseMaterialSavePath, "gameControllerTagMaterial.mat"));
			}
		}

		/// <summary>
		/// Seed default values from config json in to debugger settings
		/// </summary>
		/// <param name="debuggerSettings"></param>
		public static void SeedDefaultValues(MsDebuggerSettings debuggerSettings) {
			var baseResourceConfigPath = EditorUtils.PackageFullPath + "/PackageResources/DefaultConfig";

			var ignoreLayer = "DevIgnore";
			if (!LayerUtil.LayerExists(ignoreLayer)) {
				var created = LayerUtil.CreateLayer(ignoreLayer);
				Debug.Log(created ? $"Layer successfully created: {ignoreLayer}" : $"Failed to creat Layer: {ignoreLayer}");
			}

			using (var r = new StreamReader(
				       Path.GetFullPath(baseResourceConfigPath + "/ComponentsToIgnoreValueList.json"))) {
				var json = r.ReadToEnd();
				debuggerSettings.ComponentsToIgnoreList = JsonConvert.DeserializeObject<List<string>>(json);
			}

			using (var r = new StreamReader(
				       Path.GetFullPath(baseResourceConfigPath + "/ComponentsNotDisableValueList.json"))) {
				var json = r.ReadToEnd();
				debuggerSettings.ComponentsNotDisableList = JsonConvert.DeserializeObject<List<string>>(json);
			}

			using (var r = new StreamReader(
				       Path.GetFullPath(baseResourceConfigPath + "/PropertyValueList.json"))) {
				var json = r.ReadToEnd();
				debuggerSettings.PropertiesToIgnore = JsonConvert.DeserializeObject<List<string>>(json);
			}
		}
	}
}