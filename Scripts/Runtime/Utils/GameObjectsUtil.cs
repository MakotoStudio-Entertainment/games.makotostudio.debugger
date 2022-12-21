using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MakotoStudio.Debugger.Utils {
	/// <summary>
	/// Overall Game Object util
	/// </summary>
	public static class GameObjectsUtil {
		/// <summary>
		///  Find all game objects in current scene expect game objects with the configured ignore layer mask
		/// </summary>
		/// <returns>List of GameObject in current scene</returns>
		public static List<GameObject> GetAllGameObjectsInScene() {
			var gos = new List<GameObject>();
			var root = Object.FindObjectsOfType(typeof(GameObject)).ToList();
			var layerMaskIgnore = DevDebuggerSettingManager.Singleton.MsDebuggerSettings.LayerMaskField;
			root.ForEach(obj => {
				var go = (GameObject) obj;
				if (layerMaskIgnore == (layerMaskIgnore | (1 << go.layer)))
					return;
				
				gos.Add(go);
				if (go.transform.childCount > 0) {
					gos.AddRange(GetChildren(go));
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