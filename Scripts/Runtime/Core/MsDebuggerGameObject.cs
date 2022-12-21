using MakotoStudio.Debugger.Utils;
using UnityEngine;

namespace MakotoStudio.Debugger.Core {
	/// <summary>
	/// Place this script on game objects you want to high-light
	/// </summary>
	public class MsDebuggerGameObject : MonoBehaviour {
		private Material m_Material;
		private bool m_IsNewAdded;
		private bool m_IsHighLighted;

		public bool GetIsHighLighted => m_IsHighLighted;

		private void OnEnable() {
			if (!Debug.isDebugBuild) {
				Destroy(this);
			}
		}

		/// <summary>
		/// Resets the high light state and resets the material to the original material
		/// </summary>
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

		/// <summary>
		///  Apply the high light state if tag has no material configured the given material will be used
		/// </summary>
		/// <param name="material">Default Material if no material is configured for this game object tag</param>
		public void OnHighLightEvent(Material material) {
			var meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
			var match = DevDebuggerSettingManager.Singleton.MsDebuggerSettings.DebugObjectTagColors.Find(m =>
				gameObject.CompareTag(m.Name));
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