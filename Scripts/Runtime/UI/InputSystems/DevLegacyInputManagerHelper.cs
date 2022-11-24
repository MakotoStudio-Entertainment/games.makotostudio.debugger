using UnityEngine;

namespace MakotoStudio.Debugger.UI.InputSystems {
	public class DevLegacyInputManagerHelper : MonoBehaviour, IDevInputHelper {
		private void Start() {
		}

		private void Update() {
			if (Input.GetKeyDown(DevUiManager.Singleton.GetLegacyInputManagerKeyBinding)) {
				OpenDevUi();
			}
		}

		public void OpenDevUi() {
			DevUiManager.Singleton.OpenDevBuildPanel();
		}
	}
}