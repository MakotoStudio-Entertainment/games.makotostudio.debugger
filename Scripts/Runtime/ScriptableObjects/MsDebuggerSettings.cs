using System.Collections.Generic;
using System.Linq;
using MakotoStudio.Debugger.Constant;
using MakotoStudio.Debugger.Models;
using UnityEngine;
using Application = UnityEngine.Device.Application;

namespace MakotoStudio.Debugger.ScriptableObjects {
	public class MsDebuggerSettings : ScriptableObject {
		[Header("General Config")]
		[SerializeField] private LogType logType;
		[SerializeField] private string logPath;
		

		[Header("Special Components")]
		[SerializeField] private List<string> componentsToIgnoreList;//= new List<string>( {"Transform", "MeshFilter"} );
		[SerializeField] private List<string> componentsNotDisableList;

		[Header("SpecialProperties")]
		[SerializeField] private List<string> propertiesToIgnore; 

		[Header("Tag Colors List")]
		[SerializeField] private List<DebugObjectColor> debugObjectTagColors;
		public List<DebugObjectColor> DebugObjectTagColors => debugObjectTagColors;
		[SerializeField] private List<DebugObjectColor> debugObjectLayerColors;
		public List<DebugObjectColor> DebugObjectLayerColors => debugObjectLayerColors;

		[Space(20)]
		[Header("Default Material")]
		[SerializeField] public Material defaultMaterial;
		[SerializeField] public Material defaultLayerMaterial;

		[Header("Tag Materials")]
		[SerializeField] public Material untaggedTagMaterial;
		[SerializeField] public Material respawnTagMaterial;
		[SerializeField] public Material finishTagMaterial;
		[SerializeField] public Material editorOnlyTagMaterial;
		[SerializeField] public Material mainCameraTagMaterial;
		[SerializeField] public Material playerTagMaterial;
		[SerializeField] public Material gameControllerTagMaterial;

		[Header("Layer Materials")]
		[SerializeField] public Material transparentFxLayerMaterial;
		[SerializeField] public Material ignoreRaycastLayerMaterial;
		[SerializeField] public Material waterLayerMaterial;
		[SerializeField] public Material uiLayerMaterial;


		public LogType LogType {
			get => logType;
			set => logType = value;
		}

		public string LogPath {
			get {
				if (string.IsNullOrWhiteSpace(logPath)) {
					logPath = Application.persistentDataPath + "/Player.log";
				}

				Debug.Log(logPath);
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

		#region Unity Inspepector

		/// <summary>
		///  For Editor purpose (Load and Update Tags / Layers
		/// </summary>
#if UNITY_EDITOR
		public void BtnLoadUpdateTagList() {
			var tags = UnityEditorInternal.InternalEditorUtility.tags;
			FindAndAddToList(debugObjectTagColors, tags);
			RemoveNotExist(debugObjectTagColors, tags);
			UpdateMaterial(debugObjectTagColors);
		}

		public void BtnLoadUpdateLayerList() {
			var layers = UnityEditorInternal.InternalEditorUtility.layers;
			FindAndAddToList(debugObjectLayerColors, layers);
			RemoveNotExist(debugObjectLayerColors, layers);
			UpdateMaterial(debugObjectLayerColors);
		}

		private void FindAndAddToList(List<DebugObjectColor> debugObjectColors, string[] stringList) {
			foreach (var s in stringList) {
				if (debugObjectColors.Find(m => m.Name == s) == null) {
					debugObjectColors.Add(new DebugObjectColor {
						Name = s,
						ColorMaterial = new Material(defaultMaterial)
					});
				}
			}
		}

		private void RemoveNotExist(List<DebugObjectColor> debugObjectColors, string[] stringList) {
			var toRemoveFromList = new List<DebugObjectColor>();
			debugObjectColors.ForEach(t => {
				var match = stringList.ToList().Find(m => m == t.Name);
				if (match == null) {
					toRemoveFromList.Add(t);
				}
			});
			toRemoveFromList.ForEach(t => debugObjectColors.Remove(t));
		}

		private void UpdateMaterial(List<DebugObjectColor> debugObjectColors) {
			debugObjectColors.ForEach(n => {
				var debugObjectName = n.Name.Replace(" ", "");
				if (debugObjectName == DefaultUnityTagLayerType.Untagged.ToString()) {
					n.ColorMaterial = untaggedTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.Respawn.ToString()) {
					n.ColorMaterial = respawnTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.Finished.ToString()) {
					n.ColorMaterial = finishTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.Player.ToString()) {
					n.ColorMaterial = playerTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.EditorOnly.ToString()) {
					n.ColorMaterial = editorOnlyTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.GameController.ToString()) {
					n.ColorMaterial = gameControllerTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.MainCamera.ToString()) {
					n.ColorMaterial = mainCameraTagMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.Default.ToString()) {
					n.ColorMaterial = defaultLayerMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.TransparentFX.ToString()) {
					n.ColorMaterial = transparentFxLayerMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.IgnoreRaycast.ToString()) {
					n.ColorMaterial = ignoreRaycastLayerMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.Water.ToString()) {
					n.ColorMaterial = waterLayerMaterial;
				}
				else if (debugObjectName == DefaultUnityTagLayerType.UI.ToString()) {
					n.ColorMaterial = uiLayerMaterial;
				}
			});
		}

#endif

		#endregion
	}
}