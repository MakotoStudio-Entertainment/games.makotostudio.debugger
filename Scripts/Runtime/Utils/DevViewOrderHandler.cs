using MakotoStudio.Debugger.Interfaces;
using UnityEngine;

namespace MakotoStudio.Debugger.Utils {
	public class DevViewOrderHandler : MonoBehaviour {
		public static DevViewOrderHandler Singleton;

		private void Start() {
			if (Singleton == null) {
				Singleton = this;
			}

			if (Singleton != this) {
				Destroy(this);
			}
		}

		public void SetViewOnTop(IViewOrder viewOrder) {
			var viewsCount = gameObject.GetComponentsInChildren<IViewOrder>().Length;
			viewOrder.SetAtSiblingIndex(viewsCount);
		}
	}
}