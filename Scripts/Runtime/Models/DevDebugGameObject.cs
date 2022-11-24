using System;
using System.Collections.Generic;
using MakotoStudio.Debugger.Core;
using UnityEngine;

namespace MakotoStudio.Debugger.Models {
	[Serializable]
	public class DevDebugGameObject {
		public GameObject GameObject;
		public DevGameObject DevGameObject;
		public string Name;
		public bool ActiveState;
		public bool HighLightedState;
		public string Tag;
		public int Layer;
		public List<Component> AttatchedComponents;
	}
}