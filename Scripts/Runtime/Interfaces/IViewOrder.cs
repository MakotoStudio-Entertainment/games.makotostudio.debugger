namespace MakotoStudio.Debugger.Interfaces {
	public interface IViewOrder {
		/// <summary>
		///		
		/// </summary>
		public void OnBarClickEnd();
		
		/// <summary>
		/// Set the game object to the sibling index
		/// </summary>
		/// <param name="index">Index to set.</param>
		public void SetAtSiblingIndex(int index);
		
		/// <summary>
		///		Set view active or inactive based on activeSelf
		/// </summary>
		public void SetActiveView();
	}
}