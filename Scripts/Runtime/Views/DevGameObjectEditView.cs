using System.Collections;
using System.Collections.Generic;
using MakotoStudio.Debugger.Core;
using MakotoStudio.Debugger.Interfaces;
using MakotoStudio.Debugger.Models;
using MakotoStudio.Debugger.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace MakotoStudio.Debugger.Views {
	/// <summary>
	/// Game Object Edit view, displays all components on current game object
	/// </summary>
	public class DevGameObjectEditView : MonoBehaviour, IViewOrder {
		[SerializeField] private Text viewTitle;
		[SerializeField] private Button btnLoadComponents;
		[SerializeField] private RectTransform contentView;
		[SerializeField] private GameObject prefabComponentInfo;

		private DevDebugObjectInformation m_DevObjectInformation;
		private List<Component> m_Components;

		/// <summary>
		///  Set the DevGameObjectInformation in the GameObjectEditView
		/// </summary>
		/// <param name="devObjectInformation"></param>
		public void SetDevDebugGameObject(DevDebugObjectInformation devObjectInformation) {
			m_DevObjectInformation = devObjectInformation;
			viewTitle.text = m_DevObjectInformation.Name;
		}

		/// <summary>
		///		Set this viewOrder to the front
		/// </summary>
		public void SetToFront() {
			DevViewOrderHandler.Singleton.SetViewOnTop(this);
		}

		/// <summary>
		/// Set the game object to the sibling index
		/// </summary>
		/// <param name="index">Index to set.</param>
		public void SetAtSiblingIndex(int index) {
			transform.SetSiblingIndex(index);
		}

		/// <summary>
		///		Destroy View if no longer used
		/// </summary>
		public void SetActiveView() {
			Destroy(gameObject);
		}


		private IEnumerator InitComponents(DevDebugObjectInformation devObjectInformation) {
			foreach (var componentInfo in devObjectInformation.AttatchedComponents) {
				if (IgnoreComponent(componentInfo.GetType().Name))
					continue;
				
				var go = Instantiate(prefabComponentInfo, contentView);
				yield return go.GetComponent<DevGameObjectComponentInfo>().SetComponent(componentInfo);
			}

			// Force rerender after finish set all components and properties
			gameObject.SetActive(false);
			gameObject.SetActive(true);
		}

		private void Awake() {
			DebuggerUIUtil.BindButtonUnityAction(btnLoadComponents, LoadGameObjectComponents);
		}

		private void LoadGameObjectComponents() {
			btnLoadComponents.gameObject.SetActive(false);
			StartCoroutine(InitComponents(m_DevObjectInformation));
		}

		private bool IgnoreComponent(string componentName) =>
			DevDebuggerSettingManager.Singleton.MsDebuggerSettings.ComponentsToIgnoreList.Find(c =>
				componentName.ToLower().Equals(c.ToLower())) != null;
	}
}