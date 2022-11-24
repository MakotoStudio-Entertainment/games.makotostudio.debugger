using System.IO;
using MakotoStudio.Debugger.ScriptableObjects;
using UnityEditor;
using UnityEngine;
using UEditor = UnityEditor.Editor;

namespace MakotoStudio.Debugger.Editor.Utils {
	public class EditorUtils : UEditor {
		private static string _PACKAGE_FULL_PATH;

		public static string PackageFullPath {
			get {
				if (string.IsNullOrEmpty(_PACKAGE_FULL_PATH))
					_PACKAGE_FULL_PATH = GetPackageFullPath();

				return _PACKAGE_FULL_PATH;
			}
		}

		public static T CreateAsset<T>(string path) where T : ScriptableObject {
			var dataClass = CreateInstance<T>();
			AssetDatabase.CreateAsset(dataClass, path);
			AssetDatabase.Refresh();
			AssetDatabase.SaveAssets();
			return dataClass;
		}

		public static void UpdateDebuggerSettingAsset(MsDebuggerSettings msDebuggerSettings) {
			AssetDatabase.SaveAssetIfDirty(msDebuggerSettings);
			AssetDatabase.Refresh();
			AssetDatabase.SaveAssets();
		}

		public static void CreateDebuggerSettingAsset(string path, MsDebuggerSettings msDebuggerSettings) {
			AssetDatabase.CreateAsset(msDebuggerSettings, path);
			AssetDatabase.Refresh();
			AssetDatabase.SaveAssets();
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