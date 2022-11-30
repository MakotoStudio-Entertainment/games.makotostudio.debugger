using UnityEngine;

namespace MakotoStudio.Debugger.ScriptableObjects {
	public class MsMaterialSettings : ScriptableObject {
		[Space(20)]
		[Header("Default Material")]
		[SerializeField] public Material defaultMaterial;

		[Header("Tag Materials")]
		[SerializeField] public Material untaggedTagMaterial;
		[SerializeField] public Material respawnTagMaterial;
		[SerializeField] public Material finishTagMaterial;
		[SerializeField] public Material editorOnlyTagMaterial;
		[SerializeField] public Material mainCameraTagMaterial;
		[SerializeField] public Material playerTagMaterial;
		[SerializeField] public Material gameControllerTagMaterial;
	}
}