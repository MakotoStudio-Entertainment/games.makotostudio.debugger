using System.Reflection;
using MakotoStudio.Debugger.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UObject = UnityEngine.Object;

namespace MakotoStudio.Debugger.Core.PropertyTypes {
	public class DevIntegerPropertyTypeView : MonoBehaviour, IPropertyType {
		[SerializeField] private InputField valueInput;
		
		private bool m_LiveValueUpdate;

		private DevComponentProperty m_DevComponentProperty;
		private Toggle m_LiveViewToggle;

		private PropertyInfo m_PropertyInfo;
		private UObject m_Obj;
		private int m_Integer;

		public void SetPropertyInfo(PropertyInfo propertyInfo, UObject obj) {
			m_PropertyInfo = propertyInfo;
			m_Obj = obj;
			SetValue();
		}

		public void SetLiveUpdate(bool state) {
			m_LiveViewToggle.isOn = state;
		}

		private void SetValue() {
			m_Integer = (int) m_PropertyInfo.GetValue(m_Obj);
			SetValuesInView();
		}

		private void SetValuesInView() {
			valueInput.SetTextWithoutNotify($"{m_Integer}"); //xValue.ToString(CultureInfo.InvariantCulture);
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

			var inputChangeEvent = new InputField.OnChangeEvent();
			inputChangeEvent.AddListener(InputFiledChangeEvent);

			valueInput.onValueChanged = inputChangeEvent;

		}

		private void InputFiledChangeEvent(string arg0) {
			m_Integer = int.Parse(valueInput.text);
			m_PropertyInfo.SetValue(m_Obj, m_Integer);
		}

		private void ToggleValueChange(bool arg0) {
			m_LiveValueUpdate = m_LiveViewToggle.isOn;
		}
	}
}