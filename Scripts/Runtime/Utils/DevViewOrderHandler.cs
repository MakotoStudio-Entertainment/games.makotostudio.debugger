using MakotoStudio.Debugger.Interfaces;
using UnityEngine;

namespace MakotoStudio.Debugger.Utils {
	/// <summary>
	/// View handler, manages the view order for all debugger views
	/// </summary>
	public class DevViewOrderHandler : MonoBehaviour {
		private static DevViewOrderHandler _SINGLETON;

		/// <summary>
		///  Get Instance of DevViewOrderHandler
		/// </summary>
		public static DevViewOrderHandler Singleton => _SINGLETON;

		/// <summary>
		///  Set the viewOrder to the front
		/// </summary>
		/// <param name="viewOrder"></param>
		public void SetViewOnTop(IViewOrder viewOrder) {
			var viewsCount = gameObject.GetComponentsInChildren<IViewOrder>().Length;
			viewOrder.SetAtSiblingIndex(viewsCount);
		}

		private void Start() {
			if (_SINGLETON == null) {
				_SINGLETON = this;
			}

			if (_SINGLETON != this) {
				Destroy(this);
			}
		}
	}
}