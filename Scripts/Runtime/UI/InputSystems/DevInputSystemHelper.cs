using UnityEngine;
using UnityEngine.InputSystem;

namespace MakotoStudio.Debugger.UI.InputSystems {
	public class DevInputSystemHelper : MonoBehaviour, IDevInputHelper {
		private readonly InputAction m_OpenDevUiAction = new ();

		public void OpenDevUi(InputAction.CallbackContext callbackContext) {
			DevUiManager.Singleton.SetDevConfigViewState();
		}

		private void Start() {
			// Bind Developer Open Panel Key
			DevKeyBinding();
		}

		private void DevKeyBinding() {
			m_OpenDevUiAction.AddBinding(DevUiManager.Singleton.GetInputSystemKeyBinding);
			m_OpenDevUiAction.performed -= OpenDevUi;
			m_OpenDevUiAction.performed += OpenDevUi;
			m_OpenDevUiAction.Enable();
		}

		private void OnDestroy() {
			m_OpenDevUiAction.performed -= OpenDevUi;
		}
	}
}