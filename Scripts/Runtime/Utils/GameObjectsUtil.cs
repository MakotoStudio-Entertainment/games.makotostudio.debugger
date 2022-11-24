using System.Collections.Generic;
using UnityEngine;

namespace MakotoStudio.Debugger.Utils {
	public static class GameObjectsUtil {
		public static List<Component> GameObjectComponentsLoader(GameObject gameObject) {
			List<Component> components = new List<Component>();
			gameObject.GetComponents(components);
			return components;
		}
	}
}