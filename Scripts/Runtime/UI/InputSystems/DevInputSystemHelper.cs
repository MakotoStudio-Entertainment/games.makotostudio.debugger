using UnityEngine;

#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MakotoStudio.Debugger.UI.InputSystems {
	/// <summary>
	/// Input handler for the new Input System
	/// </summary>
	public class DevInputSystemHelper : MonoBehaviour, IDevInputHelper {
#if ENABLE_INPUT_SYSTEM
		private readonly InputAction m_OpenDevUiAction = new();

		/// <summary>
		/// Opens the Log Config view with the Input System
		/// </summary>
		public void OpenDevUi(InputAction.CallbackContext callbackContext) {
			DevUiManager.Singleton.SetDevConfigViewState();
		}

		private void Start() {
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

#endif
	}
}