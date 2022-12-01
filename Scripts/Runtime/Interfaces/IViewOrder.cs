namespace MakotoStudio.Debugger.Interfaces {
	public interface IViewOrder {
		public void SetToFront();
		public void SetAtSiblingIndex(int index);
		public void SetActiveView();
	}
}