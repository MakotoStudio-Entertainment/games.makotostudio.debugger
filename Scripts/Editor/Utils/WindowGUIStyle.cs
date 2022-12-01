using UnityEngine;

namespace MakotoStudio.Debugger.Editor.Utils {
	public static class WindowGUIStyle {
		/// <summary> 
		/// <list type="bullet">
		///		<listheader>
		///			<term>Get Header styled GUIStyle</term>
		///			<description></description>
		///		</listheader>
		///		<item>
		///			<term>Font Style - </term>
		///			<description>Bold</description>
		///		</item>
		///		<item>
		///			<term>Font Size - </term>
		///			<description>20</description>
		///		</item>
		/// 	<item>
		///			<term>Text Color - </term>
		///			<description>White</description>
		///		</item>
		/// </list>
		/// </summary>
		/// <returns>Header styled GUIStyle</returns>
		public static GUIStyle GetHeaderStyle() => new() {
			fontStyle = FontStyle.Bold,
			fontSize = 20,
			normal = new GUIStyleState {
				textColor = Color.white
			}
		};

		/// <summary>
		/// <list type="bullet">
		///		<listheader>
		///			<term>Get Sub-Header styled GUIStyle </term>
		///			<description></description>
		///		</listheader>
		///		<item>
		///			<term>Font Style - </term>
		///			<description>Bold</description>
		///		</item>
		///		<item>
		///			<term>Font Size - </term>
		///			<description>12</description>
		///		</item>
		/// 	<item>
		///			<term>Text Color - </term>
		///			<description>White</description>
		///		</item>
		/// </list>
		/// </summary>
		/// <returns>Sub-Header styled GUIStyle</returns>
		public static GUIStyle GetSubHeaderStyle() => new() {
			fontStyle = FontStyle.Bold,
			fontSize = 12,
			normal = new GUIStyleState {
				textColor = Color.white
			}
		};
	}
}