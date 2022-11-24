using UnityEditor;
using UEditor = UnityEditor.Editor;

namespace MakotoStudio.Debugger.Editor.Utils {
	public class PackageUtils : UEditor {
		public static void ImportResources() {
			string packageFullPath = EditorUtils.PackageFullPath;
			AssetDatabase.ImportPackage(packageFullPath + "/PackageResources/MakotoStudioDebuggerResources.unitypackage",
				true);
		}
	}
}