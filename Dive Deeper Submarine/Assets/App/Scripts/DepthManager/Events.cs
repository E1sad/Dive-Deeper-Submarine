namespace SOG.DepthManager{
  public static class DepthEvent{
    public delegate void DepthDelegate(int depth);
    public static event DepthDelegate EventDepth;
    public static void Raise(int depth) {EventDepth?.Invoke(depth);}
  }
}
