using System.Collections.Generic;
using MakotoStudio.Debugger.ScriptableObjects;
using UnityEngine;

namespace MakotoStudio.Debugger.Utils {
	public class DevMaterialUtil : MonoBehaviour {
		public static DevMaterialUtil Singleton;
		public MsDebuggerSettings MsDebuggerSettings => msDebuggerSettings;
		[SerializeField] private MsDebuggerSettings msDebuggerSettings;

		private void Awake() {
			SetSingleton();
			InitDefaultValues();
		}

		private void InitDefaultValues() {
			if (MsDebuggerSettings.ComponentsNotDisableList.Count == 0) {
				var comList = new List<string> {
					"Transform",
					"MeshFilter"
				};
				MsDebuggerSettings.ComponentsNotDisableList.AddRange(comList);
			}

			if (MsDebuggerSettings.PropertiesToIgnore.Count == 0) {
				var propList = new List<string> {
					"tag",
					"name",
					"hideFlags",
					"transform",
					"gameObject",
					"sharedMaterial",
					"sharedMaterials",
					"materials",
					"rigidbody",
					"rigidbody2D",
					"camera",
					"light",
					"animation",
					"constantForce",
					"renderer",
					"audio",
					"networkView",
					"collider",
					"collider2D",
					"hingeJoint",
					"particleSystem",
				};
				MsDebuggerSettings.PropertiesToIgnore.AddRange(propList);
			}
		}

		private void SetSingleton() {
			if (Singleton == null)
				Singleton = this;

			if (Singleton != this)
				Destroy(this);
		}
	}
}