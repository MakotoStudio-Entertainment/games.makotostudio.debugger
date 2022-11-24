using System;
using UnityEngine;

namespace MakotoStudio.Debugger.Models {
	[Serializable]
	public class DebugObjectColor {
		[SerializeField] private string name;
		[SerializeField] private Material colorMaterial;

		public string Name {
			get => name;
			set => name = value;
		}

		public Material ColorMaterial {
			get => colorMaterial;
			set => colorMaterial = value;
		}
	}
}