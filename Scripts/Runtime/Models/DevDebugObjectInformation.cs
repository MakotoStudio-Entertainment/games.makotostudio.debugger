using System;
using System.Collections.Generic;
using MakotoStudio.Debugger.Core;
using UnityEngine;

namespace MakotoStudio.Debugger.Models {
	[Serializable]
	public class DevDebugObjectInformation {
		[SerializeField] private GameObject gameObject;
		[SerializeField] private MsDebuggerGameObject msDebuggerGameObject;
		[SerializeField] private string name;
		[SerializeField] private bool activeState;
		[SerializeField] private bool highLightedState;
		[SerializeField] private string tag;
		[SerializeField] private int layer;
		[SerializeField] private List<Component> attatchedComponents;

		/// <summary>
		/// GameObject Property
		/// </summary>
		public GameObject GameObject {
			get => gameObject;
			set => gameObject = value;
		}

		/// <summary>
		/// MSDebuggerGameObject Property
		/// </summary>
		public MsDebuggerGameObject MSDebuggerGameObject {
			get => msDebuggerGameObject;
			set => msDebuggerGameObject = value;
		}

		/// <summary>
		/// Name Property
		/// </summary>
		public string Name {
			get => name;
			set => name = value;
		}

		/// <summary>
		/// ActiveState Property
		/// </summary>
		public bool ActiveState {
			get => activeState;
			set => activeState = value;
		}

		/// <summary>
		/// HighLightedState Property
		/// </summary>
		public bool HighLightedState {
			get => highLightedState;
			set => highLightedState = value;
		}

		/// <summary>
		/// Tag Property
		/// </summary>
		public string Tag {
			get => tag;
			set => tag = value;
		}

		/// <summary>
		/// Layer Property
		/// </summary>
		public int Layer {
			get => layer;
			set => layer = value;
		}

		/// <summary>
		/// AttatchedComponents Property
		/// </summary>
		public List<Component> AttatchedComponents {
			get => attatchedComponents;
			set => attatchedComponents = value;
		}
	}
}