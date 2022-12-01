using UnityEngine;
using UnityEngine.InputSystem;

namespace MakotoStudio.Debugger.UI.InputSystems {
	public class DevLegacyInputManagerHelper : MonoBehaviour, IDevInputHelper {
		public void OpenDevUi() {
			DevUiManager.Singleton.SetDevConfigViewState();
		}

		private void Update() {
			if (Input.GetKeyDown(DevUiManager.Singleton.GetLegacyInputManagerKeyBinding)) {
				OpenDevUi();
			}
		}
	}
}