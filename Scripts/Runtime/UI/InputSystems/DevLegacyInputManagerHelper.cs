using UnityEngine;

namespace MakotoStudio.Debugger.UI.InputSystems {
	/// <summary>
	/// Input handler for the old Input Manager
	/// </summary>
	public class DevLegacyInputManagerHelper : MonoBehaviour, IDevInputHelper {
#if ENABLE_LEGACY_INPUT_MANAGER
		/// <summary>
		/// Opens the Log Config view
		/// </summary>
		public void OpenDevUi() {
			DevUiManager.Singleton.SetDevConfigViewState();
		}

		private void Update() {
			if (Input.GetKeyDown(DevUiManager.Singleton.GetLegacyInputManagerKeyBinding)) {
				OpenDevUi();
			}
		}
#endif
	}
}