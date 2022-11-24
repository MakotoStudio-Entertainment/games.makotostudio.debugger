using System.Reflection;
using UnityEngine;

namespace MakotoStudio.Debugger.Interfaces {
  public interface IPropertyType {
    public void SetPropertyInfo(PropertyInfo propertyInfo, Object obj);
    public void SetLiveUpdate(bool state);
  }
}