using MakotoStudio.Debugger.Models;
using MakotoStudio.Debugger.Utils;
using MakotoStudio.Debugger.Views;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MakotoStudio.Debugger.Core {
	public class DevDebugGameObjectInfo : MonoBehaviour {
		[SerializeField] private TMP_Text gameObjectName;
		[SerializeField] private TMP_Text gameObjectTag;
		[SerializeField] private TMP_Text gameObjectLayer;
		[SerializeField] private Button btnGameObjectButtonHighLight;
		[SerializeField] private Button btnGameObjectButtonDisable;
		[SerializeField] private Button btnEditGameObject;

		[Header("Edit GameObject view")] [SerializeField]
		private GameObject prefabGameObjectEditView;

		private Transform m_RootTransform;
		private DevDebugGameObject m_DevDebugGameObject;
		private TMP_Text m_BtnHighLightText;
		private TMP_Text m_BtnDisableText;
		private TMP_Text m_BtnEditText;
		private DevGameObject m_DevGameObject;
		private bool m_IsEventSystem;


		public void SetDevDebugGameObject(DevDebugGameObject value) {
			m_DevDebugGameObject = value;
			m_DevGameObject = m_DevDebugGameObject.GameObject.GetComponent<DevGameObject>();
			SetIsEventListener();

			gameObjectName.text = m_DevDebugGameObject.Name;
			gameObjectTag.text = m_DevDebugGameObject.Tag;
			gameObjectLayer.text = m_DevDebugGameObject.Layer.ToString();

			SetBtnDisableState();
			SetBtnHighLightState();
		}

		private void SetIsEventListener() {
			var eventSystem = m_DevDebugGameObject.GameObject.GetComponentInChildren<EventSystem>();
			if (eventSystem) {
				m_IsEventSystem = true;
			}
		}


		private void Awake() {
			m_RootTransform = DevViewOrderHandler.Singleton.gameObject.transform;
			var btnEventHighLight = new Button.ButtonClickedEvent();
			btnEventHighLight.AddListener(HighLightEvent);
			btnGameObjectButtonHighLight.onClick = btnEventHighLight;
			m_BtnHighLightText = btnGameObjectButtonHighLight.GetComponentInChildren<TMP_Text>();


			var btnDEventisable = new Button.ButtonClickedEvent();
			btnDEventisable.AddListener(DisableGameObjectEvent);
			btnGameObjectButtonDisable.onClick = btnDEventisable;
			m_BtnDisableText = btnGameObjectButtonDisable.GetComponentInChildren<TMP_Text>();

			var btnEventEdit = new Button.ButtonClickedEvent();
			btnEventEdit.AddListener(OpenEditGameObjectViewEvent);
			btnEditGameObject.onClick = btnEventEdit;
			m_BtnEditText = btnEditGameObject.GetComponentInChildren<TMP_Text>();
		}

		private void OpenEditGameObjectViewEvent() {
			var editWindow = Instantiate(prefabGameObjectEditView, m_RootTransform);
			editWindow.GetComponent<DevGameObjectEditView>().SetDevGameObject(m_DevDebugGameObject);
			editWindow.SetActive(true);
		}

		private void HighLightEvent() {
			if (m_DevGameObject.GetIsHighLighted) {
				m_DevGameObject.OnResetHighLightEvent();
			}
			else {
				m_DevGameObject.OnHighLightEvent(null);
			}

			SetBtnHighLightState();
		}

		private void DisableGameObjectEvent() {
			var activeState = !m_DevDebugGameObject.GameObject.activeSelf;
			m_DevDebugGameObject.GameObject.SetActive(activeState);
			SetBtnDisableState();
		}

		private void SetBtnHighLightState() {
			if (m_DevGameObject == null) {
				btnGameObjectButtonHighLight.interactable = false;
				return;
			}

			if (m_DevGameObject.GetIsHighLighted) {
				m_BtnHighLightText.text = "Unhighlight";
			}
			else {
				m_BtnHighLightText.text = "Highlight";
			}
		}

		private void SetBtnDisableState() {
			btnGameObjectButtonDisable.interactable = !m_IsEventSystem;
			if (m_DevDebugGameObject.GameObject.activeSelf) {
				m_BtnDisableText.text = "Disable";
			}
			else {
				m_BtnDisableText.text = "Enable";
			}
		}
	}
}