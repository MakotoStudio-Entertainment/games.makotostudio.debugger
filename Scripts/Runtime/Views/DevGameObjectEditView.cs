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

		private DevDebugGameObject m_DevGameObject;
		private List<Component> m_Components;
		private Light m_ComponentsInfomration = new();

		public void SetDevGameObject(DevDebugGameObject devGameObject) {
			m_DevGameObject = devGameObject;
			viewTitle.text = m_DevGameObject.Name;
		}

		public void OnBarClickEnd() {
			DevViewOrderHandler.Singleton.SetViewOnTop(this);
		}

		public void SetAtIndex(int newIndex) {
			transform.SetSiblingIndex(newIndex);
		}

		public void SetActiveView() {
			Destroy(gameObject);
		}

		public void LoadGameObjectComponents() {
			btnLoadComponents.gameObject.SetActive(false);
			StartCoroutine(InitComponents());
		}

		private IEnumerator InitComponents() {
			foreach (var componentInfo in m_DevGameObject.AttatchedComponents) {
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