using System;
using UnityEngine;

namespace MakotoStudio.Debugger.Utils {
	public class DevBuildEventHandler : MonoBehaviour {
		public static DevBuildEventHandler Singleton;

		private bool m_IsHighLightTag;
		private bool m_IsHighLightLayer;

		public event Action<Material> OnSetHighLightTagEvent;
		public event Action OnResetHighLightTagEvent;

		public event Action<Material> OnSetHighLightLayerEvent;
		public event Action OnResetHighLightLayerEvent;

		public void SetHighLightTagEvent() {
			if (m_IsHighLightTag) {
				OnResetHighLightTagEvent?.Invoke();
				m_IsHighLightTag = false;
			}
			else {
				OnSetHighLightTagEvent?.Invoke(DevMaterialUtil.Singleton.MsDebuggerSettings.defaultMaterial);
				m_IsHighLightTag = true;
			}
		}

		public void SetHighLightLayerEvent() {
			if (m_IsHighLightLayer) {
				OnResetHighLightLayerEvent?.Invoke();
				m_IsHighLightLayer = false;
			}
			else {
				OnSetHighLightLayerEvent?.Invoke(DevMaterialUtil.Singleton.MsDebuggerSettings.defaultLayerMaterial);
				m_IsHighLightLayer = true;
			}
		}

		private void Awake() {
			SetSingleton();
		}

		private void SetSingleton() {
			if (Singleton == null)
				Singleton = this;

			if (Singleton != this)
				Destroy(this);
		}
	}
}