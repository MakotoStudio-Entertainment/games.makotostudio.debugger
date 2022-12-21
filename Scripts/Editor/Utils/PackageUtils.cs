using UnityEditor;
using UEditor = UnityEditor.Editor;

namespace MakotoStudio.Debugger.Editor.Utils {
	public class PackageUtils : UEditor {
		/// <summary>
		/// Start Import the package resources
		/// </summary>
		public static void ImportResources() {
			var packageFullPath =
				EditorUtils.PackageFullPath + "/PackageResources/MakotoStudioDebuggerResources.unitypackage";
			AssetDatabase.ImportPackage(packageFullPath, true);
		}
	}
}