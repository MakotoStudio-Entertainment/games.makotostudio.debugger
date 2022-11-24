using System.Collections.Generic;
using System.Diagnostics;
using MakotoStudio.Debugger.Constant;
using MakotoStudio.Debugger.Interfaces;
using MakotoStudio.Debugger.Utils;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace MakotoStudio.Debugger.Views {
	public class DevLogConfigView : MonoBehaviour, IViewOrder {
		[SerializeField] private Button btnDevLogOutputView;
		[SerializeField] private GameObject devLogOutputView;
		private string m_DevLogOutputViewOpenText;
		private string m_DevLogOutputViewClosedText;

		[SerializeField] private Button btnDevGameObjectListView;
		[SerializeField] private GameObject devGameObjectListView;
		private string m_DevGameObjectListViewOpenText;
		private string m_DevGameObjectListViewClosedText;

		[SerializeField] private Button btnOpenLogFile;

		[Space(20)] [Header("Log Config View")] [Header("Log Level")] [SerializeField]
		private Dropdown logLevelDropdown;


		private ActiveInputSystemType m_ActiveInputSystemType;

		public void OnBarClickEnd() {
			DevViewOrderHandler.Singleton.SetViewOnTop(this);
			var index = transform.GetSiblingIndex();
		}

		public void SetAtIndex(int newIndex) {
			transform.SetSiblingIndex(newIndex);
		}

		public void SetActiveView() {
			gameObject.SetActive(!gameObject.activeSelf);
		}


		private void Awake() {
			InitUI();
		}

		private void InitUI() {
			var openLogView = new Button.ButtonClickedEvent();
			openLogView.AddListener(BtnOpenDevLogOutputView);
			btnDevLogOutputView.onClick = openLogView;

			var openGoListView = new Button.ButtonClickedEvent();
			openGoListView.AddListener(BtnOpenDevGameObjectListView);
			btnDevGameObjectListView.onClick = openGoListView;

			var openLogFileEvent = new Button.ButtonClickedEvent();
			openLogFileEvent.AddListener(BtnOpenLogFileEvent);
			btnOpenLogFile.onClick = openLogFileEvent;

			var optionDataList = new List<Dropdown.OptionData>();
			var list = new List<string>() {
				LogType.Error.ToString(),
				LogType.Assert.ToString(),
				LogType.Exception.ToString(),
				LogType.Log.ToString(),
				LogType.Warning.ToString()
			};
			list.ForEach(i => optionDataList.Add(new Dropdown.OptionData {text = i}));

			logLevelDropdown.options = optionDataList;
			var dropdownChangeEvent = new Dropdown.DropdownEvent();
			dropdownChangeEvent.AddListener(ChangeDropdownValueEvent);
			logLevelDropdown.onValueChanged = dropdownChangeEvent;
		}

		private void BtnOpenLogFileEvent() {
			var logFilePath = DevMaterialUtil.Singleton.MsDebuggerSettings.LogPath;
			Process.Start("notepad.exe", logFilePath);
		}

		private void BtnOpenDevLogOutputView() {
			devLogOutputView.SetActive(!devLogOutputView.activeSelf);
		}

		private void BtnOpenDevGameObjectListView() {
			devGameObjectListView.SetActive(!devGameObjectListView.activeSelf);
		}

		private void ChangeDropdownValueEvent(int value) {
			switch (logLevelDropdown.options[value].text) {
				case "Assert":
					Debug.unityLogger.filterLogType = LogType.Assert;
					break;
				case "Warning":
					Debug.unityLogger.filterLogType = LogType.Warning;
					break;
				case "Error":
					Debug.unityLogger.filterLogType = LogType.Error;
					break;
				case "Exception":
					Debug.unityLogger.filterLogType = LogType.Exception;
					break;
				default:
					Debug.unityLogger.filterLogType = LogType.Log;
					break;
			}
		}
	}
}