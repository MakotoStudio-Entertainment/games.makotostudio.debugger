using System.Reflection;
using MakotoStudio.Debugger.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UObject = UnityEngine.Object;

namespace MakotoStudio.Debugger.Core.PropertyTypes {
	public class DevQuaternionPropertyTypeView : MonoBehaviour, IPropertyType {
		[SerializeField] private InputField xValueInput;
		[SerializeField] private InputField yValueInput;
		[SerializeField] private InputField zValueInput;
		[SerializeField] private InputField wValueInput;

		private bool m_LiveValueUpdate;

		private DevComponentProperty m_DevComponentProperty;
		private Toggle m_LiveViewToggle;

		private PropertyInfo m_PropertyInfo;
		private UObject m_Obj;
		private Quaternion m_Quaternion;

		public void SetPropertyInfo(PropertyInfo propertyInfo, UObject obj) {
			m_PropertyInfo = propertyInfo;
			m_Obj = obj;
			SetValue();
		}

		public void SetLiveUpdate(bool state) {
			m_LiveViewToggle.isOn = true;
		}

		private void SetValue() {
			m_Quaternion = (Quaternion) m_PropertyInfo.GetValue(m_Obj);
			SetValuesInView();
		}

		private void SetValuesInView() {
			xValueInput.SetTextWithoutNotify($"{m_Quaternion.x:N5}"); //xValue.ToString(CultureInfo.InvariantCulture);
			yValueInput.SetTextWithoutNotify($"{m_Quaternion.y:N5}"); //yValue.ToString(CultureInfo.InvariantCulture);
			zValueInput.SetTextWithoutNotify($"{m_Quaternion.z:N5}"); //zValue.ToString(CultureInfo.InvariantCulture);
			wValueInput.SetTextWithoutNotify($"{m_Quaternion.w:N5}"); //zValue.ToString(CultureInfo.InvariantCulture);
		}

		private void Update() {
			if (m_LiveValueUpdate) {
				SetValue();
			}
		}

		private void Awake() {
			m_DevComponentProperty = gameObject.GetComponentInParent<DevComponentProperty>();
			m_LiveViewToggle = m_DevComponentProperty.GetLiveViewToggle();
			m_LiveValueUpdate = m_LiveViewToggle.isOn;

			var toggleEvent = new Toggle.ToggleEvent();
			toggleEvent.AddListener(ToggleValueChange);
			m_LiveViewToggle.onValueChanged = toggleEvent;

			var inputChangeEvent = new InputField.OnChangeEvent();
			inputChangeEvent.AddListener(InputFiledChangeEvent);

			xValueInput.onValueChanged = inputChangeEvent;
			yValueInput.onValueChanged = inputChangeEvent;
			zValueInput.onValueChanged = inputChangeEvent;
			wValueInput.onValueChanged = inputChangeEvent;
		}

		private void InputFiledChangeEvent(string arg0) {
			m_Quaternion.x = float.Parse(xValueInput.text);
			m_Quaternion.y = float.Parse(yValueInput.text);
			m_Quaternion.z = float.Parse(zValueInput.text);
			m_Quaternion.w = float.Parse(wValueInput.text);
			m_PropertyInfo.SetValue(m_Obj, m_Quaternion);
		}

		private void ToggleValueChange(bool arg0) {
			m_LiveValueUpdate = m_LiveViewToggle.isOn;
		}
	}
}