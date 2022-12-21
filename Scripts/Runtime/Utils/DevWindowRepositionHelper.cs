using System.Linq;
using MakotoStudio.Debugger.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MakotoStudio.Debugger.Utils {
	/// <summary>
	/// Window position helper class, this allows us to drag and drop the views and still stay inside the canvas view
	/// </summary>
	public class DevWindowRepositionHelper : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerClickHandler {
		[SerializeField] private RectTransform rootWindow;
		[SerializeField] private Button btnClose;

		private Vector2 m_LastMousePosition;
		private IViewOrder m_ViewOrder;

		/// <summary>
		/// Called by a BaseInputModule before a drag is started.
		/// </summary>
		public void OnBeginDrag(PointerEventData eventData) {
			m_ViewOrder.SetToFront();
			m_LastMousePosition = eventData.position;
		}

		/// <summary>
		/// When dragging is occurring this will be called every time the cursor is moved.
		/// </summary>
		public void OnDrag(PointerEventData eventData) {
			var currentMousePosition = eventData.position;
			var diff = currentMousePosition - m_LastMousePosition;
			var rect = rootWindow.GetComponent<RectTransform>();

			var position = rect.position;
			var newPosition = position + new Vector3(diff.x, diff.y, transform.position.z);
			var oldPos = position;
			position = newPosition;
			rect.position = position;
			if (!IsRectTransformInsideScreen(rect)) {
				rect.position = oldPos;
			}

			m_LastMousePosition = currentMousePosition;
		}

		/// <summary>
		/// Use this callback to detect clicks.
		/// </summary>
		public void OnPointerClick(PointerEventData pointerEventData) {
			m_ViewOrder.SetToFront();
		}

		private void Awake() {
			m_ViewOrder = rootWindow.GetComponent<IViewOrder>();
			DebuggerUIUtil.BindButtonUnityAction(btnClose, () => { m_ViewOrder.SetActiveView(); });
		}

		private bool IsRectTransformInsideScreen(RectTransform rectTransform) {
			var isInside = false;
			var corners = new Vector3[4];
			rectTransform.GetWorldCorners(corners);
			var rect = new Rect(0, 0, Screen.width, Screen.height);
			var visibleCorners = corners.Count(corner => rect.Contains(corner));

			if (visibleCorners == 4) {
				isInside = true;
			}

			return isInside;
		}
	}
}