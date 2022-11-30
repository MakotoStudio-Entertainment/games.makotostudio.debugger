using System.Collections.Generic;
using System.Linq;
using MakotoStudio.Debugger.Core;
using MakotoStudio.Debugger.Interfaces;
using MakotoStudio.Debugger.Models;
using MakotoStudio.Debugger.ScriptableObjects;
using MakotoStudio.Debugger.Utils;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MakotoStudio.Debugger.Views {
	public class DevGameObjectListView : MonoBehaviour, IViewOrder {
		[Space(20)] [Header("Scroll Views")] [Header("Game Object View")] [SerializeField]
		private Button btnListAllGameObjects;

		[SerializeField] private Button btnReloadListAllGameObjects;
		[SerializeField] private RectTransform prefabGameObjectView;
		[SerializeField] private RectTransform gameObjectView;
		[SerializeField] private MsDebuggerSettings msDebuggerSettings;

		public void OnBarClickEnd() {
			DevViewOrderHandler.Singleton.SetViewOnTop(this);
		}

		public void SetAtSiblingIndex(int index) {
			transform.SetSiblingIndex(index);
		}

		public void SetActiveView() {
			gameObject.SetActive(!gameObject.activeSelf);
		}

		private void Awake() {
			InitUi();
		}

		private void InitUi() {
			var listGoEvent = new Button.ButtonClickedEvent();
			listGoEvent.AddListener(ListAllGameObjectsEvent);
			btnListAllGameObjects.onClick = listGoEvent;

			var reloadListGoEvent = new Button.ButtonClickedEvent();
			reloadListGoEvent.AddListener(ReloadALlGameObjectsEvent);
			btnReloadListAllGameObjects.onClick = reloadListGoEvent;
		}

		private void ListAllGameObjectsEvent() {
			btnListAllGameObjects.gameObject.SetActive(false);
			InitGameObjectView();
			btnReloadListAllGameObjects.interactable = true;
		}

		private void ReloadALlGameObjectsEvent() {
			InitGameObjectView();
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
					MsDebuggerGameObject = msDebuggerGameObject,
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