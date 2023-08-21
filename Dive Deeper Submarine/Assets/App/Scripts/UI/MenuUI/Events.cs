using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.UI.Menu{
  public static class PlayButtonEvent{
    public delegate void PlayButtonDelegate();
    public static event PlayButtonDelegate OnPlayButtonPressedEvent;
    public static void Raise(){ OnPlayButtonPressedEvent?.Invoke(); }
  }
  public class BackButtonEventArgs: System.EventArgs {
    public UIEnum From;
    public BackButtonEventArgs(UIEnum from) { From = from; }
  }
  public static class BackButtonEvent{
    public delegate void BackButtonDelegate(BackButtonEventArgs evetArgs);
    public static event BackButtonDelegate OnBackButtonPressedEvent;
    public static void Raise(BackButtonEventArgs evetArgs) { OnBackButtonPressedEvent?.Invoke(evetArgs); }
  }
}
