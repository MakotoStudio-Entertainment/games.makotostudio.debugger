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
		private DevDebugObjectInformation m_DevDebugObjectInformation;
		private TMP_Text m_BtnHighLightText;
		private TMP_Text m_BtnDisableText;
		private MsDebuggerGameObject m_MsDebuggerGameObject;
		private bool m_IsEventSystem;


		public void SetDevDebugGameObject(DevDebugObjectInformation value) {
			m_DevDebugObjectInformation = value;
			m_MsDebuggerGameObject = m_DevDebugObjectInformation.GameObject.GetComponent<MsDebuggerGameObject>();
			SetIsEventListener();

			gameObjectName.text = m_DevDebugObjectInformation.Name;
			gameObjectTag.text = m_DevDebugObjectInformation.Tag;
			gameObjectLayer.text = m_DevDebugObjectInformation.Layer.ToString();

			SetBtnDisableState();
			SetBtnHighLightState();
		}

		private void SetIsEventListener() {
			var eventSystem = m_DevDebugObjectInformation.GameObject.GetComponentInChildren<EventSystem>();
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
		}

		private void OpenEditGameObjectViewEvent() {
			var editWindow = Instantiate(prefabGameObjectEditView, m_RootTransform);
			editWindow.GetComponent<DevGameObjectEditView>().SetDevDebugGameObject(m_DevDebugObjectInformation);
			editWindow.SetActive(true);
		}

		private void HighLightEvent() {
			if (m_MsDebuggerGameObject.GetIsHighLighted) {
				m_MsDebuggerGameObject.OnResetHighLightEvent();
			}
			else {
				m_MsDebuggerGameObject.OnHighLightEvent(null);
			}

			SetBtnHighLightState();
		}

		private void DisableGameObjectEvent() {
			var activeState = !m_DevDebugObjectInformation.GameObject.activeSelf;
			m_DevDebugObjectInformation.GameObject.SetActive(activeState);
			SetBtnDisableState();
		}

		private void SetBtnHighLightState() {
			if (m_MsDebuggerGameObject == null) {
				btnGameObjectButtonHighLight.interactable = false;
				return;
			}

			if (m_MsDebuggerGameObject.GetIsHighLighted) {
				m_BtnHighLightText.text = "Unhighlight";
			}
			else {
				m_BtnHighLightText.text = "Highlight";
			}
		}

		private void SetBtnDisableState() {
			btnGameObjectButtonDisable.interactable = !m_IsEventSystem;
			if (m_DevDebugObjectInformation.GameObject.activeSelf) {
				m_BtnDisableText.text = "Disable";
			}
			else {
				m_BtnDisableText.text = "Enable";
			}
		}
	}
}