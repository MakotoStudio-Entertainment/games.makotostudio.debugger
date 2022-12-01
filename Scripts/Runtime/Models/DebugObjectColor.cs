using System;
using UnityEngine;

namespace MakotoStudio.Debugger.Models {
	[Serializable]
	public class DebugObjectColor {
		[SerializeField] private string name;
		[SerializeField] private Material colorMaterial;

		/// <summary>
		/// Name Property
		/// </summary>
		public string Name {
			get => name;
			set => name = value;
		}

		/// <summary>
		/// ColorMaterial Property
		/// </summary>
		public Material ColorMaterial {
			get => colorMaterial;
			set => colorMaterial = value;
		}
	}
}