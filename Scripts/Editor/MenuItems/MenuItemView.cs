using MakotoStudio.Debugger.Editor.Utils;
using MakotoStudio.Debugger.Editor.Views;
using UnityEditor;

namespace MakotoStudio.Debugger.Editor.MenuItems {
	public static class MenuItemView {
		[MenuItem("Tools/Makoto Studio Debugger/Import Resources", false, 2050)]
		public static void ImportProjectResourcesMenu() {
			PackageUtils.ImportResources();
		}

		[MenuItem("Tools/Makoto Studio Debugger/Update Settings")]
		private static void CreateDebuggerSettings() {
			SettingsView.ShowSettingsView();
		}
	}
}