using MakotoStudio.Debugger.Interfaces;
using MakotoStudio.Debugger.Utils;
using TMPro;
using UnityEngine;

namespace MakotoStudio.Debugger.Views {
	public class DevLogOutputView : MonoBehaviour, IViewOrder {
		[Header("Log View")] 
		[SerializeField] private TMP_Text logOutputText;

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
			// MakotoLogFileHandler.Current.OnLogEvent += LogInUi;
		}

		private void OnDestroy() {
			// MakotoLogFileHandler.Current.OnLogEvent -= LogInUi;
		}

		private void LogInUi(string logOutput) {
			logOutputText.text = logOutput + "\n" + logOutputText.text;
		}
	}
}