using System.Reflection;
using UObject = UnityEngine.Object;

namespace MakotoStudio.Debugger.Interfaces {
	/// <summary>
	/// Property Type interface to extend Property Type Views
	/// </summary>
	public interface IPropertyType {
		/// <summary>
		///  Set Property info and the object of the proprty
		/// </summary>
		/// <param name="propertyInfo">Property info</param>
		/// <param name="obj">The Object of the property holder</param>
		public void SetPropertyInfo(PropertyInfo propertyInfo, UObject obj);

		/// <summary>
		///  Set if live update the value of the property
		/// </summary>
		/// <param name="state">Change the live update state</param>
		public void SetLiveUpdate(bool state);
	}
}