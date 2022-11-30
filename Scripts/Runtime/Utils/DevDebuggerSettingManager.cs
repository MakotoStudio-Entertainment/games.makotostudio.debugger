using MakotoStudio.Debugger.ScriptableObjects;
using UnityEngine;

namespace MakotoStudio.Debugger.Utils {
	public class DevDebuggerSettingManager : MonoBehaviour {
		public static DevDebuggerSettingManager Singleton;
		public MsDebuggerSettings MsDebuggerSettings => msDebuggerSettings;
		[SerializeField] private MsDebuggerSettings msDebuggerSettings;

		private void Awake() {
			SetSingleton();
		}

		private void SetSingleton() {
			if (Singleton == null)
				Singleton = this;

			if (Singleton != this)
				Destroy(gameObject);
		}
	}
}