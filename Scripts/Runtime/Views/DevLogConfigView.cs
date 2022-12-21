using System.Collections.Generic;
using System.Diagnostics;
using MakotoStudio.Debugger.Constants;
using MakotoStudio.Debugger.Interfaces;
using MakotoStudio.Debugger.Utils;
using UnityEngine;
using UnityEngine.UI;
using UDebug = UnityEngine.Debug;

namespace MakotoStudio.Debugger.Views {
	/// <summary>
	/// Log Config view, displays configuration options 
	/// </summary>
	public class DevLogConfigView : MonoBehaviour, IViewOrder {
		[SerializeField] private Dropdown logLevelDropdown;
		[SerializeField] private Button btnDevLogOutputView;
		[SerializeField] private GameObject devLogOutputView;
		[SerializeField] private Button btnDevGameObjectListView;
		[SerializeField] private GameObject devGameObjectListView;
		[SerializeField] private Button btnOpenLogFile;

		private ActiveInputSystemType m_ActiveInputSystemType;
		private string m_DevLogOutputViewOpenText;
		private string m_DevLogOutputViewClosedText;
		private string m_DevGameObjectListViewOpenText;
		private string m_DevGameObjectListViewClosedText;

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
			DebuggerUIUtil.BindButtonUnityAction(btnDevLogOutputView,
				() => { devLogOutputView.SetActive(!devLogOutputView.activeSelf); });
			DebuggerUIUtil.BindButtonUnityAction(btnDevGameObjectListView,
				() => { devGameObjectListView.SetActive(!devGameObjectListView.activeSelf); });
			DebuggerUIUtil.BindButtonUnityAction(btnOpenLogFile,
				() => { Process.Start("notepad.exe", DevDebuggerSettingManager.Singleton.MsDebuggerSettings.LogPath); });

			var optionDataList = new List<Dropdown.OptionData>();
			var list = new List<string>() {
				LogType.Error.ToString(),
				LogType.Assert.ToString(),
				LogType.Exception.ToString(),
				LogType.Log.ToString(),
				LogType.Warning.ToString()
			};
			list.ForEach(i => optionDataList.Add(new Dropdown.OptionData {text = i}));
			DebuggerUIUtil.BindDropdownUnityAction(logLevelDropdown, optionDataList, ChangeDropdownValueEvent);
		}

		private void ChangeDropdownValueEvent(int value) {
			switch (logLevelDropdown.options[value].text) {
				case "Assert":
					UDebug.unityLogger.filterLogType = LogType.Assert;
					break;
				case "Warning":
					UDebug.unityLogger.filterLogType = LogType.Warning;
					break;
				case "Error":
					UDebug.unityLogger.filterLogType = LogType.Error;
					break;
				case "Exception":
					UDebug.unityLogger.filterLogType = LogType.Exception;
					break;
				default:
					UDebug.unityLogger.filterLogType = LogType.Log;
					break;
			}

			UDebug.LogFormat(UDebug.unityLogger.filterLogType, LogOption.None, this, "New Log Level");
		}
	}
}