namespace SOG.EngineManager{
  public static class EngineBrokeEvent{
    public static event System.Action EventEngineBroke;
    public static void Raise() { EventEngineBroke?.Invoke(); }
  }
  public static class EngineRepairedEvent {
    public static event System.Action EventEngineRepaired;
    public static void Raise(){EventEngineRepaired?.Invoke();}
  }
}
