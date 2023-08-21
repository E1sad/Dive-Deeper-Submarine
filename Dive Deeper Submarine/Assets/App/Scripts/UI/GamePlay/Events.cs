namespace SOG.UI.GamePlay{
  public static class GamePlayEvent{
    public static event System.Action OnGamePlayEvent;
    public static void Raise() { OnGamePlayEvent?.Invoke(); }
  }
}
