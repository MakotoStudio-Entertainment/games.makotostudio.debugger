using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MakotoStudio.Debugger.Utils {
	public static class DebuggerUIUtil {
		/// <summary>
		///  Set the Unity Action to the button
		/// </summary>
		/// <param name="button">Button the action should bind</param>
		/// <param name="unityAction">Action to bind</param>
		public static void BindButtonUnityAction(Button button, UnityAction unityAction) {
			var btnEvent = new Button.ButtonClickedEvent();
			btnEvent.AddListener(unityAction);
			button.onClick = btnEvent;
		}
		
		/// <summary>
		///		Set the Unity Action to the Dropdown on value change
		/// </summary>
		/// <param name="dropdown">Dropdown to bind</param>
		/// <param name="dropdownOptions">Dropdown options</param>
		/// <param name="unityAction">Action to bind</param>
		public static void BindDropdownUnityAction(Dropdown dropdown, List<Dropdown.OptionData> dropdownOptions, UnityAction<int> unityAction) {
			dropdown.options = dropdownOptions;
			var dbEvent = new Dropdown.DropdownEvent();
			dbEvent.AddListener(unityAction);
			dropdown.onValueChanged = dbEvent;
		}
	}
}