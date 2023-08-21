namespace SOG.UI.Pause{
  public static class PauseEvent{
    public delegate void PauseButtonDelegate();
    public static event PauseButtonDelegate OnPauseEvent;
    public static void Raise() { OnPauseEvent?.Invoke(); }
  }
  public static class ToMenuEvent{
    public delegate void ToMenuDelegate();
    public static event ToMenuDelegate OnToMenuEvent;
    public static void Raise() { OnToMenuEvent?.Invoke(); }
  }
  public static class ContinueEvent {
    public delegate void ContinueDeleagate();
    public static event ContinueDeleagate OnContinueEvent;
    public static void Raise() { OnContinueEvent?.Invoke();}
  }
  public static class RestartEvent{
    public delegate void RestartDeleagate();
    public static event RestartDeleagate OnRestartEvent;
    public static void Raise() { OnRestartEvent?.Invoke(); }
  }
}
