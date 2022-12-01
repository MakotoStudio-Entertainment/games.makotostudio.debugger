using System.Linq;
using MakotoStudio.Debugger.Core;
using MakotoStudio.Debugger.Interfaces;
using MakotoStudio.Debugger.Models;
using MakotoStudio.Debugger.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace MakotoStudio.Debugger.Views {
	public class DevGameObjectListView : MonoBehaviour, IViewOrder {
		[Space(20)] [Header("Scroll Views")] [Header("Game Object View")] [SerializeField]
		private Button btnListAllGameObjects;

		[SerializeField] private Button btnReloadListAllGameObjects;
		[SerializeField] private RectTransform prefabGameObjectView;
		[SerializeField] private RectTransform gameObjectView;

		/// <summary>
		///		Set this viewOrder to the front
		/// </summary>
		public void SetToFront() {
			DevViewOrderHandler.Singleton.SetViewOnTop(this);
		}

		/// <summary>
		///		Set the game object to the sibling index
		/// </summary>
		/// <param name="index">Index to set.</param>
		public void SetAtSiblingIndex(int index) {
			transform.SetSiblingIndex(index);
		}

		/// <summary>
		///		Set view active or inactive based on activeSelf
		/// </summary>
		public void SetActiveView() {
			gameObject.SetActive(!gameObject.activeSelf);
		}

		private void Awake() {
			DebuggerUIUtil.BindButtonUnityAction(btnListAllGameObjects, () => {
				btnListAllGameObjects.gameObject.SetActive(false);
				InitGameObjectView();
				btnReloadListAllGameObjects.interactable = true;
			});
			DebuggerUIUtil.BindButtonUnityAction(btnReloadListAllGameObjects, InitGameObjectView);
		}

		private void InitGameObjectView() {
			DestroyAllExistingInView();

			GameObjectsUtil.GetAllGameObjectsInScene().ForEach(go => {
				var newGo = Instantiate(prefabGameObjectView, gameObjectView);
				var msDebuggerGameObject = go.GetComponent<MsDebuggerGameObject>();
				if (msDebuggerGameObject != null) {
					msDebuggerGameObject.enabled = true;
				}

				newGo.gameObject.GetComponent<DevDebugGameObjectInfo>().SetDevDebugGameObject(new DevDebugObjectInformation {
					GameObject = go,
					MSDebuggerGameObject = msDebuggerGameObject,
					Name = go.name,
					ActiveState = go.activeSelf,
					HighLightedState = false,
					Tag = go.tag,
					Layer = go.layer,
					AttatchedComponents = go.GetComponents<Component>().ToList()
				});
			});
		}

		private void DestroyAllExistingInView() {
			var gameObjects = gameObjectView.gameObject.GetComponentsInChildren<DevDebugGameObjectInfo>();
			foreach (var componentsInChild in gameObjects) {
				Destroy(componentsInChild.gameObject);
			}
		}
	}
}