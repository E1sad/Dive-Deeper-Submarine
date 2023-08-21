namespace SOG.UI.Credits{
  public class CreditsButtonEventArgs : System.EventArgs{
    public UIEnum From;
    public CreditsButtonEventArgs(UIEnum from) { From = from; }
  }
  public static class CreditsButtonEvent{
    public delegate void CreditsButtonDelegate(CreditsButtonEventArgs EventArgs);
    public static event CreditsButtonDelegate OnCreditsButtonPressedEvent;
    public static void Raise(CreditsButtonEventArgs EventArgs) { OnCreditsButtonPressedEvent?.Invoke(EventArgs); }
  }
}
