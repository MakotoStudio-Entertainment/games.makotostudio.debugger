using System.Collections.Generic;
using MakotoStudio.Debugger.Utils;
using UnityEngine;

namespace MakotoStudio.Debugger.Core {
	public class DevGameObject : MonoBehaviour {
		public string TestString { get; set; }
		public bool TestBool { get; set; }
		public int TestInt { get; set; }
		public Vector2 TestVector2 { get; set; }
		public Vector3 TestVector3 { get; set; }
		public Quaternion TestQuaternion { get; set; }

		private Material m_Material;
		private bool m_IsNewAdded;
		private bool m_IsHighLighted;

		// private List<DebugObjectColor> m_DebugObjectLayersColors;
		private List<Component> m_GameObjectComponents;


		public bool GetIsHighLighted => m_IsHighLighted;


		private void OnEnable() {
			TestString = "TEST MY FRIEND";
			if (!Debug.isDebugBuild) {
				Destroy(this);
			}

			DevBuildEventHandler.Singleton.OnSetHighLightTagEvent -= OnHighLightEvent;
			DevBuildEventHandler.Singleton.OnSetHighLightTagEvent += OnHighLightEvent;

			DevBuildEventHandler.Singleton.OnResetHighLightTagEvent -= OnResetHighLightEvent;
			DevBuildEventHandler.Singleton.OnResetHighLightTagEvent += OnResetHighLightEvent;

			// m_DebugObjectLayersColors = DevMaterialUtil.Singleton.MsDebuggerSettings.DebugObjectLayerColors;
		}

		private void OnEditGameObject() {
			// Load all components on GameObject
			m_GameObjectComponents = GameObjectsUtil.GameObjectComponentsLoader(gameObject);
		}

		private void OnDestroy() {
			DevBuildEventHandler.Singleton.OnSetHighLightTagEvent -= OnHighLightEvent;
			DevBuildEventHandler.Singleton.OnResetHighLightTagEvent -= OnResetHighLightEvent;
		}

		public void OnResetHighLightEvent() {
			var meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
			if (m_IsNewAdded) {
				Destroy(meshRenderer);
			}
			else {
				meshRenderer.material = m_Material;
			}

			m_IsHighLighted = false;
		}


		public void OnHighLightEvent(Material material) {
			var meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
			var match = DevMaterialUtil.Singleton.MsDebuggerSettings.DebugObjectTagColors.Find(m => gameObject.CompareTag(m.Name));
			if (match != null) {
				material = match.ColorMaterial;
			}

			if (meshRenderer != null) {
				m_Material = meshRenderer.material;
				meshRenderer.material = material;
			}
			else {
				this.gameObject.AddComponent<MeshRenderer>().material = material;
				m_IsNewAdded = true;
			}

			m_IsHighLighted = true;
		}
	}
}