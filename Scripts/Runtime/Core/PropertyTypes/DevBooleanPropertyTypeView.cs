using System.Reflection;
using MakotoStudio.Debugger.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UObject = UnityEngine.Object;

namespace MakotoStudio.Debugger.Core.PropertyTypes {
	/// <summary>
	/// Manages the Boolean type Property Type View
	/// </summary>
	public class DevBooleanPropertyTypeView : MonoBehaviour, IPropertyType {
		[SerializeField] private Toggle propertyToggle;

		private bool m_LiveValueUpdate;
		private DevComponentProperty m_DevComponentProperty;
		private Toggle m_LiveViewToggle;
		private PropertyInfo m_PropertyInfo;
		private UObject m_Obj;
		private bool m_PropertyValue;

		/// <summary>
		///  Set Property info and the object of the proprty
		/// </summary>
		/// <param name="propertyInfo">Property info</param>
		/// <param name="obj">The Object of the property holder</param>
		public void SetPropertyInfo(PropertyInfo propertyInfo, UObject obj) {
			m_PropertyInfo = propertyInfo;
			m_Obj = obj;
			SetValue();
		}

		/// <summary>
		///  Set if live update the value of the property
		/// </summary>
		/// <param name="state">Change the live update state</param>
		public void SetLiveUpdate(bool state) {
			m_LiveViewToggle.isOn = state;
		}

		private void SetValue() {
			m_PropertyValue = (bool) m_PropertyInfo.GetValue(m_Obj);
			SetValuesInView();
		}

		private void SetValuesInView() {
			propertyToggle.SetIsOnWithoutNotify(m_PropertyValue);
		}

		private void Update() {
			if (m_LiveValueUpdate) {
				SetValue();
			}
		}

		private void Awake() {
			m_DevComponentProperty = gameObject.GetComponentInParent<DevComponentProperty>();
			m_LiveViewToggle = m_DevComponentProperty.GetLiveViewToggle;
			m_LiveValueUpdate = m_LiveViewToggle.isOn;

			var toggleEvent = new Toggle.ToggleEvent();
			toggleEvent.AddListener(ToggleValueChange);
			m_LiveViewToggle.onValueChanged = toggleEvent;

			var toggleEventBoolean = new Toggle.ToggleEvent();
			toggleEventBoolean.AddListener(PropertyValueChange);
			propertyToggle.onValueChanged = toggleEventBoolean;
		}

		private void PropertyValueChange(bool arg0) {
			m_PropertyInfo.SetValue(m_Obj, propertyToggle.isOn);
		}

		private void ToggleValueChange(bool arg0) {
			m_LiveValueUpdate = m_LiveViewToggle.isOn;
		}
	}
}