using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using MakotoStudio.Debugger.Interfaces;
using MakotoStudio.Debugger.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace MakotoStudio.Debugger.Core {
	/// <summary>
	/// Contains and hold Component Information of game object
	/// </summary>
	public class DevGameObjectComponentInfo : MonoBehaviour {
		[SerializeField] private Text componentName;
		[SerializeField] private GameObject prefabProperty;
		[SerializeField] private RectTransform content;
		[SerializeField] private Toggle toggleEnableComponent;
		[SerializeField] private Toggle toggleEnableLiveListeningProperty;

		private List<IPropertyType> m_PropertyTypes;

		private Component m_Component;
		private PropertyInfo m_EnablePropertyInfo;

		/// <summary>
		///  Set the component to view
		/// </summary>
		/// <param name="componentInfo"></param>
		/// <returns>yield null</returns>
		public IEnumerator SetComponent(Component componentInfo) {
			m_Component = componentInfo;
			yield return InitView();
		}

		private IEnumerator InitView() {
			var toggleEventEnableComponent = new Toggle.ToggleEvent();
			toggleEventEnableComponent.AddListener(ToggleEventEnableChangeEvent);
			toggleEnableComponent.onValueChanged = toggleEventEnableComponent;

			m_PropertyTypes = new List<IPropertyType>();

			var toggleEventEnableLiveListeningProperty = new Toggle.ToggleEvent();
			toggleEventEnableLiveListeningProperty.AddListener(EnableLiveListeningPropertyChangeEvent);
			toggleEnableLiveListeningProperty.onValueChanged = toggleEventEnableLiveListeningProperty;
			yield return GetPropertyValues();
		}

		private void ToggleEventEnableChangeEvent(bool arg0) {
			m_EnablePropertyInfo.SetValue(m_Component, toggleEnableComponent.isOn);
		}

		private void EnableLiveListeningPropertyChangeEvent(bool arg0) {
			m_PropertyTypes.ForEach(prop => prop?.SetLiveUpdate(toggleEnableLiveListeningProperty.isOn));
		}

		private IEnumerator GetPropertyValues() {
			var propertyType = m_Component.GetType();
			componentName.text = propertyType.Name;

			if (SpecialComponent(propertyType.Name)) {
				toggleEnableComponent.interactable = false;
			}
			
			foreach (var propertyInfo in propertyType.GetProperties()) {
				try {
					if (propertyInfo.GetIndexParameters().Length == 0) {
						SetProperty(propertyInfo);
					}
				}
				catch {
					// ignored
				}

				yield return null;
			}
		}

		private void SetProperty(PropertyInfo propertyInfo) {
			if (SpecialProperty(propertyInfo))
				return;
			if (NotDisplayProperty(propertyInfo.Name))
				return;

			var go = Instantiate(prefabProperty, content.transform);
			var componentProperty = go.GetComponent<DevComponentProperty>();
			m_PropertyTypes.Add(componentProperty.Init(propertyInfo, m_Component));
		}

		private bool SpecialProperty(PropertyInfo propertyInfo) {
			switch (propertyInfo.Name.ToLower()) {
				case "enabled":
					m_EnablePropertyInfo = propertyInfo;
					toggleEnableComponent.isOn = (bool) propertyInfo.GetValue(m_Component);
					return true;
				default:
					return false;
			}
		}
		
		private bool SpecialComponent(string componentName) =>
			DevDebuggerSettingManager.Singleton.MsDebuggerSettings.ComponentsNotDisableList.Find(c =>
				componentName.ToLower().Equals(c.ToLower())) != null;

		private bool NotDisplayProperty(string propertyName) =>
			DevDebuggerSettingManager.Singleton.MsDebuggerSettings.PropertiesToIgnore.Find(p =>
				propertyName.ToLower().Equals(p.ToLower())) != null;
	}
}