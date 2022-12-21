using System;
using MakotoStudio.Debugger.Constants;
using MakotoStudio.Debugger.UI.InputSystems;
using MakotoStudio.Debugger.Utils;
using UnityEngine;

namespace MakotoStudio.Debugger.UI {
	/// <summary>
	/// Manages the configured input systems
	/// </summary>
	[RequireComponent(typeof(DevDebuggerSettingManager))]
	public class DevUiManager : MonoBehaviour {
		private static DevUiManager _SINGLETON;

		#region InputSystem

		[Header("Input Keybinding")]
#if ENABLE_LEGACY_INPUT_MANAGER && !ENABLE_INPUT_SYSTEM
    [HideInInspector]
#endif
		[SerializeField] private string inputSystemKeyBinding = "<Keyboard>/f1";

#if ENABLE_INPUT_SYSTEM || (ENABLE_INPUT_SYSTEM && ENABLE_LEGACY_INPUT_MANAGER)
		[HideInInspector]
#endif
		[SerializeField] private KeyCode legacyInputManagerKeyBinding = KeyCode.F1;

		#endregion

		[SerializeField] private GameObject devLogConfigView;

		/// <summary>
		/// Get instance of DevUiManager
		/// </summary>
		public static DevUiManager Singleton => _SINGLETON;

		/// <summary>
		///   Get the KeyBinding for the Input System Developer / Debug panel open event
		/// </summary>
		public string GetInputSystemKeyBinding => inputSystemKeyBinding;

		/// <summary>
		///   Get the KeyBinding for the Legacy Input Manager Developer / Debug panel open event
		/// </summary>
		public KeyCode GetLegacyInputManagerKeyBinding => legacyInputManagerKeyBinding;

		private ActiveInputSystemType m_ActiveInputSystemType;

		/// <summary>
		///  Set the Debugger Config View active based on activeSelf State
		/// </summary>
		public void SetDevConfigViewState() {
			devLogConfigView.SetActive(!devLogConfigView.activeSelf);
		}

		private void Awake() {
			if (Application.isEditor) {
				SetSingleton();
				return;
			}

			if (!Debug.isDebugBuild) {
				Debug.LogError("NO DEBUG BUILD");
				Destroy(gameObject);
				return;
			}

			SetSingleton();
		}

		private void SetSingleton() {
			if (_SINGLETON == null) {
				_SINGLETON = this;
				Debug.Log("DEBUG BUILD");
				DontDestroyOnLoad(_SINGLETON);
			}
			else {
				Debug.LogError("Singleton already Exist");
				Destroy(gameObject);
			}
		}

		private void Start() {
			m_ActiveInputSystemType = GetActiveInputSystem();
			SetInputSystem();
		}

		private void SetInputSystem() {
			switch (m_ActiveInputSystemType) {
				case ActiveInputSystemType.InputSystem:
					gameObject.AddComponent<DevInputSystemHelper>();
					gameObject.GetComponent<DevInputSystemHelper>().enabled = true;
					break;
				case ActiveInputSystemType.LegacyInputManager:
					gameObject.AddComponent<DevLegacyInputManagerHelper>();
					gameObject.GetComponent<DevLegacyInputManagerHelper>().enabled = true;
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private ActiveInputSystemType GetActiveInputSystem() {
			// Note: If both active we use the new input system;
#if ENABLE_INPUT_SYSTEM && ENABLE_LEGACY_INPUT_MANAGER
			return ActiveInputSystemType.InputSystem;
#elif ENABLE_INPUT_SYSTEM
      return ActiveInputSystemType.InputSystem;
#elif ENABLE_LEGACY_INPUT_MANAGER
      return ActiveInputSystemType.LegacyInputManager;
#endif
		}
	}
}