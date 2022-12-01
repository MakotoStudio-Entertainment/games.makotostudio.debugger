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


		/// <summary>
		/// LogType Property
		/// </summary>
		public LogType LogType {
			get => logType;
			set => logType = value;
		}

		/// <summary>
		/// LogPath Property
		/// </summary>
		public string LogPath {
			get {
				if (string.IsNullOrWhiteSpace(logPath)) {
					logPath = Application.persistentDataPath + "/Player.log";
				}

				return logPath;
			}
			set => logPath = value;
		}

		/// <summary>
		/// ComponentsToIgnoreList Property
		/// </summary>
		public List<string> ComponentsToIgnoreList {
			get => componentsToIgnoreList;
			set => componentsToIgnoreList = value;
		}

		/// <summary>
		/// ComponentsNotDisableList Property
		/// </summary>
		public List<string> ComponentsNotDisableList {
			get => componentsNotDisableList;
			set => componentsNotDisableList = value;
		}

		/// <summary>
		/// PropertiesToIgnore Property
		/// </summary>
		public List<string> PropertiesToIgnore {
			get => propertiesToIgnore;
			set => propertiesToIgnore = value;
		}

		/// <summary>
		/// DebugObjectTagColors Property
		/// </summary>
		public List<DebugObjectColor> DebugObjectTagColors {
			get => debugObjectTagColors;
			set => debugObjectTagColors = value;
		}
	}
}