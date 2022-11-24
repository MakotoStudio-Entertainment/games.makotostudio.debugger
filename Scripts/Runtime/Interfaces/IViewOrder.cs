namespace MakotoStudio.Debugger.Interfaces {
	public interface IViewOrder {
		public void OnBarClickEnd();
		public void SetAtIndex(int newIndex);
		public void SetActiveView();
	}
}