using System.Collections.Generic;
using MakotoStudio.Debugger.Models;
using UnityEngine;

namespace MakotoStudio.Debugger.ScriptableObjects {
	/// <summary>
	/// Debugger Settings Configuration information holder
	/// </summary>
	public class MsDebuggerSettings : ScriptableObject {
		[Header("General Config")]
		[SerializeField] private LogType logType;
		[SerializeField] private string logPath;
		[SerializeField] private LayerMask layerMaskField;
		
		[Header("Special Settings")]
		[SerializeField] private List<string> componentsToIgnoreList;
		[SerializeField] private List<string> componentsNotDisableList;
		[SerializeField] private List<string> propertiesToIgnore;

		[Header("Tag Colors List")]
		[SerializeField] private List<DebugObjectColor> debugObjectTagColors;


		/// <summary>
		/// Configured LogType Property
		/// </summary>
		public LogType LogType {
			get => logType;
			set => logType = value;
		}

		/// <summary>
		/// Configured LogPath Property
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
		/// Configured LayerMask to ignore
		/// </summary>
		public LayerMask LayerMaskField {
			get => layerMaskField;
			set => layerMaskField = value;
		}

		/// <summary>
		/// Configured ComponentsToIgnoreList Property
		/// </summary>
		public List<string> ComponentsToIgnoreList {
			get => componentsToIgnoreList;
			set => componentsToIgnoreList = value;
		}

		/// <summary>
		/// Configured ComponentsNotDisableList Property
		/// </summary>
		public List<string> ComponentsNotDisableList {
			get => componentsNotDisableList;
			set => componentsNotDisableList = value;
		}

		/// <summary>
		/// Configured PropertiesToIgnore Property
		/// </summary>
		public List<string> PropertiesToIgnore {
			get => propertiesToIgnore;
			set => propertiesToIgnore = value;
		}

		/// <summary>
		/// Configured DebugObjectTagColors Property
		/// </summary>
		public List<DebugObjectColor> DebugObjectTagColors {
			get => debugObjectTagColors;
			set => debugObjectTagColors = value;
		}
	}
}