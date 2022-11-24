using System.Collections.Generic;
using System.Linq;
using MakotoStudio.Debugger.Core;
using MakotoStudio.Debugger.Interfaces;
using MakotoStudio.Debugger.Models;
using MakotoStudio.Debugger.Utils;
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

		public void OnBarClickEnd() {
			DevViewOrderHandler.Singleton.SetViewOnTop(this);
		}

		public void SetAtIndex(int newIndex) {
			transform.SetSiblingIndex(newIndex);
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
			var gameObjects = gameObjectView.gameObject.GetComponentsInChildren<DevDebugGameObjectInfo>();
			foreach (var componentsInChild in gameObjects) {
				Destroy(componentsInChild.gameObject);
			}

			var gameObjectsInScene = GetAllObjectsInScene();
			int initPosY = -15;
			gameObjectsInScene.ForEach(go => {
				var newGo = Instantiate(prefabGameObjectView, gameObjectView);
				newGo.gameObject.transform.localPosition = new Vector3(400, initPosY, 0);
				var devGameObject = go.GetComponent<DevGameObject>();
				if (devGameObject != null) {
					devGameObject.enabled = true;
				}
				newGo.gameObject.GetComponent<DevDebugGameObjectInfo>().SetDevDebugGameObject(new DevDebugGameObject {
					GameObject = go,
					DevGameObject = devGameObject,
					Name = go.name,
					ActiveState = go.activeSelf,
					HighLightedState = false,
					Tag = go.tag,
					Layer = go.layer,
					AttatchedComponents = go.GetComponents<Component>().ToList()
				});
				initPosY -= 30;
			});
		}

		private List<GameObject> GetAllObjectsInScene() {
			var allGameObjects = new List<GameObject>();
			var rootGameObjects = SceneManager.GetActiveScene().GetRootGameObjects().ToList();
			rootGameObjects.ForEach(go => {
				allGameObjects.Add(go);
				if (go.transform.childCount > 0) {
					allGameObjects.AddRange(GetChildren(go));
				}
			});
			return allGameObjects;
		}

		private IEnumerable<GameObject> GetChildren(GameObject go) {
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