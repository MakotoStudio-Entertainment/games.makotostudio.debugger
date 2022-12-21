using MakotoStudio.Debugger.ScriptableObjects;
using UnityEngine;

namespace MakotoStudio.Debugger.Utils {
	/// <summary>
	/// MonoBehaviour script for Debugger Setting holder 
	/// </summary>
	public class DevDebuggerSettingManager : MonoBehaviour {
		private static DevDebuggerSettingManager _SINGLETON;
		
		[SerializeField] private MsDebuggerSettings msDebuggerSettings;
		
		/// <summary>
		/// Get MsDebuggerSettings
		/// </summary>
		public MsDebuggerSettings MsDebuggerSettings => msDebuggerSettings;
		
		/// <summary>
		/// Get instance of DevDebuggerSettingManager
		/// </summary>
		public static DevDebuggerSettingManager Singleton => _SINGLETON;

		private void Awake() {
			SetSingleton();
		}

		private void SetSingleton() {
			if (_SINGLETON == null)
				_SINGLETON = this;

			if (_SINGLETON != this)
				Destroy(gameObject);
		}
	}
}