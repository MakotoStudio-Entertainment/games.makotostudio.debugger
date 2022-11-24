using System.Linq;
using MakotoStudio.Debugger.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MakotoStudio.Debugger.Utils {
	public class DevWindowRepositionHelper : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerClickHandler {
		[SerializeField] private RectTransform rootWindow;

		private Vector2 m_LastMousePosition;
		private IViewOrder m_ViewOrder;

		public void OnBeginDrag(PointerEventData eventData) {
			SetToFront();
			m_LastMousePosition = eventData.position;
		}

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

		public void OnPointerClick(PointerEventData pointerEventData) {
			SetToFront();
		}

		private void SetToFront() {
			m_ViewOrder.OnBarClickEnd();
		}

		public void SetActiveView() {
			m_ViewOrder.SetActiveView();
		}

		private void Awake() {
			m_ViewOrder = rootWindow.GetComponent<IViewOrder>();
		}
	}
}