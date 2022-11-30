using System.Collections;
using System.Collections.Generic;
using MakotoStudio.Debugger.Core;
using MakotoStudio.Debugger.Interfaces;
using MakotoStudio.Debugger.Models;
using MakotoStudio.Debugger.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace MakotoStudio.Debugger.Views {
	public class DevGameObjectEditView : MonoBehaviour, IViewOrder {
		[SerializeField] private Text viewTitle;
		[SerializeField] private Button btnLoadComponents;
		[SerializeField] private RectTransform contentView;
		[SerializeField] private GameObject prefabComponentInfo;

		private DevDebugObjectInformation m_DevObjectInformation;
		private List<Component> m_Components;
		private Light m_ComponentsInfomration = new();

		public void SetDevDebugGameObject(DevDebugObjectInformation devObjectInformation) {
			m_DevObjectInformation = devObjectInformation;
			viewTitle.text = m_DevObjectInformation.Name;
		}

		public void OnBarClickEnd() {
			DevViewOrderHandler.Singleton.SetViewOnTop(this);
		}

		public void SetAtSiblingIndex(int index) {
			transform.SetSiblingIndex(index);
		}

		/// <summary>
		///		Destroy View if no longer used
		/// </summary>
		public void SetActiveView() {
			Destroy(gameObject);
		}

		public void LoadGameObjectComponents() {
			btnLoadComponents.gameObject.SetActive(false);
			StartCoroutine(InitComponents(m_DevObjectInformation));
		}

		private IEnumerator InitComponents(DevDebugObjectInformation devObjectInformation) {
			foreach (var componentInfo in devObjectInformation.AttatchedComponents) {
				var go = Instantiate(prefabComponentInfo, contentView);
				yield return go.GetComponent<DevGameObjectComponentInfo>().SetComponent(componentInfo);
			}

			// Force rerender after finish set all components and properties
			gameObject.SetActive(false);
			gameObject.SetActive(true);
		}

		private void Awake() {
			InitUi();
		}

		private void InitUi() {
			var btnEventLoadComponents = new Button.ButtonClickedEvent();
			btnEventLoadComponents.AddListener(LoadGameObjectComponents);
			btnLoadComponents.onClick = btnEventLoadComponents;
		}
	}
}