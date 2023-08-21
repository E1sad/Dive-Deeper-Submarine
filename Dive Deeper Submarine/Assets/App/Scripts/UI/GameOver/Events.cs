using System;

namespace SOG.UI.GameOver{
  public static class GameOverEvent{
    public static event Action OnGameOverEvent;
    public static void Raise() { OnGameOverEvent?.Invoke(); }
  }
}
