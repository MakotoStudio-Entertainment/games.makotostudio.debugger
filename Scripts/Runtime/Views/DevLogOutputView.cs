using System;
using System.Collections;
using MakotoStudio.Debugger.Interfaces;
using MakotoStudio.Debugger.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UObject = UnityEngine.Object;

namespace MakotoStudio.Debugger.Views {
	public class DevLogOutputView : MonoBehaviour, IViewOrder {
		[Header("Log View")] [SerializeField] private TMP_Text logOutputText;

		[SerializeField] private uint maxLogSize = 15;
		[SerializeField] private bool liveLogEnabled;
		[SerializeField] private Button btnEnableLiveLog;
		
		private Queue m_LogQueue = new();

		public void OnBarClickEnd() {
			DevViewOrderHandler.Singleton.SetViewOnTop(this);
		}

		public void SetAtSiblingIndex(int index) {
			transform.SetSiblingIndex(index);
		}

		/// <summary>
		///		Set view active or inactive based on activeSelf
		/// </summary>
		public void SetActiveView() {
			gameObject.SetActive(!gameObject.activeSelf);
		}

		private void AddLogText(string logString, string stackTrace, LogType type) {
			if (!liveLogEnabled)
				return;
			m_LogQueue.Enqueue("[" + type + "] : " + logString);
			if (type == LogType.Exception)
				m_LogQueue.Enqueue(stackTrace);
			while (m_LogQueue.Count > maxLogSize)
				m_LogQueue.Dequeue();

			logOutputText.text = "\n" + string.Join("\n", m_LogQueue.ToArray());
		}

		private void Awake() {
			var logEnableButtonEvent = new Button.ButtonClickedEvent();
			logEnableButtonEvent.AddListener(LiveLogStatus);
			btnEnableLiveLog.onClick = logEnableButtonEvent;
		}

		private void LiveLogStatus() {
			liveLogEnabled = !liveLogEnabled;

			if (liveLogEnabled) {
				btnEnableLiveLog.GetComponentInChildren<Text>().text = "Disable Live Log";
			}
			else {
				btnEnableLiveLog.GetComponentInChildren<Text>().text = "Enable Live Log";
			}
		}

		private void OnEnable() {
			Application.logMessageReceived -= AddLogText;
			Application.logMessageReceived += AddLogText;
		}

		private void OnDisable() {
			Application.logMessageReceived -= AddLogText;
		}
	}
}