using System.Reflection;
using MakotoStudio.Debugger.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using UObject = UnityEngine.Object;

namespace MakotoStudio.Debugger.Core {
	/// <summary>
	/// Component Property manager
	/// </summary>
	public class DevComponentProperty : MonoBehaviour {
		[SerializeField] private Text propertyName;
		[SerializeField] private Toggle liveValueViewToggle;

		[SerializeField] private GameObject prefabReadonly;
		[SerializeField] private GameObject prefabQuaternion;
		[SerializeField] private GameObject prefabVector3;
		[SerializeField] private GameObject prefabVector2;
		[SerializeField] private GameObject prefabString;
		[SerializeField] private GameObject prefabInt;
		[SerializeField] private GameObject prefabEnum;
		[SerializeField] private GameObject prefabBoolean;

		private PropertyInfo m_PropertyInfo;
		private UObject m_Obj;

		/// <summary>
		///  Get Toggle fom component
		/// </summary>
		public Toggle GetLiveViewToggle => liveValueViewToggle;

		/// <summary>
		/// Instantiate Component Property
		/// </summary>
		/// <param name="propertyInfo">Property info</param>
		/// <param name="obj">Object from the component</param>
		/// <returns>Property type</returns>
		public IPropertyType Init(PropertyInfo propertyInfo, UObject obj) {
			liveValueViewToggle.isOn = false;
			m_PropertyInfo = propertyInfo;
			m_Obj = obj;
			propertyName.text = propertyInfo.Name;
			return SetPropertyValue();
		}

		private IPropertyType SetPropertyValue() {
			switch (m_PropertyInfo.PropertyType.Name) {
				case "Vector3":
					if (!m_PropertyInfo.CanWrite)
						return SetValueReadOnly(m_PropertyInfo, m_Obj);
					var goVec3 = Instantiate(prefabVector3, transform).GetComponent<IPropertyType>();
					goVec3.SetPropertyInfo(m_PropertyInfo, m_Obj);
					return goVec3;
				case "Vector2":
					if (!m_PropertyInfo.CanWrite)
						return SetValueReadOnly(m_PropertyInfo, m_Obj);
					var goVec2 = Instantiate(prefabVector2, transform).GetComponent<IPropertyType>();
					goVec2.SetPropertyInfo(m_PropertyInfo, m_Obj);
					return goVec2;
				case "Boolean":
					if (!m_PropertyInfo.CanWrite)
						return SetValueReadOnly(m_PropertyInfo, m_Obj);
					var goBoo = Instantiate(prefabBoolean, transform).GetComponent<IPropertyType>();
					goBoo.SetPropertyInfo(m_PropertyInfo, m_Obj);
					return goBoo;
				case "Quaternion":
					if (!m_PropertyInfo.CanWrite)
						return SetValueReadOnly(m_PropertyInfo, m_Obj);
					var goQua = Instantiate(prefabQuaternion, transform).GetComponent<IPropertyType>();
					goQua.SetPropertyInfo(m_PropertyInfo, m_Obj);
					return goQua;
				case "Int32":
					if (!m_PropertyInfo.CanWrite)
						return SetValueReadOnly(m_PropertyInfo, m_Obj);
					var goInt = Instantiate(prefabInt, transform).GetComponent<IPropertyType>();
					goInt.SetPropertyInfo(m_PropertyInfo, m_Obj);
					return goInt;
				default:
					return SetValueReadOnly(m_PropertyInfo, m_Obj);
			}
		}

		private IPropertyType SetValueReadOnly(PropertyInfo propertyInfo, UObject obj) {
			var go = Instantiate(prefabReadonly, transform).GetComponent<IPropertyType>();
			go.SetPropertyInfo(propertyInfo, obj);
			return go;
		}
	}
}