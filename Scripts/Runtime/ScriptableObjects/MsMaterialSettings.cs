using UnityEngine;

namespace MakotoStudio.Debugger.ScriptableObjects {
	public class MsMaterialSettings : ScriptableObject {
		[Space(20)]
		[Header("Default Material")]
		[SerializeField] private Material defaultMaterial;

		[Header("Tag Materials")]
		[SerializeField] private Material untaggedTagMaterial;
		[SerializeField] private Material respawnTagMaterial;
		[SerializeField] private Material finishTagMaterial;
		[SerializeField] private Material editorOnlyTagMaterial;
		[SerializeField] private Material mainCameraTagMaterial;
		[SerializeField] private Material playerTagMaterial;
		[SerializeField] private Material gameControllerTagMaterial;

		/// <summary>
		/// DefaultMaterial Property
		/// </summary>
		public Material DefaultMaterial {
			get => defaultMaterial;
			set => defaultMaterial = value;
		}

		/// <summary>
		/// UntaggedTagMaterial Property
		/// </summary>
		public Material UntaggedTagMaterial {
			get => untaggedTagMaterial;
			set => untaggedTagMaterial = value;
		}

		/// <summary>
		/// RespawnTagMaterial Property
		/// </summary>
		public Material RespawnTagMaterial {
			get => respawnTagMaterial;
			set => respawnTagMaterial = value;
		}

		/// <summary>
		/// FinishTagMaterial Property
		/// </summary>
		public Material FinishTagMaterial {
			get => finishTagMaterial;
			set => finishTagMaterial = value;
		}

		/// <summary>
		/// EditorOnlyTagMaterial Property
		/// </summary>
		public Material EditorOnlyTagMaterial {
			get => editorOnlyTagMaterial;
			set => editorOnlyTagMaterial = value;
		}

		/// <summary>
		/// MainCameraTagMaterial Property
		/// </summary>
		public Material MainCameraTagMaterial {
			get => mainCameraTagMaterial;
			set => mainCameraTagMaterial = value;
		}

		/// <summary>
		/// PlayerTagMaterial Property
		/// </summary>
		public Material PlayerTagMaterial {
			get => playerTagMaterial;
			set => playerTagMaterial = value;
		}

		/// <summary>
		/// GameControllerTagMaterial Property
		/// </summary>
		public Material GameControllerTagMaterial {
			get => gameControllerTagMaterial;
			set => gameControllerTagMaterial = value;
		}
	}
}