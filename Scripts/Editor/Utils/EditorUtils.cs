using System.IO;
using UnityEditor;
using UnityEngine;
using UEditor = UnityEditor.Editor;

namespace MakotoStudio.Debugger.Editor.Utils {
	public class EditorUtils : UEditor {
		private static string _PACKAGE_FULL_PATH;

		/// <summary>
		/// Get Package full path
		/// </summary>
		/// <returns>The fully qualified location of path or null if not found</returns>
		public static string PackageFullPath {
			get {
				if (string.IsNullOrEmpty(_PACKAGE_FULL_PATH))
					_PACKAGE_FULL_PATH = GetPackageFullPath();

				return _PACKAGE_FULL_PATH;
			}
		}

		/// <summary>
		/// Returns the first asset object of type type at given path assetPath.
		/// </summary>
		/// <param name="assetPath">Path of the asset to load.</param>
		/// <typeparam name="T">Data type of the asset.</typeparam>
		/// <returns>Object The asset matching the parameters.</returns>
		public static T GetAssets<T>(string assetPath) where T : Object => AssetDatabase.LoadAssetAtPath<T>(assetPath);

		/// <summary>
		/// Saves and returns the asset object of type at given path assetPath.
		/// </summary>
		///  <param name="assetPath">Path of the asset to save.</param>
		/// <typeparam name="T">Data type of the asset.</typeparam>
		/// <returns>Object The asset matching the parameters.</returns>
		public static T CreateAsset<T>(string assetPath) where T : ScriptableObject {
			var createdAsset = CreateInstance<T>();
			AssetDatabase.CreateAsset(createdAsset, assetPath);
			AssetDatabase.Refresh();
			AssetDatabase.SaveAssets();
			return createdAsset;
		}

		private static string GetPackageFullPath() {
			// Check for potential UPM package
			string packagePath = Path.GetFullPath("Packages/games.makotostudio.debugger");
			if (Directory.Exists(packagePath)) {
				return packagePath;
			}

			return null;
		}
	}
}