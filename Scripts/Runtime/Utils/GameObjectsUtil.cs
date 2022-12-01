using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakotoStudio.Debugger.Utils {
	public static class GameObjectsUtil {
		/// <summary>
		///  Find all game objects in current scene expect game objects with the DevIgnore layer mask
		/// </summary>
		/// <param name="layerName"></param>
		/// <returns>List of GameObject in current scene</returns>
		public static List<GameObject> GetAllGameObjectsInScene() {
			var gos = new List<GameObject>();
			var root = Object.FindObjectsOfType(typeof(GameObject)).ToList();
			int layerIgnore = LayerMask.NameToLayer("DevIgnore");
			root.ForEach(obj => {
				var go = (GameObject) obj;
				if (go.layer != layerIgnore) {
					gos.Add(go);
					if (go.transform.childCount > 0) {
						gos.AddRange(GetChildren(go));
					}
				}
			});
			return gos;
		}

		private static IEnumerable<GameObject> GetChildren(GameObject go) {
			var children = new List<GameObject>();
			for (var i = 0; i < go.transform.childCount; i++) {
				var child = go.transform.GetChild(i)?.gameObject;
				if (child == null)
					continue;
				children.Add(child);
				children.AddRange(GetChildren(child));
			}

			return children;
		}
	}
}