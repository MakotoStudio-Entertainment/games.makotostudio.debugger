using System;
using MakotoStudio.Debugger.Constant;
using MakotoStudio.Debugger.UI.InputSystems;
using MakotoStudio.Debugger.Utils;
using UnityEngine;

namespace MakotoStudio.Debugger.UI {
	[RequireComponent(typeof(DevMaterialUtil))]
	[RequireComponent(typeof(DevBuildEventHandler))]
	public class DevUiManager : MonoBehaviour {
		public static DevUiManager Singleton;

		[Header("Input Keybinding")]
#if ENABLE_LEGACY_INPUT_MANAGER && !ENABLE_INPUT_SYSTEM
    [HideInInspector]
#endif
		[SerializeField]
		private string inputSystemKeyBinding = "<Keyboard>/f1";

#if ENABLE_INPUT_SYSTEM || (ENABLE_INPUT_SYSTEM && ENABLE_LEGACY_INPUT_MANAGER)
		[HideInInspector]
#endif
		[SerializeField]
		private KeyCode legacyInputManagerKeyBinding = KeyCode.F1;


		[SerializeField] private GameObject devLogConfigView;


		/// <summary>
		///   Get the KeyBinding for the Input System Developer / Debug panel open event
		/// </summary>
		public string GetInputSystemKeyBinding => inputSystemKeyBinding;

		/// <summary>
		///   Get the KeyBinding for the Legacy Input Manager Developer / Debug panel open event
		/// </summary>
		public KeyCode GetLegacyInputManagerKeyBinding => legacyInputManagerKeyBinding;

		private IDevInputHelper m_DevInputHelper;
		private ActiveInputSystemType m_ActiveInputSystemType;

		public void OpenDevBuildPanel() {
			devLogConfigView.SetActive(!devLogConfigView.activeSelf);
		}
		
		private void Awake() {
			if (!Debug.isDebugBuild) {
				Debug.LogError("NO DEBUG BUILD");
				Destroy(gameObject);
				return;
			}

			if (Singleton == null) {
				Singleton = this;
				Debug.Log("DEBUG BUILD");
				DontDestroyOnLoad(Singleton);
			}
			else {
				Debug.LogError("NO DEBUG BUILD");
				Destroy(this);
			}
		}

		private void Start() {
			m_ActiveInputSystemType = GetActiveInputSystem();
			SetInputSystem();
		}

		private void SetInputSystem() {
			switch (m_ActiveInputSystemType) {
				case ActiveInputSystemType.InputSystem:
					m_DevInputHelper = gameObject.AddComponent<DevInputSystemHelper>();
					gameObject.GetComponent<DevInputSystemHelper>().enabled = true;
					break;
				case ActiveInputSystemType.LegacyInputManager:
					m_DevInputHelper = gameObject.AddComponent<DevLegacyInputManagerHelper>();
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