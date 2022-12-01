using System.Reflection;
using MakotoStudio.Debugger.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace MakotoStudio.Debugger.Core.PropertyTypes {
	public class DevVector2PropertyTypeView : MonoBehaviour, IPropertyType {
		[SerializeField] private InputField xValueInput;
		[SerializeField] private InputField yValueInput;

		private bool m_LiveValueUpdate;

		private DevComponentProperty m_DevComponentProperty;
		private Toggle m_LiveViewToggle;

		private PropertyInfo m_PropertyInfo;
		private UnityEngine.Object m_Obj;
		private Vector2 m_Vector;

		public void SetPropertyInfo(PropertyInfo propertyInfo, Object obj) {
			m_PropertyInfo = propertyInfo;
			m_Obj = obj;
			SetValue();
		}
			
		public void SetLiveUpdate(bool state) {
			m_LiveViewToggle.isOn = state;
		}

		private void SetValue() {
			m_Vector = (Vector2) m_PropertyInfo.GetValue(m_Obj);
			SetValuesInView();
		}

		private void SetValuesInView() {
			xValueInput.SetTextWithoutNotify($"{m_Vector.x:N5}"); //xValue.ToString(CultureInfo.InvariantCulture);
			yValueInput.SetTextWithoutNotify($"{m_Vector.y:N5}"); //yValue.ToString(CultureInfo.InvariantCulture);
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

			xValueInput.onValueChanged = inputChangeEvent;
			yValueInput.onValueChanged = inputChangeEvent;
		}

		private void InputFiledChangeEvent(string arg0) {
			m_Vector.x = float.Parse(xValueInput.text);
			m_Vector.y = float.Parse(yValueInput.text);
			m_PropertyInfo.SetValue(m_Obj, m_Vector);
		}

		private void ToggleValueChange(bool arg0) {
			m_LiveValueUpdate = m_LiveViewToggle.isOn;
		}
	}
}