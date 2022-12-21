namespace MakotoStudio.Debugger.Interfaces {
	/// <summary>
	/// Interface for the Makoto Studio Debugger views
	/// </summary>
	public interface IViewOrder {
		/// <summary>
		/// Set View Order
		/// </summary>
		public void SetToFront();

		/// <summary>
		/// Set object sibling index in game object hierarch
		/// </summary>
		/// <param name="index"></param>
		public void SetAtSiblingIndex(int index);

		/// <summary>
		/// Sets the game object active state depending on his state
		/// </summary>
		public void SetActiveView();
	}
}