using System.Collections.Generic;
using MakotoStudio.Debugger.Models;
using UnityEngine;

namespace MakotoStudio.Debugger.ScriptableObjects {
	public class MsDebuggerSettings : ScriptableObject {
		[Header("General Config")]
		[SerializeField] private LogType logType;
		[SerializeField] private string logPath;

		[Header("Special Settings")]
		[SerializeField] private List<string> componentsToIgnoreList;
		[SerializeField] private List<string> componentsNotDisableList;
		[SerializeField] private List<string> propertiesToIgnore;

		[Header("Tag Colors List")]
		[SerializeField] private List<DebugObjectColor> debugObjectTagColors;


		public LogType LogType {
			get => logType;
			set => logType = value;
		}

		public string LogPath {
			get {
				if (string.IsNullOrWhiteSpace(logPath)) {
					logPath = Application.persistentDataPath + "/Player.log";
				}

				return logPath;
			}
			set => logPath = value;
		}

		public List<string> ComponentsToIgnoreList {
			get => componentsToIgnoreList;
			set => componentsToIgnoreList = value;
		}

		public List<string> ComponentsNotDisableList {
			get => componentsNotDisableList;
			set => componentsNotDisableList = value;
		}

		public List<string> PropertiesToIgnore {
			get => propertiesToIgnore;
			set => propertiesToIgnore = value;
		}

		public List<DebugObjectColor> DebugObjectTagColors {
			get => debugObjectTagColors;
			set => debugObjectTagColors = value;
		}
	}
}