using System.Reflection;
using UnityEngine;

namespace MakotoStudio.Debugger.Interfaces {
  public interface IPropertyType {
    /// <summary>
		///  Set Property info and the object of the proprty
		/// </summary>
		/// <param name="propertyInfo"></param>
		/// <param name="obj"></param>
    public void SetPropertyInfo(PropertyInfo propertyInfo, Object obj);
    
    /// <summary>
    ///  Set if live update the value of the property
    /// </summary>
    /// <param name="state"></param>
    public void SetLiveUpdate(bool state);
  }
}