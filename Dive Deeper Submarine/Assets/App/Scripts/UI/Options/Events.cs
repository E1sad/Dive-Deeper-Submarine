using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Options{
  public class OptionsButtonEventArgs :System.EventArgs{
    public UIEnum From;
    public OptionsButtonEventArgs(UIEnum from) { From = from; }
  }
  public static class OptionsButtonEvent{
    public delegate void OptionsButtonDelegate(OptionsButtonEventArgs eventArgs);
    public static event OptionsButtonDelegate OnOptionsButtonPressedEvent;
    public static void Raise(OptionsButtonEventArgs eventArgs) { OnOptionsButtonPressedEvent?.Invoke(eventArgs); }
  }
}
