namespace SOG.DepthManager{
  public static class DepthEvent{
    public delegate void DepthDelegate(int depth);
    public static event DepthDelegate EventDepth;
    public static void Raise(int depth) {EventDepth?.Invoke(depth);}
  }
  public static class GameOverDepthScoreEvent {
    public static event System.Action<int, int> EventGameOverDepthScore;
    public static void Raise(int depth, int bestDepth) {EventGameOverDepthScore?.Invoke(depth, bestDepth);}
  }
}
