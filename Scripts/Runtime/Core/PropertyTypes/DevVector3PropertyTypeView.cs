using System.Reflection;
using System.Threading.Tasks;
using MakotoStudio.Debugger.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UObject = UnityEngine.Object;

namespace MakotoStudio.Debugger.Core.PropertyTypes {
	public class DevVector3PropertyTypeView : MonoBehaviour, IPropertyType {
		[SerializeField] private InputField xValueInput;
		[SerializeField] private InputField yValueInput;
		[SerializeField] private InputField zValueInput;

		private bool m_LiveValueUpdate;

		private DevComponentProperty m_DevComponentProperty;
		private Toggle m_LiveViewToggle;

		private PropertyInfo m_PropertyInfo;
		private UObject m_Obj;
		private Vector3 m_Vector;

		public void SetPropertyInfo(PropertyInfo propertyInfo, UObject obj) {
			m_PropertyInfo = propertyInfo;
			m_Obj = obj;
			SetValue();
		}

		public void SetLiveUpdate(bool state) {
			m_LiveViewToggle.isOn = true;
		}

		private void SetValue() {
			m_Vector = (Vector3) m_PropertyInfo.GetValue(m_Obj);
			SetValuesInView();
		}

		private void SetValuesInView() {
			xValueInput.SetTextWithoutNotify($"{m_Vector.x:N5}"); //xValue.ToString(CultureInfo.InvariantCulture);
			yValueInput.SetTextWithoutNotify($"{m_Vector.y:N5}"); //yValue.ToString(CultureInfo.InvariantCulture);
			zValueInput.SetTextWithoutNotify($"{m_Vector.z:N5}"); //zValue.ToString(CultureInfo.InvariantCulture);
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
		}

		private void InputFiledChangeEvent(string arg0) {
			m_Vector.x = float.Parse(xValueInput.text);
			m_Vector.y = float.Parse(yValueInput.text);
			m_Vector.z = float.Parse(zValueInput.text);
			m_PropertyInfo.SetValue(m_Obj, m_Vector);
		}

		private void ToggleValueChange(bool arg0) {
			m_LiveValueUpdate = m_LiveViewToggle.isOn;
		}
	}
}