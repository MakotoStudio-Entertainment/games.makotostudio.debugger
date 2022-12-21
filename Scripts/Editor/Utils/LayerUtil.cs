using System.Collections.Generic;
using UnityEditor;

namespace MakotoStudio.Debugger.Editor.Utils {
	public static class LayerUtil {
		private const int MaxLayers = 31;

		/// <summary>
		/// Adds the layer.
		/// </summary>
		/// <returns><c>true</c>, if layer was added, <c>false</c> otherwise.</returns>
		/// <param name="layerName">Layer name.</param>
		public static bool CreateLayer(string layerName) {
			var tagManager =
				new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
			var layersProp = tagManager.FindProperty("layers");
			if (PropertyExists(layersProp, 0, MaxLayers, layerName)) return false;
			for (int i = 1, j = MaxLayers; i < j; i++) {
				var sp = layersProp.GetArrayElementAtIndex(i);
				if (!string.IsNullOrWhiteSpace(sp.stringValue))
					continue;
				sp.stringValue = layerName;
				tagManager.ApplyModifiedProperties();
				return true;
			}

			return false;
		}

		/// <summary>
		/// Removes the layer.
		/// </summary>
		/// <returns><c>true</c>, if layer was removed, <c>false</c> otherwise.</returns>
		/// <param name="layerName">Layer name.</param>
		public static bool RemoveLayer(string layerName) {
			var tagManager =
				new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
			var layersProp = tagManager.FindProperty("layers");

			if (!PropertyExists(layersProp, 0, layersProp.arraySize, layerName))
				return false;

			for (int i = 0, j = layersProp.arraySize; i < j; i++) {
				var sp = layersProp.GetArrayElementAtIndex(i);

				if (sp.stringValue != layerName)
					continue;
				sp.stringValue = "";
				tagManager.ApplyModifiedProperties();
				return true;
			}

			return false;
		}

		/// <summary>
		/// Checks to see if layer exists.
		/// </summary>
		/// <returns><c>true</c>, if layer exists, <c>false</c> otherwise.</returns>
		/// <param name="layerName">Layer name.</param>
		public static bool LayerExists(string layerName) {
			var tagManager =
				new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
			var layersProp = tagManager.FindProperty("layers");
			return PropertyExists(layersProp, 0, MaxLayers, layerName);
		}

		/// <summary>
		/// Checks if the value exists in the property.
		/// </summary>
		/// <returns><c>true</c>, if exists was propertyed, <c>false</c> otherwise.</returns>
		/// <param name="property">Property.</param>
		/// <param name="start">Start.</param>
		/// <param name="end">End.</param>
		/// <param name="value">Value.</param>
		private static bool PropertyExists(SerializedProperty property, int start, int end, string value) {
			for (var i = start; i < end; i++) {
				var t = property.GetArrayElementAtIndex(i);
				if (t.stringValue.Equals(value)) {
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Get all layers configured in the TagManager
		/// </summary>
		/// <returns>string array of layers</returns>
		public static string[] GetAllLayers() {
			var tagManager =
				new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
			var layersProp = tagManager.FindProperty("layers");
			var stringArray = new List<string>();
			for (int i = 0, j = MaxLayers; i < j; i++) {
				var sp = layersProp.GetArrayElementAtIndex(i);
				if (string.IsNullOrWhiteSpace(sp.stringValue))
					continue;

				stringArray.Add(sp.stringValue);
			}

			return stringArray.ToArray();
		}
	}
}