#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace MakotoStudio.Debugger.UI.InputSystems {
	/// <summary>
	/// Interface for input handling
	/// </summary>
	public interface IDevInputHelper {
#if ENABLE_LEGACY_INPUT_MANAGER
		/// <summary>
		/// Opens the Log Config view with the Input Manager
		/// </summary>
		public void OpenDevUi() {
		}
#endif

#if ENABLE_INPUT_SYSTEM
		/// <summary>
		/// Opens the Log Config view with the Input System
		/// </summary>
		/// <param name="callbackContext">Input Action callback</param>
		public void OpenDevUi(InputAction.CallbackContext callbackContext) {
		}
#endif
	}
}