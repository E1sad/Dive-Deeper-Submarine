using System;

namespace SOG.GameManger {
  public class OnGameStateChangeEventArg : EventArgs {
    public GameStateEnum Current, Previous;
    public OnGameStateChangeEventArg(GameStateEnum current, GameStateEnum previous) {
      Current = current; Previous = previous;}
  }
  public static class OnGameStateChangedEvent {
    public delegate void OnGameStateChangedDelegate(OnGameStateChangeEventArg gameState);
    public static event OnGameStateChangedDelegate EventGameStateChanged;
    public static void Raise(OnGameStateChangeEventArg eventArgs) {EventGameStateChanged?.Invoke(eventArgs);}
  }
}